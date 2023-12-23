using System.Xml.Serialization;

namespace BusinessLayer.DTO
{
    public class SubjectStudentDTO
    {
        [XmlElement("subjectId")]
        public int SubjectId { get; set; }
        [XmlElement("studentId")]
        public int StudentId { get; set; }
        [XmlElement("studentGrade1")]
        public int StudentGrade1 { get; set; }
        [XmlElement("studentGrade2")]
        public int StudentGrade2 { get; set; }

        //public SubjectDTO? Subject;
        //public StudentDTO? Student;

    }
}
