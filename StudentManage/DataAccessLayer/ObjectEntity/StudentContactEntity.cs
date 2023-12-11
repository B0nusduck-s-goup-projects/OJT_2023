using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.ObjectEntity
{
    [Table("student_contact")]
    public class StudentContactEntity
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Required]
        [Column("user_id")]
        public int UserId { get; set; } = 0!;
        [Phone]
        [MinLength(10)]
        [MaxLength(11)]
        [Column("phone")]
        public string? Phone { get; set; }
        [EmailAddress]
        [Column("email")]
        public string? Email { get; set; }
        [MinLength(8)]
        [MaxLength(150)]
        [Column("address")]
        public string? Address { get; set; }

        public StudentEntity Student = null!;
    }
}
