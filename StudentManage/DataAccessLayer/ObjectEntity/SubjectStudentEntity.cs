using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.ObjectEntity
{
    [Table("subject_student")]
    public class SubjectStudentEntity
    {
        [Column("subject_id")]
        public int SubjectId { get; set; }
        [Column("student_id")]
        public int StudentId { get; set; }
        [Column("student_grade_1")]
        public int StudentGrade1 { get; set; }
        [Column("student_grade_1")]
        public int StudentGrade2 { get; set; }

        public SubjectEntity Subject = null!;
        public StudentEntity Student = null!;

    }
}
