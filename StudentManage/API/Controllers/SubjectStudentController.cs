using BusinessLayer.DTO;
using BusinessLayer.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SubjectStudentController : Controller
    {
        private readonly ISubjectStudentService _service;
        public SubjectStudentController(ISubjectStudentService service)
        {
            _service = service;
        }

        // GET api/<SubjectStudentController>/Get/
        [HttpGet]
        public List<SubjectStudentDTO> Get()
        {
            return _service.Get();
        }

        // GET api/<SubjectStudentController>/GetById/?subjectId=5&studentId=5
        [HttpGet]
        public SubjectStudentDTO? GetById(int subjectId, int studentId)
        {
            return _service.Get(subjectId,studentId);
        }

        // GET api/<SubjectStudentController>/GetByStudent/5
        [HttpGet]
        public List<SubjectStudentDTO> GetByStudent(int id)
        {
            return _service.GetByStudent(id);
        }

        // GET api/<SubjectStudentController>/GetBySubject/5
        [HttpGet]
        public List<SubjectStudentDTO> GetBySubject(int id)
        {
            return _service.GetBySubject(id);
        }

        // GET api/<SubjectStudentController>/GetPage/?pageNum=5&pageLength=5
        [HttpGet]
        public List<SubjectStudentDTO> GetPage(int pageNum, int pageLength)
        {
            return _service.GetPage(pageNum, pageLength);
        }

        // POST api/<SubjectStudentController>/Post
        [HttpPost]
        public ActionStatusDTO Post(SubjectStudentDTO subject)
        {
            return _service.Post(subject);
        }

        // POST api/<SubjectStudentController>/Put
        [HttpPut]
        public ActionStatusDTO Put(SubjectStudentDTO subject)
        {
            return _service.Put(subject);
        }

        // POST api/<SubjectStudentController>/Delete
        [HttpDelete]
        public ActionStatusDTO Delete(int subjectId, int studentId)
        {
            return _service.Delete(subjectId, studentId);
        }
    }
}
