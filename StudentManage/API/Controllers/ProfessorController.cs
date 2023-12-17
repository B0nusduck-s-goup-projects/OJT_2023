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
        [HttpGet]
        public List<ProfessorDTO> Get()
        {
            return _service.Get();
        }

        // GET api/<ProfessorController>/GetByName/?name=name
        [HttpGet]
        public List<ProfessorDTO> GetByName(string name)
        {
            return _service.Get(name);
        }

        // GET api/<ProfessorController>/GetBySubject/5
        [HttpGet]
        public ProfessorDTO? GetBySubject(int id)
        {
            return _service.GetBySubject(id);
        }

        // GET api/<ProfessorController>/GetById/5
        [HttpGet]
        public ProfessorDTO? GetById(int id)
        {
            return _service.Get(id);
        }

        // GET api/<ProfessorController>/GetPage/?pageNum=5&pageLength=5
        [HttpGet]
        public List<ProfessorDTO> GetPage(int pageNum, int pageLength)
        {
            return _service.Get(pageNum, pageLength);
        }

        // GET api/<ProfessorController>/GetPageByName/?pageNum=5&pageLength=5&name=name
        [HttpGet]
        public List<ProfessorDTO> GetPageByName(int pageNum, int pageLength, string name)
        {
            return _service.Get(pageNum, pageLength, name);
        }

        // POST api/<ProfessorController>/Post
        [HttpPost]
        public void Post([FromBody]ProfessorDTO professor)
        {
            _service.Post(professor);
        }

        // POST api/<ProfessorController>/Put
        [HttpPut]
        public void Put([FromBody]ProfessorDTO professor)
        {
            _service.Put(professor);
        }

        // POST api/<ProfessorController>/Delete
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _service.Delete(id);
        }
    }
}
