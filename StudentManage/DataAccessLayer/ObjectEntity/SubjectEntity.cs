using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace DataAccessLayer.ObjectEntity
{
    [Table("subject")]
    public class SubjectEntity
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Required]
        [MinLength(5)]
        [MaxLength(40)]
        [Column("name")]
        public string Name { get; set; } = null!;
        [Required]
        [MaxLength(80)]
        [Column("description")]
        public string Description { get; set; } = null!;

        public List<ProfessorEntity>? Professors;

        public List<StudentEntity>? Students;
        public List<SubjectStudentEntity>? SubjectStudents;
    }
}
