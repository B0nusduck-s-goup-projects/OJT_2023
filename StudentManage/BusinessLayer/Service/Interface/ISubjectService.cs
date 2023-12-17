using BusinessLayer.DTO;

namespace BusinessLayer.Service.Interface
{
    public interface ISubjectService
    {
        public List<SubjectDTO> Get();
        public List<SubjectDTO> Get(string name);
        public SubjectDTO? Get(int id);
        public List<SubjectDTO> Get(int pageNum, int pageLength);
        public List<SubjectDTO> Get(int pageNum, int pageLength, string name);
        public ActionStatusDTO Post(SubjectDTO subject);
        public ActionStatusDTO Put(SubjectDTO subject);
        public ActionStatusDTO Delete(int id);
    }
}
