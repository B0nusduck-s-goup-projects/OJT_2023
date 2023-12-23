using BusinessLayer.DTO;
using BusinessLayer.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class StudentController : Controller
    {
        private readonly IStudentService _service;
        public StudentController(IStudentService service)
        {
            _service = service;
        }

        // GET api/<StudentController>/Get/
        /// <summary>
        /// Get all students 
        /// </summary>
        /// <response code="200">Success: Get all students</response>
        [HttpGet]
        public List<StudentDTO> Get()
        {
            return _service.Get();
        }

        // GET api/<StudentController>/GetByName/?name=name
        /// <summary>
        /// Get students by name
        /// </summary>
        /// <response code="200">Success: Get students by name</response>
        [HttpGet]
        public List<StudentDTO> GetByName(string name)
        {
            return _service.Get(name);
        }

        // GET api/<StudentController>/GetById/5
        /// <summary>
        /// Get student by id
        /// </summary>
        /// <response code="200">Success: Get student by id</response>
        [HttpGet]
        public StudentDTO? GetById(int id)
        {
            return _service.Get(id);
        }

        // GET api/<StudentController>/GetPage/?pageNum=5&pageLength=5
        /// <summary>
        /// Get and page students
        /// </summary>
        /// <response code="200">Success: Get and page students</response>
        [HttpGet]
        public List<StudentDTO> GetPage(int pageNum, int pageLength)
        {
            return _service.Get(pageNum, pageLength);
        }

        // GET api/<StudentController>/GetPageByName/?pageNum=5&pageLength=5&name=name
        /// <summary>
        /// Get and page student by name
        /// </summary>
        /// <response code="200">Success: Get and page student by name</response>
        [HttpGet]
        public List<StudentDTO> GetPageByName(int pageNum, int pageLength, string name)
        {
            return _service.Get(pageNum, pageLength, name);
        }

        // POST api/<StudentController>/Post
        /// <summary>
        /// Create a student
        /// </summary>
        /// <response code="200">Success: Create a student</response>
        [HttpPost]
        public void Post([FromBody]StudentDTO student)
        {
            _service.Post(student);
        }

        // POST api/<StudentController>/Put
        /// <summary>
        /// Update a student
        /// </summary>
        /// <response code="200">Success: Update a student</response>
        [HttpPut]
        public void Put([FromBody]StudentDTO student)
        {
            _service.Put(student);
        }

        // POST api/<StudentController>/Delete
        /// <summary>
        /// Delete a student
        /// </summary>
        /// <response code="200">Success: Delete a student</response>
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _service.Delete(id);
        }
    }
}
