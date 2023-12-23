using BusinessLayer.DTO;

namespace BusinessLayer.Service.Interface
{
    public interface ISubjectStudentService
    {
        public List<SubjectStudentDTO> Get();
        public SubjectStudentDTO? Get(int subjectId, int studentId);
        public List<SubjectStudentDTO> GetByStudent(int studentId);
        public List<SubjectStudentDTO> GetBySubject(int subjectId);
        public List<SubjectStudentDTO> GetPage(int pageNum, int pageLength);
        public ActionStatusDTO Post(SubjectStudentDTO subjectStudent);
        public ActionStatusDTO Put(SubjectStudentDTO subjectStudent);
        public ActionStatusDTO Delete(int subjectId, int studentId);
    }
}
