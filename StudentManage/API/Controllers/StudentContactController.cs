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
        [HttpGet]
        public List<StudentContactDTO> Get()
        {
            return _service.Get();
        }

        // GET api/<StudentContactController>/GetByUser/?name=name
        [HttpGet]
        public StudentContactDTO? GetByUser(int id)
        {
            return _service.GetByUser(id);
        }

        // GET api/<StudentContactController>/GetById/5
        [HttpGet]
        public StudentContactDTO? GetById(int id)
        {
            return _service.Get(id);
        }

        // GET api/<StudentContactController>/GetPage/?pageNum=5&pageLength=5
        [HttpGet]
        public List<StudentContactDTO> GetPage(int pageNum, int pageLength)
        {
            return _service.Get(pageNum, pageLength);
        }

        // POST api/<StudentContactController>/Post
        [HttpPost]
        public void Post([FromBody]StudentContactDTO studentContact)
        {
            _service.Post(studentContact);
        }

        // POST api/<StudentContactController>/Put
        [HttpPut]
        public void Put([FromBody]StudentContactDTO studentContact)
        {
            _service.Put(studentContact);
        }

        // POST api/<StudentContactController>/Delete
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _service.Delete(id);
        }
    }
}
