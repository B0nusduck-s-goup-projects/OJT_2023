using BusinessLayer.DTO;
using BusinessLayer.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class StudentContactController : Controller
    {
        private readonly IStudentContactService _service;
        public StudentContactController(IStudentContactService service)
        {
            _service = service;
        }

        // GET api/<StudentContactController>/Get/
        /// <summary>
        /// Get student contacts
        /// </summary>
        /// <response code="200">Success: Get student contacts</response>
        [HttpGet]
        public List<StudentContactDTO> Get()
        {
            return _service.Get();
        }

        // GET api/<StudentContactController>/GetByUser/?name=name
        /// <summary>
        /// Get student contacts by student id
        /// </summary>
        /// <response code="200">Success: Get student contacts by student id</response>
        [HttpGet]
        public StudentContactDTO? GetByUser(int id)
        {
            return _service.GetByUser(id);
        }

        // GET api/<StudentContactController>/GetById/5
        /// <summary>
        /// Get student contacts by id
        /// </summary>
        /// <response code="200">Success: Get student contacts by id</response>
        [HttpGet]
        public StudentContactDTO? GetById(int id)
        {
            return _service.Get(id);
        }

        // GET api/<StudentContactController>/GetPage/?pageNum=5&pageLength=5
        /// <summary>
        /// Get and page student contacts
        /// </summary>
        /// <response code="200">Success: Get and page student contacts</response>
        [HttpGet]
        public List<StudentContactDTO> GetPage(int pageNum, int pageLength)
        {
            return _service.Get(pageNum, pageLength);
        }

        // POST api/<StudentContactController>/Post
        /// <summary>
        /// Create student contact
        /// </summary>
        /// <response code="200">Success: Create student contact</response>
        [HttpPost]
        public void Post([FromBody]StudentContactDTO studentContact)
        {
            _service.Post(studentContact);
        }

        // POST api/<StudentContactController>/Put
        /// <summary>
        /// Update student contact
        /// </summary>
        /// <response code="200">Success: Update student contact</response>
        [HttpPut]
        public void Put([FromBody]StudentContactDTO studentContact)
        {
            _service.Put(studentContact);
        }

        // POST api/<StudentContactController>/Delete
        /// <summary>
        /// Delete student contact
        /// </summary>
        /// <response code="200">Success: Delete student contact</response>
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _service.Delete(id);
        }
    }
}
