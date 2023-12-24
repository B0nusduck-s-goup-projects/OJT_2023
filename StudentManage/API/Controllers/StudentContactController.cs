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
        public ActionResult<List<StudentContactDTO>> Get()
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
        public ActionResult<StudentContactDTO> GetByUser(int id)
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
        public ActionResult<StudentContactDTO> GetById(int id)
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
        public ActionResult<List<StudentContactDTO>> GetPage(int pageNum, int pageLength)
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
        public ActionResult<StudentContactDTO> Post([FromBody] StudentContactDTO studentContact)
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

            ActionStatusDTO result = _service.Post(studentContact);
            studentContact.Id = result.objectIds[0];
            return Created("", studentContact);
        }


        // PUT api/<StudentContactController>/Put
        /// <summary>
        /// Update student contact
        /// </summary>
        /// <response code="200">Success: Update student contact</response>
        [HttpPut]
        public ActionResult Put([FromBody] StudentContactDTO studentContact)
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
            _service.Put(studentContact);
            return NoContent();
        }

        // DELETE api/<StudentContactController>/Delete
        /// <summary>
        /// Delete student contact
        /// </summary>
        /// <response code="200">Success: Delete student contact</response>
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            if (id == 0)
            {
                return BadRequest("Id can not be empty");
            }
            _service.Delete(id);
            return NoContent();
        }
    }
}
