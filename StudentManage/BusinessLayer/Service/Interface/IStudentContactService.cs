
using BusinessLayer.DTO;

namespace BusinessLayer.Service.Interface
{
    public interface IStudentContactService
    {
        public List<StudentContactDTO> Get();
        public StudentContactDTO? GetByUser(int id);
        public StudentContactDTO? Get(int id);
        public List<StudentContactDTO> Get(int pageNum, int pageLength);
        public ActionStatusDTO Post(StudentContactDTO studentContact);
        public ActionStatusDTO Put(StudentContactDTO studentContact);
        public ActionStatusDTO Delete(int id);
    }
}
