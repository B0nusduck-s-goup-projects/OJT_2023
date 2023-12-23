using BusinessLayer.DTO;
using BusinessLayer.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SubjectController : Controller
    {
        private readonly ISubjectService _service;
        public SubjectController(ISubjectService service)
        {
            _service = service;
        }

        // GET api/<SubjectController>/Get/
        /// <summary>
        /// Get subjects
        /// </summary>
        /// <response code="200">Success: Get subjects</response>
        [HttpGet]
        public List<SubjectDTO> Get()
        {
            return _service.Get();
        }

        // GET api/<SubjectController>/GetByName/?name=name
        /// <summary>
        /// Get subjects by name
        /// </summary>
        /// <response code="200">Success: Get subjects by name</response>
        [HttpGet]
        public List<SubjectDTO> GetByName(string name)
        {
            return _service.Get(name);
        }

        // GET api/<SubjectController>/GetById/5
        /// <summary>
        /// Get subjects by id
        /// </summary>
        /// <response code="200">Success: Get subjects by id</response>
        [HttpGet]
        public SubjectDTO? GetById(int id)
        {
            return _service.Get(id);
        }

        // GET api/<SubjectController>/GetPage/?pageNum=5&pageLength=5
        /// <summary>
        /// Get and page subjects
        /// </summary>
        /// <response code="200">Success: Get and page subjects</response>
        [HttpGet]
        public List<SubjectDTO> GetPage(int pageNum, int pageLength)
        {
            return _service.Get(pageNum, pageLength);
        }

        // GET api/<SubjectController>/GetPageByName/?pageNum=5&pageLength=5&name=name
        /// <summary>
        /// Get and page subjects by name 
        /// </summary>
        /// <response code="200">Success: Get and page subjects by name</response>
        [HttpGet]
        public List<SubjectDTO> GetPageByName(int pageNum, int pageLength, string name)
        {
            return _service.Get(pageNum, pageLength, name);
        }

        // POST api/<SubjectController>/Post
        /// <summary>
        /// Create subject 
        /// </summary>
        /// <response code="200">Success: Create subject</response>
        [HttpPost]
        public ActionStatusDTO Post(SubjectDTO subject)
        {
            return _service.Post(subject);
        }

        // POST api/<SubjectController>/Put
        /// <summary>
        /// Update subject 
        /// </summary>
        /// <response code="200">Success: Update subject</response>
        [HttpPut]
        public ActionStatusDTO Put(SubjectDTO subject)
        {
            return _service.Put(subject);
        }

        // POST api/<SubjectController>/Delete
        /// <summary>
        /// Delete subject 
        /// </summary>
        /// <response code="200">Success: Delete subject</response>
        [HttpDelete("{id}")]
        public ActionStatusDTO Delete(int id)
        {
            return _service.Delete(id);
        }
    }
}
