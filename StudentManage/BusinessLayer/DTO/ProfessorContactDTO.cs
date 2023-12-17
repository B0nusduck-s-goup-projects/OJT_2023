namespace BusinessLayer.DTO
{
    public class ProfessorContactDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }

        //public ProfessorDTO? Professor;
    }
}
