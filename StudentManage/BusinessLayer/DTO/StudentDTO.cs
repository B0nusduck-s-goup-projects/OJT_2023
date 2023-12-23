using System.Xml.Serialization;

namespace BusinessLayer.DTO
{
    public class StudentDTO
    {
        [XmlElement("id")]
        public int Id { get; set; }
        [XmlElement("fullName")]
        public string FullName { get; set; } = null!;

        //public StudentContactDTO? StudentContact;

        //public List<SubjectDTO>? Subjects;

        //public List<SubjectStudentDTO>? StudentSubjects;
    }
}
