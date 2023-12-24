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
        /// <summary>
        /// Get all students 
        /// </summary>
        /// <response code="200">Success: Get all students</response>
        [HttpGet]
        public ActionResult<List<StudentDTO>> Get()
        {
            if (!_service.Get().Any())
            {
                return NotFound("The student list is empty!");
            }
            return Ok(_service.Get());
        }


        // GET api/<StudentController>/GetByName/?name=name
        /// <summary>
        /// Get students by name
        /// </summary>
        /// <response code="200">Success: Get students by name</response>
        [HttpGet]
        public ActionResult<List<StudentDTO>> GetByName(string name)
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
        /// <summary>
        /// Get student by id
        /// </summary>
        /// <response code="200">Success: Get student by id</response>
        [HttpGet]
        public ActionResult<List<StudentDTO>> GetById(int id)
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
        /// <summary>
        /// Get and page students
        /// </summary>
        /// <response code="200">Success: Get and page students</response>
        [HttpGet]
        public ActionResult<List<StudentDTO>> GetPage(int pageNum, int pageLength)
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
        /// <summary>
        /// Get and page student by name
        /// </summary>
        /// <response code="200">Success: Get and page student by name</response>
        [HttpGet]
        public ActionResult<List<StudentDTO>> GetPageByName(int pageNum, int pageLength, string name)
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
        /// <summary>
        /// Create a student
        /// </summary>
        /// <response code="200">Success: Create a student</response>
        [HttpPost]
        public ActionResult<StudentDTO> Post([FromBody] StudentDTO student)
        {
            if (student.Id > 0)
            {
                return BadRequest("Student Id can not be changed.");
            }
            if (string.IsNullOrEmpty(student.FullName))
            {
                return BadRequest("Student requires Name");
            }
            ActionStatusDTO result = _service.Post(student);
            student.Id = result.objectIds[0];
            return Created("", student);
        }

        // PUT api/<StudentController>/Put
        /// <summary>
        /// Update a student
        /// </summary>
        /// <response code="200">Success: Update a student</response>
        [HttpPut]
        public ActionResult Put([FromBody] StudentDTO student)
        {
            if (string.IsNullOrEmpty(student.FullName))
            {
                return BadRequest("Student requires Name");
            }
            _service.Put(student);
            return NoContent();
        }

        // DELETE api/<StudentController>/Delete
        /// <summary>
        /// Delete a student
        /// </summary>
        /// <response code="200">Success: Delete a student</response>
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
