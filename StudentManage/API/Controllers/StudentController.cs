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
        [HttpGet]
        public List<StudentDTO> Get()
        {
            return _service.Get();
        }

        // GET api/<StudentController>/GetByName/?name=name
        [HttpGet]
        public List<StudentDTO> GetByName(string name)
        {
            return _service.Get(name);
        }

        // GET api/<StudentController>/GetById/5
        [HttpGet]
        public StudentDTO? GetById(int id)
        {
            return _service.Get(id);
        }

        // GET api/<StudentController>/GetPage/?pageNum=5&pageLength=5
        [HttpGet]
        public List<StudentDTO> GetPage(int pageNum, int pageLength)
        {
            return _service.Get(pageNum, pageLength);
        }

        // GET api/<StudentController>/GetPageByName/?pageNum=5&pageLength=5&name=name
        [HttpGet]
        public List<StudentDTO> GetPageByName(int pageNum, int pageLength, string name)
        {
            return _service.Get(pageNum, pageLength, name);
        }

        // POST api/<StudentController>/Post
        [HttpPost]
        public void Post([FromBody]StudentDTO student)
        {
            _service.Post(student);
        }

        // POST api/<StudentController>/Put
        [HttpPut]
        public void Put([FromBody]StudentDTO student)
        {
            _service.Put(student);
        }

        // POST api/<StudentController>/Delete
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _service.Delete(id);
        }
    }
}
