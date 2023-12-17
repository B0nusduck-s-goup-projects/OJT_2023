
using BusinessLayer.DTO;
using DataAccessLayer.ObjectEntity;

namespace BusinessLayer.Service.Interface
{
    public interface IProfessorContactService
    {
        public List<ProfessorContactDTO> Get();
        public ProfessorContactDTO? GetByUser(int id);
        public ProfessorContactDTO? Get(int id);
        public List<ProfessorContactDTO> Get(int pageNum, int pageLength);
        public ActionStatusDTO Post(ProfessorContactDTO contact);
        public ActionStatusDTO Put(ProfessorContactDTO contact);
        public ActionStatusDTO Delete(int id);
    }
}
