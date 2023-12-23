using System.Xml.Serialization;

namespace BusinessLayer.DTO
{
    public class ProfessorDTO
    {
        [XmlElement("id")]
        public int Id { get; set; }
        [XmlElement("fullName")]
        public string FullName { get; set; } = null!;
        [XmlElement("subjectId")]
        public int SubjectId { get; set; }

        //public ProfessorContactDTO? ProfessorContact;
        //public SubjectDTO? Subject;
    }
}
