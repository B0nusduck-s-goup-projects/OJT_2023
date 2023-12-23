using DataAccessLayer.ObjectEntity;

namespace DataAccessLayer.Repository.Interface
{
    public interface IStudentRepository
    {
        public List<StudentEntity> Get();
        public List<StudentEntity> Get(string name);
        public StudentEntity? Get(int id);
        public List<StudentEntity> Get(int pageNum, int pageLength);
        public List<StudentEntity> Get(int pageNum, int pageLength, string name);
        public ActionStatusEntity Post(StudentEntity student);
        public ActionStatusEntity Put(StudentEntity student);
        public ActionStatusEntity Delete(int id);
        public bool HasStudent();
        public StudentEntity? HasStudent(int id);

    }
}
