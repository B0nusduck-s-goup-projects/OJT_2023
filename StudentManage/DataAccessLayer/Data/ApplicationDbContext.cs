using DataAccessLayer.ObjectEntity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;

namespace DataAccessLayer.Data
{
    public class ApplicationDbContext : DbContext //IdentityDbContext
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public ApplicationDbContext(DbContextOptions options) : base(options) { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            /*--------------------primary key--------------------*/
            //composite key
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<SubjectStudentEntity>()
                .HasKey(e => new
                {
                    e.StudentId,
                    e.SubjectId
                })
                .HasName("subject_student_entity_PK");

            /*--------------------foreign key--------------------*/

            //one to one key
            modelBuilder.Entity<ProfessorEntity>()
            .HasOne(e => e.ProfessorContact)
            .WithOne(e => e.Professor)
            .HasForeignKey<ProfessorContactEntity>(e => e.UserId);

            modelBuilder.Entity<StudentEntity>()
            .HasOne(e => e.StudentContact)
            .WithOne(e => e.Student)
            .HasForeignKey<StudentContactEntity>(e => e.UserId);

            //one to many key
            modelBuilder.Entity<SubjectEntity>()
            .HasMany(e => e.Professors)
            .WithOne(e => e.Subject)
            .HasForeignKey(e => e.SubjectId);



            //many-many key
            modelBuilder.Entity<SubjectEntity>()
                .HasMany(e => e.Students)
                .WithMany(e => e.Subjects)
                .UsingEntity<SubjectStudentEntity>(
                    student => student.HasOne<StudentEntity>(e => e.Student).WithMany(e => e.StudentSubjects).HasForeignKey(e => e.StudentId),
                    subject => subject.HasOne<SubjectEntity>(e => e.Subject).WithMany(e => e.SubjectStudents).HasForeignKey(e => e.SubjectId)
                );

            /*-----------------uniqe constraint-----------------*/

            modelBuilder.Entity<StudentContactEntity>()
            .HasIndex(b => b.UserId).IsUnique();

            modelBuilder.Entity<ProfessorContactEntity>()
            .HasIndex(b => b.UserId).IsUnique();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            base.OnConfiguring(builder);
            builder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        }

        //declare object entity to database
        public DbSet<ProfessorEntity> Professors {  get; set; }
        public DbSet<StudentEntity> Students {  get; set; }
        public DbSet<ProfessorContactEntity> PContacts {  get; set; }
        public DbSet<StudentContactEntity> SContacts {  get; set; }
        public DbSet<SubjectEntity> Subjects {  get; set; }
        public DbSet<SubjectStudentEntity> SubjectStudent {  get; set; }
    }
}
