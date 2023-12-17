namespace BusinessLayer.DTO
{
    public class SubjectDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;

        //public List<ProfessorDTO>? Professors;

        //public List<StudentDTO>? Students;
        //public List<SubjectStudentDTO>? SubjectStudents;
    }
}
