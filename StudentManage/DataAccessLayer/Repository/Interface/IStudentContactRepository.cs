using DataAccessLayer.ObjectEntity;

namespace DataAccessLayer.Repository.Interface
{
    public interface IStudentContactRepository
    {
        public List<StudentContactEntity> Get();
        public StudentContactEntity? GetByUser(int id);
        public StudentContactEntity? Get(int id);
        public List<StudentContactEntity> Get(int pageNum, int pageLength);
        public ActionStatusEntity Post(StudentContactEntity studentContact);
        public ActionStatusEntity Put(StudentContactEntity studentContact);
        public ActionStatusEntity Delete(int id);
        public bool HasContact();
        public StudentContactEntity? HasContact(int id);
    }
}
