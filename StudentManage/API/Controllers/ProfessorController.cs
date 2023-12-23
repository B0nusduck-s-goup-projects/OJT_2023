using BusinessLayer.DTO;
using BusinessLayer.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProfessorController : Controller
    {
        private readonly IProfessorService _service;
        public ProfessorController(IProfessorService service)
        {
            _service = service;
        }

        // GET api/<ProfessorController>/Get/
        /// <summary>
        /// Get professors
        /// </summary>
        /// <response code="200">Success: Get professors</response>
        [HttpGet]
        public List<ProfessorDTO> Get()
        {
            return _service.Get();
        }

        // GET api/<ProfessorController>/GetByName/?name=name
        /// <summary>
        /// Get professors by name
        /// </summary>
        /// <response code="200">Success: Get professors by name</response>
        /// 
        [HttpGet]
        public List<ProfessorDTO> GetByName(string name)
        {
            return _service.Get(name);
        }

        // GET api/<ProfessorController>/GetBySubject/5
        /// <summary>
        /// Get professors by subject
        /// </summary>
        /// <response code="200">Success: Get professors by subject</response>
        [HttpGet]
        public ProfessorDTO? GetBySubject(int id)
        {
            return _service.GetBySubject(id);
        }

        // GET api/<ProfessorController>/GetById/5
        /// <summary>
        /// Get professors by id
        /// </summary>
        /// <response code="200">Success: Get professors by id</response>
        [HttpGet]
        public ProfessorDTO? GetById(int id)
        {
            return _service.Get(id);
        }

        // GET api/<ProfessorController>/GetPage/?pageNum=5&pageLength=5
        /// <summary>
        /// Get and page professors
        /// </summary>
        /// <response code="200">Success: Get and page professors</response>
        [HttpGet]
        public List<ProfessorDTO> GetPage(int pageNum, int pageLength)
        {
            return _service.Get(pageNum, pageLength);
        }

        // GET api/<ProfessorController>/GetPageByName/?pageNum=5&pageLength=5&name=name
        /// <summary>
        /// Get and page professors by name
        /// </summary>
        /// <response code="200">Success: Get and page professors by name</response>
        [HttpGet]
        public List<ProfessorDTO> GetPageByName(int pageNum, int pageLength, string name)
        {
            return _service.Get(pageNum, pageLength, name);
        }

        // POST api/<ProfessorController>/Post
        /// <summary>
        /// Create professor
        /// </summary>
        /// <response code="200">Success: Create professor</response>
        [HttpPost]
        public void Post([FromBody]ProfessorDTO professor)
        {
            _service.Post(professor);
        }

        // POST api/<ProfessorController>/Put
        /// <summary>
        /// Update professor
        /// </summary>
        /// <response code="200">Success: Update professor</response>
        [HttpPut]
        public void Put([FromBody]ProfessorDTO professor)
        {
            _service.Put(professor);
        }

        // POST api/<ProfessorController>/Delete
        /// <summary>
        /// Delete professor
        /// </summary>
        /// <response code="200">Success: Delete professor</response>
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _service.Delete(id);
        }
    }
}
