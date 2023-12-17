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
        [HttpGet]
        public List<ProfessorContactDTO> Get()
        {
            return _service.Get();
        }

        // GET api/<ProfessorContactController>/GetByUser/5
        [HttpGet]
        public ProfessorContactDTO? GetByUser(int id)
        {
            return _service.GetByUser(id);
        }

        // GET api/<ProfessorContactController>/GetById/5
        [HttpGet]
        public ProfessorContactDTO? GetById(int id)
        {
            return _service.Get(id);
        }

        // GET api/<ProfessorContactController>/GetPage/?pageNum=5&pageLength=5
        [HttpGet]
        public List<ProfessorContactDTO> GetPage(int pageNum, int pageLength)
        {
            return _service.Get(pageNum, pageLength);
        }

        // POST api/<ProfessorContactController>/Post
        [HttpPost]
        public void Post([FromBody]ProfessorContactDTO professorContact)
        {
            _service.Post(professorContact);
        }

        // POST api/<ProfessorContactController>/Put
        [HttpPut]
        public void Put([FromBody]ProfessorContactDTO professorContact)
        {
            _service.Put(professorContact);
        }

        // POST api/<ProfessorContactController>/Delete
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _service.Delete(id);
        }
    }
}
