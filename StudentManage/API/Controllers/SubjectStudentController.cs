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
        /// Get subject student by subjectId and studentId
        /// </summary>
        /// <param name="subjectId">The ID of the subject</param>
        /// <param name="studentId">The ID of the student</param>
        /// <response code="200">Success: Get subject student</response>
        /// <response code="400">Bad Request: Invalid subjectId or studentId</response>
        /// <response code="404">Not Found: Subject student not found</response>
        [HttpGet]
        public ActionResult<List<SubjectStudentDTO>> Get()
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
        /// <param name="subjectId">The unique identifier for the subject (greater than 0).</param>
        /// <param name="studentId">The unique identifier for the student (greater than 0).</param>
        /// <response code="200">Success: Returns the subject student information.</response>
        /// <response code="400">Bad Request: If SubjectId or StudentId is not greater than 0.</response>
        /// <response code="404">Not Found: If no data is found for the specified SubjectId and StudentId.</response>
        [HttpGet]
        public ActionResult<SubjectStudentDTO> GetById(int subjectId, int studentId)
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
        /// <param name="id">The unique identifier for the student (greater than 0).</param>
        /// <response code="200">Success: Returns the subject student information for the specified student ID.</response>
        /// <response code="400">Bad Request: If the Student ID is not greater than 0.</response>
        /// <response code="404">Not Found: If the student or student scores are not found for the specified Student ID.</response>
        [HttpGet]
        public ActionResult<List<SubjectStudentDTO>> GetByStudent(int id)
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
        /// <param name="id">The unique identifier for the subject (greater than 0).</param>
        /// <response code="200">Success: Returns the subject student information for the specified subject.</response>
        /// <response code="400">Bad Request: If SubjectId is not greater than 0.</response>
        /// <response code="404">Not Found: If the subject or student scores are not found for the specified SubjectId.</response>
        [HttpGet]
        public ActionResult<List<SubjectStudentDTO>> GetBySubject(int id)
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
        /// Retrieve and paginate subject student information.
        /// </summary>
        /// <param name="pageNum">The page number (greater than or equal to 1).</param>
        /// <param name="pageLength">The length of the page (a positive value).</param>
        /// <response code="200">Success: Returns a paginated list of subject student information.</response>
        /// <response code="400">Bad Request: If pageNum is less than 1 or pageLength is not a positive value.</response>
        /// <response code="404">Not Found: If no student scores are found for the specified page number and length.</response>
        [HttpGet]
        public ActionResult<List<SubjectStudentDTO>> GetPage(int pageNum, int pageLength)
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
        /// Create subject student.
        /// </summary>
        /// <param name="subject">The subject student information to be created.</param>
        /// <response code="200">Success: Returns the created subject student information.</response>
        /// <response code="400">Bad Request: If validation fails, it returns detailed error messages.</response>
        /// <response code="404">Not Found: If the specified SubjectId or StudentId does not exist.</response>
        [HttpPost]
        public ActionResult<SubjectStudentDTO> Post(SubjectStudentDTO subject)
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

            ActionStatusDTO result = _service.Post(subject);
            subject.SubjectId = result.objectIds[0];
            subject.StudentId = result.objectIds[1];
            return Created("", subject);
        }

        // PUT api/<SubjectStudentController>/Put
        /// <summary>
        /// Update subject student 
        /// </summary>
        /// <param name="subject">The subject student information to be updated.</param>
        /// <response code="200">Success: No content.</response>
        /// <response code="400">Bad Request: If validation fails, it returns detailed error messages.</response>
        /// <response code="404">Not Found: If the specified SubjectId or StudentId does not exist.</response>
        [HttpPut]
        public ActionResult Put(SubjectStudentDTO subject)
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
            _service.Put(subject);
            return NoContent();
        }

        // DELETE api/<SubjectStudentController>/Delete
        /// <summary>
        /// Delete subject student
        /// </summary>
        /// <param name="subjectId">The ID of the subject associated with the student.</param>
        /// <param name="studentId">The ID of the student associated with the subject.</param>
        /// <response code="200">Success: No content.</response>
        /// <response code="400">Bad Request: If SubjectId or StudentId is less than or equal to 0.</response>
        /// <response code="404">Not Found: If the specified SubjectId and StudentId combination does not exist.</response>
        [HttpDelete]
        public ActionResult Delete(int subjectId, int studentId)
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
            _service.Delete(subjectId, studentId);
            return NoContent();
        }
    }
}
