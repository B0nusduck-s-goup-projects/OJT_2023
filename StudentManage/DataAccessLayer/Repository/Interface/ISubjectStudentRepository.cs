using DataAccessLayer.ObjectEntity;

namespace DataAccessLayer.Repository.Interface
{
    public interface ISubjectStudentRepository
    {
        public List<SubjectStudentEntity> Get();
        public SubjectStudentEntity? Get(int subjectId, int studentId);
        public List<SubjectStudentEntity> GetByStudent(int studentId);
        public List<SubjectStudentEntity> GetBySubject(int subjectId);
        public List<SubjectStudentEntity> GetPage(int pageNum, int pageLength);
        public ActionStatusEntity Post(SubjectStudentEntity subjectStudent);
        public ActionStatusEntity Put(SubjectStudentEntity subjectStudent);
        public ActionStatusEntity Delete(int subjectId, int studentId);
        public bool HasSubjectStudent();
        public SubjectStudentEntity? HasSubjectStudent(int subjectId, int studentId);
    }
}
