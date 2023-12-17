
using BusinessLayer.DTO;

namespace BusinessLayer.Service.Interface
{
    public interface IStudentService
    {
        public List<StudentDTO> Get();
        public List<StudentDTO> Get(string name);
        public StudentDTO? Get(int id);
        public List<StudentDTO> Get(int pageNum, int pageLength);
        public List<StudentDTO> Get(int pageNum, int pageLength, string name);
        public ActionStatusDTO Post(StudentDTO student);
        public ActionStatusDTO Put(StudentDTO student);
        public ActionStatusDTO Delete(int id);
    }
}
