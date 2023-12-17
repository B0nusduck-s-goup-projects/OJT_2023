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
        [HttpGet]
        public List<SubjectDTO> Get()
        {
            return _service.Get();
        }

        // GET api/<SubjectController>/GetByName/?name=name
        [HttpGet]
        public List<SubjectDTO> GetByName(string name)
        {
            return _service.Get(name);
        }

        // GET api/<SubjectController>/GetById/5
        [HttpGet]
        public SubjectDTO? GetById(int id)
        {
            return _service.Get(id);
        }

        // GET api/<SubjectController>/GetPage/?pageNum=5&pageLength=5
        [HttpGet]
        public List<SubjectDTO> GetPage(int pageNum, int pageLength)
        {
            return _service.Get(pageNum, pageLength);
        }

        // GET api/<SubjectController>/GetPageByName/?pageNum=5&pageLength=5&name=name
        [HttpGet]
        public List<SubjectDTO> GetPageByName(int pageNum, int pageLength, string name)
        {
            return _service.Get(pageNum, pageLength, name);
        }

        // POST api/<SubjectController>/Post
        [HttpPost]
        public ActionStatusDTO Post(SubjectDTO subject)
        {
            return _service.Post(subject);
        }

        // POST api/<SubjectController>/Put
        [HttpPut]
        public ActionStatusDTO Put(SubjectDTO subject)
        {
            return _service.Put(subject);
        }

        // POST api/<SubjectController>/Delete
        [HttpDelete("{id}")]
        public ActionStatusDTO Delete(int id)
        {
            return _service.Delete(id);
        }
    }
}
