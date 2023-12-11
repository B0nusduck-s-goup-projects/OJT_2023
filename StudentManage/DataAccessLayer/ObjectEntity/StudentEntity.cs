using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.ObjectEntity
{
    [Table("student")]
    public class StudentEntity
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Required]
        [MaxLength(30)]
        [Column("first_name")]
        public string FirstName { get; set; } = null!;
        [MaxLength(30)]
        [Column("middle_name")]
        public string MiddleName { get; set; } = null!;
        [Required]
        [MaxLength(30)]
        [Column("last_name")]
        public string LastName { get; set; } = null!;

        public StudentContactEntity? StudentContact;

        public List<SubjectEntity>? Subjects;

        public List<SubjectStudentEntity>? StudentSubjects;
    }
}
