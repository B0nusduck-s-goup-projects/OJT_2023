using BusinessLayer.DTO;
using BusinessLayer.Service;
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Get()
        {
            if (!_service.Get().Any())
            {
                return NotFound("The student list is empty!");
            }
            return Ok(_service.Get());
        }

        // GET api/<StudentController>/GetByName/?name=name
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetByName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return BadRequest("Student name cannot be empty.");
            }
            List<StudentDTO> students = _service.Get(name);
            if (students == null || students.Count == 0)
            {
                return NotFound("There is no student with the name: " + name);
            }

            return Ok(students);
        }

        // GET api/<StudentController>/GetById/5
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetById(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Student ID must be greater than 0.");
            }

            StudentDTO? students = _service.GetByStudent(id);
            if (students == null)
            {
                return NotFound("There is no student with the ID: " + id);
            }

            return Ok(students);
        }

        // GET api/<StudentController>/GetPage/?pageNum=5&pageLength=5
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetPage(int pageNum, int pageLength)
        {
            if (pageNum < 0)
            {
                return BadRequest("Page number must be greater than or equal to 1.");
            }

            if (pageLength <= 0)
            {
                return BadRequest("Page length must be a positive value.");
            }

            List<StudentDTO> students = _service.Get(pageNum, pageLength);

            if (students == null || students.Count == 0)
            {
                return NotFound("There is no students from the page number: " + pageNum + ", length: " + pageLength);
            }
            return Ok(students);
        }

        // GET api/<StudentController>/GetPageByName/?pageNum=5&pageLength=5&name=name
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetPageByName(int pageNum, int pageLength, string name)
        {
            if (pageNum < 0)
            {
                return BadRequest("Page number must be greater than or equal to 1.");
            }

            if (pageLength <= 0)
            {
                return BadRequest("Page length must be a positive value.");
            }
            List<StudentDTO> students = _service.Get(pageNum, pageLength, name);

            if (students == null || students.Count == 0)
            {
                return NotFound("There is no students from the page number: " + pageNum + ", length: " + pageLength + " or with the name: " + name);
            }
            return Ok(students);
        }

        // POST api/<StudentController>/Post
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Post([FromBody]StudentDTO student)
        {
            if (student.Id > 0)
            {
                return BadRequest("Student Id can not be changed.");
            }
            return Ok(_service.Post(student));
        }

        // POST api/<StudentController>/Put
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Put([FromBody]StudentDTO student)
        {
            if (string.IsNullOrEmpty(student.FullName))
            {
                return BadRequest("Student requires Name");
            }
            return Ok(_service.Put(student));
        }

        // POST api/<StudentController>/Delete
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Delete(int id)
        {
            if (id == 0)
            {
                return BadRequest("Id can not be empty");
            }
            return Ok(_service.Delete(id));
        }
    }
}
