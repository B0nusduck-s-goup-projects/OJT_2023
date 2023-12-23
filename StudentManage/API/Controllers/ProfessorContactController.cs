using BusinessLayer.DTO;
using BusinessLayer.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProfessorContactController : Controller
    {
        private readonly IProfessorContactService _service;
        public ProfessorContactController(IProfessorContactService service)
        {
            _service = service;
        }

        // GET api/<ProfessorContactController>/Get/
        /// <summary>
        /// Get professor contacts
        /// </summary>
        /// <response code="200">Success: Get professor contacts</response>
        [HttpGet]
        public List<ProfessorContactDTO> Get()
        {
            return _service.Get();
        }

        // GET api/<ProfessorContactController>/GetByUser/5
        /// <summary>
        /// Get professor contacts by professor id
        /// </summary>
        ///<response code="200">Success: Get professor contacts by professor id</response>
        [HttpGet]
        public ProfessorContactDTO? GetByUser(int id)
        {
            return _service.GetByUser(id);
        }

        // GET api/<ProfessorContactController>/GetById/5
        /// <summary>
        /// Get professor contacts by id
        /// </summary>
        /// <response code="200">Success: Get professor contacts by id </response>
        [HttpGet]
        public ProfessorContactDTO? GetById(int id)
        {
            return _service.Get(id);
        }

        // GET api/<ProfessorContactController>/GetPage/?pageNum=5&pageLength=5
        /// <summary>
        /// Get and page professor contacts
        /// </summary>
        /// <response code="200">Success: Get and page professor contacts </response>
        [HttpGet]
        public List<ProfessorContactDTO> GetPage(int pageNum, int pageLength)
        {
            return _service.Get(pageNum, pageLength);
        }

        // POST api/<ProfessorContactController>/Post
        /// <summary>
        /// Create professor contact
        /// </summary>
        /// <response code="200">Success: Create professor contact</response>
        [HttpPost]
        public void Post([FromBody]ProfessorContactDTO professorContact)
        {
            _service.Post(professorContact);
        }

        // POST api/<ProfessorContactController>/Put
        /// <summary>
        /// Update professor contact
        /// </summary>
        /// <response code="200">Success: Update professor contact</response>
        [HttpPut]
        public void Put([FromBody]ProfessorContactDTO professorContact)
        {
            _service.Put(professorContact);
        }

        // POST api/<ProfessorContactController>/Delete
        /// <summary>
        /// Delete professor contact
        /// </summary>
        /// <response code="200">Success: Delete professor contact</response>
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _service.Delete(id);
        }
    }
}
