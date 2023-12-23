using BusinessLayer.DTO;
using BusinessLayer.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class StudentContactController : Controller
    {
        private readonly IStudentContactService _service;
        private readonly IStudentService _StudentService;
        public StudentContactController(IStudentContactService service, IStudentService studentService)
        {
            _service = service;
            _StudentService = studentService;

        }

        // GET api/<StudentContactController>/Get/
        /// <summary>
        /// Get student contacts
        /// </summary>
        /// <response code="200">Success: Get student contacts</response>
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

        // GET api/<StudentContactController>/GetByUser/?name=name
        /// <summary>
        /// Get student contacts by student id
        /// </summary>
        /// <response code="200">Success: Get student contacts by student id</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetByUser(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Student user ID must be greater than 0.");
            }
            StudentContactDTO? students = _service.GetByUser(id);
            if (students == null)
            {
                return NotFound("There is no student with the user ID: " + id);
            }
            return Ok(students);
        }

        // GET api/<StudentContactController>/GetById/5
        /// <summary>
        /// Get student contacts by id
        /// </summary>
        /// <response code="200">Success: Get student contacts by id</response>
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
            StudentContactDTO? students = _service.Get(id);
            if (students == null)
            {
                return NotFound("There is no student with id: " + id);
            }
            return Ok(_service.Get(id));
        }

        // GET api/<StudentContactController>/GetPage/?pageNum=5&pageLength=5
        /// <summary>
        /// Get and page student contacts
        /// </summary>
        /// <response code="200">Success: Get and page student contacts</response>
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

            List<StudentContactDTO> students = _service.Get(pageNum, pageLength);
            if (students == null || students.Count == 0)
            {
                return NotFound("There is no student from the page number: " + pageNum + ", length: " + pageLength);
            }
            return Ok(students);
        }


        // POST api/<StudentContactController>/Post
        /// <summary>
        /// Create student contact
        /// </summary>
        /// <response code="200">Success: Create student contact</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Post([FromBody] StudentContactDTO studentContact)
        {
            if (studentContact.Id > 0)
            {
                return BadRequest("Student Id can not be changed.");
            }
            if (studentContact.UserId <= 0)
            {
                return BadRequest("Student requires a positive UserId number.");
            }
            var userid = _StudentService.GetByStudent(studentContact.UserId);
            if (userid == null)
            {
                return NotFound("Student with ID " + studentContact.UserId + " does not exist.");
            }
            if (!string.IsNullOrEmpty(studentContact.Phone) && !Regex.IsMatch(studentContact.Phone, @"^\d+$"))
            {
                return BadRequest("Phone number must contain only digits.");
            }
            if (!string.IsNullOrEmpty(studentContact.Email) && !studentContact.Email.EndsWith("@gmail.com"))
            {
                return BadRequest("Email address must end with @gmail.com.");
            }

            return Ok(_service.Post(studentContact));
        }


        // PUT api/<StudentContactController>/Put
        /// <summary>
        /// Update student contact
        /// </summary>
        /// <response code="200">Success: Update student contact</response>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Put([FromBody] StudentContactDTO studentContact)
        {
            if (studentContact.Id > 0)
            {
                return BadRequest("Student Id can not be changed.");
            }
            if (studentContact.UserId <= 0)
            {
                return BadRequest("Student requires a positive UserId number.");
            }
            var userid = _StudentService.GetByStudent(studentContact.UserId);
            if (userid == null)
            {
                return NotFound("Student with ID " + studentContact.UserId + " does not exist.");
            }
            if (!string.IsNullOrEmpty(studentContact.Phone) && !Regex.IsMatch(studentContact.Phone, @"^\d+$"))
            {
                return BadRequest("Phone number must contain only digits.");
            }
            if (!string.IsNullOrEmpty(studentContact.Email) && !studentContact.Email.EndsWith("@gmail.com"))
            {
                return BadRequest("Email address must end with @gmail.com.");
            }
            return Ok(_service.Put(studentContact));
        }

        // DELETE api/<StudentContactController>/Delete
        /// <summary>
        /// Delete student contact
        /// </summary>
        /// <response code="200">Success: Delete student contact</response>
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
