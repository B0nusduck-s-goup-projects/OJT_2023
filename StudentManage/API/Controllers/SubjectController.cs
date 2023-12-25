using BusinessLayer.DTO;
using BusinessLayer.Service;
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
        /// <summary>
        /// Get subjects
        /// </summary>
        /// <response code="200">Success: Get subjects</response>
        /// <response code="404">Not Found: The subject list is empty!</response>
        [HttpGet]
        public ActionResult<List<SubjectDTO>> Get()
        {
            if (!_service.Get().Any())
            {
                return NotFound("The subject list is empty!");
            }
            return Ok(_service.Get());
        }

        // GET api/<SubjectController>/GetByName/?name=name
        /// <summary>
        /// Get subjects by name
        /// </summary>
        /// <param name="name">The name of the subject to retrieve.</param>
        /// <response code="200">Success: Get subjects by name</response>
        /// <response code="400">Bad Request: Subject name cannot be empty.</response>
        /// <response code="404">Not Found: There is no subject with the name: {name}</response>
        [HttpGet]
        public ActionResult<List<SubjectDTO>> GetByName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return BadRequest("Subject name cannot be empty.");
        }
            List<SubjectDTO> subjects = _service.Get(name);
            if (subjects == null || subjects.Count == 0)
            {
                return NotFound("There is no subject with the name: " + name);
            }

            return Ok(subjects);
        }

        // GET api/<SubjectController>/GetById/5
        /// <summary>
        /// Get subjects by id
        /// <param name="id">The ID of the subject to retrieve.</param>
        /// <response code="200">Success: Get subjects by id</response>
        /// <response code="400">Bad Request: Subject ID must be greater than 0.</response>
        /// <response code="404">Not Found: There is no subject with ID: {id}</response>
        [HttpGet]
        public ActionResult<SubjectDTO> GetById(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Subject ID must be greater than 0.");
            }
            SubjectDTO? subjects = _service.Get(id);
            if (subjects == null)
            {
                return NotFound("There is no subject ID: " + id);
            }

            return Ok(subjects);
        }


        // GET api/<SubjectController>/GetPage/?pageNum=5&pageLength=5
        /// <summary>
        /// Get and page subjects
        /// </summary>
        /// <param name="pageNum">The page number to retrieve.</param>
        /// <param name="pageLength">The length of the page.</param>
        /// <response code="200">Success: Get and page subjects</response>
        /// <response code="400">Bad Request: Page number must be greater than or equal to 1. Page length must be a positive value.</response>
        /// <response code="404">Not Found: There is no subjects from the page number: {pageNum}, length: {pageLength}</response>
        [HttpGet]
        public ActionResult<List<SubjectDTO>> GetPage(int pageNum, int pageLength)
        {
            if (pageNum < 0)
            {
                return BadRequest("Page number must be greater than or equal to 1.");
            }
            if (pageLength <= 0)
            {
                return BadRequest("Page length must be a positive value.");
            }
            List<SubjectDTO> subjects = _service.Get(pageNum, pageLength);
            if (subjects == null || subjects.Count == 0)
            {
                return NotFound("There is no subjects from the page number: " + pageNum + ", length: " + pageLength);
            }
            return Ok(subjects);
        }

        // GET api/<SubjectController>/GetPageByName/?pageNum=5&pageLength=5&name=name
        /// <summary>
        /// Get and page subjects by name 
        /// </summary>
        /// <param name="pageNum">The page number to retrieve.</param>
        /// <param name="pageLength">The length of the page.</param>
        /// <param name="name">The name of the subject to filter by.</param>
        /// <response code="200">Success: Get and page subjects by name</response>
        /// <response code="400">Bad Request: Page number must be greater than or equal to 1. Page length must be a positive value.</response>
        /// <response code="404">Not Found: There is no subjects from the page number: {pageNum}, length: {pageLength} or with the name: {name}</response>
        [HttpGet]
        public ActionResult<List<SubjectDTO>> GetPageByName(int pageNum, int pageLength, string name)
        {
            if (pageNum < 0)
            {
                return BadRequest("Page number must be greater than or equal to 1.");
            }
            if (pageLength <= 0)
            {
                return BadRequest("Page length must be a positive value.");
            }
            List<SubjectDTO> subjects = _service.Get(pageNum, pageLength, name);
            if (subjects == null || subjects.Count == 0)
            {
                return NotFound("There is no subjects from the page number: " + pageNum + ", length: " + pageLength + " or with the name: " + name);
            }
            return Ok(subjects);
        }

        // POST api/<SubjectController>/Post
        /// <summary>
        /// Create subject 
        /// </summary>
        /// <param name="subject">The subject information to create.</param>
        /// <response code="200">Success: Create subject</response>
        /// <response code="400">Bad Request: Subject Id can not be changed. Subject requires Name.</response>
        [HttpPost]
        public ActionResult<SubjectDTO> Post(SubjectDTO subject)
        {
            if (subject.Id > 0)
            {
                return BadRequest("subject Id can not be changed.");
            }
            if (string.IsNullOrEmpty(subject.Name))
            {
                return BadRequest("subject requires Name");
            }
            ActionStatusDTO result = _service.Post(subject);
            subject.Id = result.objectIds[0];
            return Created("", subject);
        }

        // PUT api/<SubjectController>/Put
        /// <summary>
        /// Update subject 
        /// </summary>
        /// <param name="subject">The subject information to update.</param>
        /// <response code="200">Success: Update subject</response>
        /// <response code="400">Bad Request: Subject requires Name.</response>
        /// <response code="404">Not Found: Subject not found.</response>
        [HttpPut]
        public ActionResult Put(SubjectDTO subject)
        {
            if (string.IsNullOrEmpty(subject.Name))
            {
                return BadRequest("subject requires Name");
            }
            _service.Put(subject);
            return NoContent();
        }

        // DELETE api/<SubjectController>/Delete
        /// <summary>
        /// Delete subject 
        /// </summary>
        /// <param name="id">The ID of the subject to delete.</param>
        /// <response code="200">Success: Delete subject</response>
        /// <response code="400">Bad Request: ID cannot be empty.</response>
        /// <response code="404">Not Found: Subject not found.</response>
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
