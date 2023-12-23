using DataAccessLayer.ObjectEntity;

namespace DataAccessLayer.Repository.Interface
{
    public interface IProfessorContactRepository
    {
        public List<ProfessorContactEntity> Get();
        public ProfessorContactEntity? GetByUser(int id);
        public ProfessorContactEntity? Get(int id);
        public List<ProfessorContactEntity> Get(int pageNum, int pageLength);
        public ActionStatusEntity Post(ProfessorContactEntity contact);
        public ActionStatusEntity Put(ProfessorContactEntity contact);
        public ActionStatusEntity Delete(int id);
        public bool HasContact();
        public ProfessorContactEntity? HasContact(int id);

    }
}
