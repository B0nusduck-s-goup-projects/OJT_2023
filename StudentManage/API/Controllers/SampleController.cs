using BusinessLayer.Service.Interface;
using Microsoft.AspNetCore.Mvc;
//this libary is for returning the result of the sample function and can be safely remove along with the sample
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    //this is a sample controller used to conveniently creating new controller
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SampleController : ControllerBase
    {
        private readonly IService _service;
        
        public SampleController(IService service)
        {
            _service = service;
        }

        // GET: api/<SampleController>
        [HttpGet]
        public DbContextId /*IEnumerable<string>*/ Get()
        {
            /*return new string[] { "value1", "value2" };*/
            return _service.sample();
        }

        // GET api/<SampleController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<SampleController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<SampleController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<SampleController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
