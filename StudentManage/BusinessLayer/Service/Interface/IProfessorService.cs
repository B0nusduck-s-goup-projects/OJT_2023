
using BusinessLayer.DTO;

namespace BusinessLayer.Service.Interface
{
    public interface IProfessorService
    {
        public List<ProfessorDTO> Get();
        public List<ProfessorDTO> Get(string name);
        public ProfessorDTO? GetBySubject(int id);
        public ProfessorDTO? Get(int id);
        public List<ProfessorDTO> Get(int pageNum, int pageLength);
        public List<ProfessorDTO> Get(int pageNum, int pageLength, string name);
        public ActionStatusDTO Post(ProfessorDTO professor);
        public ActionStatusDTO Put(ProfessorDTO professor);
        public ActionStatusDTO Delete(int id);
    }
}
