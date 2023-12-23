using System.Xml.Serialization;

namespace BusinessLayer.DTO
{
    public class StudentContactDTO
    {
        [XmlElement("id")]
        public int Id { get; set; }
        [XmlElement("userId")]
        public int UserId { get; set; }
        [XmlElement("phone")]
        public string? Phone { get; set; }
        [XmlElement("email")]
        public string? Email { get; set; }
        [XmlElement("address")]
        public string? Address { get; set; }

        //public StudentDTO? Student;
    }
}


