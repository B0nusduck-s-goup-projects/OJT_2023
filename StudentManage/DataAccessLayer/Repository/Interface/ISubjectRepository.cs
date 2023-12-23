using DataAccessLayer.ObjectEntity;

namespace DataAccessLayer.Repository.Interface
{
    public interface ISubjectRepository
    {
        public List<SubjectEntity> Get();
        public List<SubjectEntity> Get(string name);
        public SubjectEntity? Get(int id);
        public List<SubjectEntity> Get(int pageNum, int pageLength);
        public List<SubjectEntity> Get(int pageNum, int pageLength, string name);
        public ActionStatusEntity Post(SubjectEntity subject);
        public ActionStatusEntity Put(SubjectEntity subject);
        public ActionStatusEntity Delete(int id);
        public bool HasSubject();
        public SubjectEntity? HasSubject(int id);
    }
}
