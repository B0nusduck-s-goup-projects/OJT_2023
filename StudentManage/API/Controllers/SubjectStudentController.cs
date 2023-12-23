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
        /// <summary>
        /// Get subject student
        /// </summary>
        [HttpGet]
        public List<SubjectStudentDTO> Get()
        {
            return _service.Get();
        }

        // GET api/<SubjectStudentController>/GetById/?subjectId=5&studentId=5
        /// <summary>
        /// Get subject student
        /// </summary>
        /// <response code="200">Success: Get subject student</response>
        [HttpGet]
        public SubjectStudentDTO? GetById(int subjectId, int studentId)
        {
            return _service.Get(subjectId,studentId);
        }

        // GET api/<SubjectStudentController>/GetByStudent/5
        /// <summary>
        /// Get subject student by student id
        /// </summary>
        /// <response code="200">Success: Get subject student by student id</response>
        [HttpGet]
        public List<SubjectStudentDTO> GetByStudent(int id)
        {
            return _service.GetByStudent(id);
        }

        // GET api/<SubjectStudentController>/GetBySubject/5
        /// <summary>
        /// Get subject student by subject id
        /// </summary>
        /// <response code="200">Success: Get subject student by subject id</response>
        [HttpGet]
        public List<SubjectStudentDTO> GetBySubject(int id)
        {
            return _service.GetBySubject(id);
        }

        // GET api/<SubjectStudentController>/GetPage/?pageNum=5&pageLength=5
        /// <summary>
        /// Get and page subject student
        /// </summary>
        /// <response code="200">Success: Get and page subject student</response>
        [HttpGet]
        public List<SubjectStudentDTO> GetPage(int pageNum, int pageLength)
        {
            return _service.GetPage(pageNum, pageLength);
        }

        // POST api/<SubjectStudentController>/Post
        /// <summary>
        /// Create subject student
        /// </summary>
        /// <response code="200">Success: Create subject student</response>
        [HttpPost]
        public ActionStatusDTO Post(SubjectStudentDTO subject)
        {
            return _service.Post(subject);
        }

        // POST api/<SubjectStudentController>/Put
        /// <summary>
        /// Update subject student 
        /// </summary>
        /// <response code="200">Success: Update subject student</response>
        [HttpPut]
        public ActionStatusDTO Put(SubjectStudentDTO subject)
        {
            return _service.Put(subject);
        }

        // POST api/<SubjectStudentController>/Delete
        /// <summary>
        /// Delete subject student
        /// </summary>
        /// <response code="200">Success: Delete subject student</response>
        [HttpDelete]
        public ActionStatusDTO Delete(int subjectId, int studentId)
        {
            return _service.Delete(subjectId, studentId);
        }
    }
}
