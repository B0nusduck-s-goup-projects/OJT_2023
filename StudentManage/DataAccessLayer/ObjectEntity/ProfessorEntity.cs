using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.ObjectEntity
{
    [Table("professor")]
    public class ProfessorEntity
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
        [Column("subject_id")]
        public int SubjectId { get; set; } = 0!;

        public ProfessorContactEntity? ProfessorContact;
        public SubjectEntity? Subject;
    }
}
