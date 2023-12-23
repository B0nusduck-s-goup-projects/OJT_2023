using System.Xml.Serialization;

namespace BusinessLayer.DTO
{
    public class SubjectDTO
    {
        [XmlElement("id")]
        public int Id { get; set; }
        [XmlElement("name")]
        public string Name { get; set; } = null!;
        [XmlElement("description")]
        public string Description { get; set; } = null!;

        //public List<ProfessorDTO>? Professors;

        //public List<StudentDTO>? Students;
        //public List<SubjectStudentDTO>? SubjectStudents;
    }
}
