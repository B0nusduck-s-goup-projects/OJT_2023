using BusinessLayer.DTO;
using BusinessLayer.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SubjectStudentController : Controller
    {
        private readonly ISubjectStudentService _service;
        private readonly IStudentService _StudentService;
        private readonly ISubjectService _SubjectService;
        public SubjectStudentController(ISubjectStudentService service, IStudentService studentService, ISubjectService _subjectService)
        {
            _service = service;
            _StudentService = studentService;
            _SubjectService = _subjectService;
        }

        // GET api/<SubjectStudentController>/Get/
        /// <summary>
        /// Get subject student
        /// </summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Get()
        {
            if (!_service.Get().Any())
            {
                return NotFound("The subject list is empty!");
            }
            return Ok(_service.Get());
        }

        // GET api/<SubjectStudentController>/GetById/?subjectId=5&studentId=5
        /// <summary>
        /// Get subject student
        /// </summary>
        /// <response code="200">Success: Get subject student</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetById(int subjectId, int studentId)
        {
            if (subjectId <= 0)
            {
                return BadRequest("SubjectId must be greater than 0.");
            }
            if (studentId <= 0)
            {
                return BadRequest("StudentId must be greater than 0.");
            }
            var result = _service.Get(subjectId, studentId);
            if (result == null)
            {
                return NotFound("No data found for the specified SubjectId and StudentId.");
            }
            return Ok(result);
        }

        // GET api/<SubjectStudentController>/GetByStudent/5
        /// <summary>
        /// Get subject student by student id
        /// </summary>
        /// <response code="200">Success: Get subject student by student id</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetByStudent(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Student ID must be greater than 0.");
            }
            var studentData = _StudentService.GetByStudent(id);
            if (studentData == null)
            {
                return NotFound("Student with ID " + id + " not found.");
            }
            var result = _service.GetByStudent(id);
            if (result == null || result.Count <= 0)
            {
                return NotFound("Student with ID " + id + " exists, but student score are not found.");
            }

            return Ok(_service.GetByStudent(id));
        }

        // GET api/<SubjectStudentController>/GetBySubject/5
        /// <summary>
        /// Get subject student by subject id
        /// </summary>
        /// <response code="200">Success: Get subject student by subject id</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetBySubject(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Subject ID must be greater than 0.");
            }
            var studentData = _SubjectService.Get(id);
            if (studentData == null)
            {
                return NotFound("Subject with ID " + id + " not found.");
            }
            var result = _service.GetBySubject(id);
            if (result == null || result.Count <= 0)
            {
                return NotFound("There are no score of any student learning subject: " + id);
            }

            return Ok(_service.GetBySubject(id));
        }

        // GET api/<SubjectStudentController>/GetPage/?pageNum=5&pageLength=5
        /// <summary>
        /// Get and page subject student
        /// </summary>
        /// <response code="200">Success: Get and page subject student</response>
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
            List<SubjectStudentDTO> page = _service.GetPage(pageNum, pageLength);

            if (page == null || page.Count == 0)
            {
                return NotFound("There is no student score from the page number: " + pageNum + ", length: " + pageLength);
            }
            return Ok(page);
        }

        // POST api/<SubjectStudentController>/Post
        /// <summary>
        /// Create subject student
        /// </summary>
        /// <response code="200">Success: Create subject student</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Post(SubjectStudentDTO subject)
        {
            var errors400 = new List<string>();
            var errors404 = new List<string>();

            // Validate subjectId
            if (subject.SubjectId <= 0)
            {
                errors400.Add("SubjectId must be greater than 0");
            }
            var subjectid = _SubjectService.Get(subject.SubjectId);
            if (subjectid == null)
            {
                errors404.Add("Subject with " + subjectid + " SubjectId does not exist");
            }

            // Validate studentId
            if (subject.StudentId <= 0)
            {
                errors400.Add("StudentId must be greater than 0");
            }
            var studentid = _StudentService.GetByStudent(subject.StudentId);
            if (studentid == null)
            {
                errors404.Add("Student with " + studentid + " StudentId does not exist.");
            }

            // Validate student grades (assuming 0-100 range)
            if (subject.StudentGrade1 < 0 || subject.StudentGrade1 > 10)
            {
                errors400.Add("StudentGrade1 must be between 0 and 10");
            }
            if (subject.StudentGrade2 < 0 || subject.StudentGrade2 > 10)
            {
                errors400.Add("StudentGrade2 must be between 0 and 10");
            }

            if (errors400.Any())
            {
                return BadRequest(string.Join(", ", errors400));
            }
            if (errors404.Any())
            {
                return NotFound(string.Join(", ", errors404));
            }

            return Ok(_service.Post(subject));
        }

        // PUT api/<SubjectStudentController>/Put
        /// <summary>
        /// Update subject student 
        /// </summary>
        /// <response code="200">Success: Update subject student</response>
        [HttpPut]
        public IActionResult Put(SubjectStudentDTO subject)
        {
            var errors400 = new List<string>();
            var errors404 = new List<string>();

            // Validate subjectId
            if (subject.SubjectId <= 0)
            {
                errors400.Add("SubjectId must be greater than 0");
            }
            var subjectid = _SubjectService.Get(subject.SubjectId);
            if (subjectid == null)
            {
                errors404.Add("Subject with " + subjectid + " SubjectId does not exist");
            }

            // Validate studentId
            if (subject.StudentId <= 0)
            {
                errors400.Add("StudentId must be greater than 0");
            }
            var studentid = _StudentService.GetByStudent(subject.StudentId);
            if (studentid == null)
            {
                errors404.Add("Student with " + studentid + " StudentId does not exist.");
            }

            // Validate student grades (assuming 0-100 range)
            if (subject.StudentGrade1 < 0 || subject.StudentGrade1 > 10)
            {
                errors400.Add("StudentGrade1 must be between 0 and 10");
            }
            if (subject.StudentGrade2 < 0 || subject.StudentGrade2 > 10)
            {
                errors400.Add("StudentGrade2 must be between 0 and 10");
            }

            if (errors400.Any())
            {
                return BadRequest(string.Join(", ", errors400));
            }
            if (errors404.Any())
            {
                return NotFound(string.Join(", ", errors404));
            }
            return Ok(_service.Put(subject));
        }

        // DELETE api/<SubjectStudentController>/Delete
        /// <summary>
        /// Delete subject student
        /// </summary>
        /// <response code="200">Success: Delete subject student</response>
        [HttpDelete]
        public IActionResult Delete(int subjectId, int studentId)
        {
            var delete404 = _service.Delete(subjectId, studentId);
            if (subjectId <= 0)
            {
                return BadRequest("SubjectId must be greater than 0.");
            }
            else if (studentId <= 0)
            {
                return BadRequest("StudentId must be greater than 0.");
            }
            else if (delete404 == null)
            {
                return NotFound("Subject-Student data with SubjectId " + subjectId + " and StudentId " + studentId + " not found.");
            }
            return Ok(_service.Delete(subjectId, studentId));
        }
    }
}
