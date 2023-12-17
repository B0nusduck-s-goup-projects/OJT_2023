namespace BusinessLayer.DTO
{
    public class ProfessorDTO
    {
        public int Id { get; set; }
        public string FullName { get; set; } = null!;
        public int SubjectId { get; set; }

        //public ProfessorContactDTO? ProfessorContact;
        //public SubjectDTO? Subject;
    }
}
