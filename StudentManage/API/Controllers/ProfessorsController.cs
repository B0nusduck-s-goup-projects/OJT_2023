using BusinessLayer.DTO;
using BusinessLayer.Service;
using BusinessLayer.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProfessorsController : Controller
    {
        private readonly IProfessorService _professorService;
        private readonly ISubjectService _subjectService;
        public ProfessorsController(IProfessorService professorService, ISubjectService subjectService)
        {
            _professorService = professorService;
            _subjectService = subjectService;
        }

        // GET api/<ProfessorController>/Get/
        /// <summary>
        /// Get professors
        /// </summary>
        /// <response code="200">Success: Get professors</response>
        /// <response code="404">Not Found: The professor list is empty!</response>
        [HttpGet]
        public ActionResult<ProfessorDTO> Get()
        {
            if (!_professorService.Get().Any())
            {
                return NotFound("The professor list is empty!");
            }
            return Ok(_professorService.Get());
        }

        // GET api/<ProfessorController>/GetByName/?name=name
        /// <summary>
        /// Get professors by name
        /// </summary>
        /// <param name="name">The name of the professor.</param>
        /// <response code="200">Success: Get professors by name</response>
        /// <response code="400">Bad Request: Professor name cannot be empty.</response>
        /// <response code="404">Not Found: There is no professor with the name: {name}</response>
        [HttpGet("{name}")]
        public ActionResult<List<ProfessorDTO>> GetByName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return BadRequest("Professor name cannot be empty.");
            }
            List<ProfessorDTO> professors = _professorService.Get(name);
            if (professors == null || professors.Count == 0)
            {
                return NotFound("There is no professor with the name: " + name);
            }

            return Ok(professors);
        }

        // GET api/<ProfessorController>/GetBySubject/5
        /// <summary>
        /// Get professors by subject
        /// </summary>
        /// <param name="id">The ID of the subject.</param>
        /// <response code="200">Success: Get professors by subject</response>
        /// <response code="400">Bad Request: Subject ID must be greater than 0.</response>
        /// <response code="404">Not Found: There is no professor teaching subject with ID: {id}</response>
        [HttpGet("bysubject/{id}")]
        public ActionResult<List<ProfessorDTO>> GetBySubject(string id)
        {
            int userId;
            if (!int.TryParse(id, out userId))
            {
                return BadRequest("Invalid user ID: Please provide a valid integer value.");
            }
            if (string.IsNullOrWhiteSpace(id))
            {
                return BadRequest("User ID is required.");
            }
            List<ProfessorDTO> professor = _professorService.GetBySubject(userId);
            if (professor == null)
            {
                return NotFound("There is no professor teaching subject: " + userId);
            }

            return Ok(professor);
        }


        // GET api/<ProfessorController>/GetById/5
        /// <summary>
        /// Get professors by id
        /// </summary>
        /// <param name="id">The ID of the professor.</param>
        /// <response code="200">Success: Get professors by ID</response>
        /// <response code="400">Bad Request: Professor ID must be greater than 0.</response>
        /// <response code="404">Not Found: There is no professor with ID: {id}</response>
        [HttpGet("ProfessorId/{id}")]
        public ActionResult<ProfessorDTO?> GetById(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Professor ID must be greater than 0.");
            }

            ProfessorDTO? professor = _professorService.Get(id);
            if (professor == null)
            {
                return NotFound("There is no professor with the id: " + id);
            }

            return Ok(professor);
        }

        // GET api/<ProfessorController>/GetPage/?pageNum=5&pageLength=5
        /// <summary>
        /// Get and page professors
        /// </summary>
        /// <param name="pageNum">The page number (starting from 1).</param>
        /// <param name="pageLength">The number of items per page (positive value).</param>
        /// <response code="200">Success: Get and page professors</response>
        /// <response code="400">Bad Request: Page number must be greater than or equal to 1, or page length must be a positive value.</response>
        /// <response code="404">Not Found: There is no professors from the page number: {pageNum}, length: {pageLength}</response>
        [HttpGet]
        public ActionResult<List<ProfessorDTO>> GetPage(int pageNum, int pageLength)
        {
            if (pageNum < 0)
            {
                return BadRequest("Page number must be greater than or equal to 1.");
            }

            if (pageLength <= 0)
            {
                return BadRequest("Page length must be a positive value.");
            }

            List<ProfessorDTO> professors = _professorService.Get(pageNum, pageLength);

            if (professors == null || professors.Count == 0)
            {
                return NotFound("There is no professors from the page number: " + pageNum + ", length: " + pageLength);
            }
            return Ok(professors);
        }


        // GET api/<ProfessorController>/GetPageByName/?pageNum=5&pageLength=5&name=name
        /// <summary>
        /// Get and page professors by name
        /// </summary>
        /// <param name="pageNum">The page number (starting from 1).</param>
        /// <param name="pageLength">The number of items per page (positive value).</param>
        /// <param name="name">The name of the professor.</param>
        /// <response code="200">Success: Get and page professors by name</response>
        /// <response code="400">Bad Request: Page number must be greater than or equal to 1, page length must be a positive value.</response>
        /// <response code="404">Not Found: There is no professors from the page number: {pageNum}, length: {pageLength} or with the name: {name}</response>
        [HttpGet]
        public ActionResult<List<ProfessorDTO>> GetPageByName(int pageNum, int pageLength, string name)
        {
            if (pageNum < 0)
            {
                return BadRequest("Page number must be greater than or equal to 1.");
            }

            if (pageLength <= 0)
            {
                return BadRequest("Page length must be a positive value.");
            }
            List<ProfessorDTO> professors = _professorService.Get(pageNum, pageLength, name);

            if (professors == null || professors.Count == 0)
            {
                return NotFound("There is no professors from the page number: " + pageNum + ", length: " + pageLength + " or with the name: " + name);
            }
            return Ok(professors);
        }


        // POST api/<ProfessorController>/Post
        /// <summary>
        /// Create professor
        /// </summary>
        /// <param name="professor">The professor to be created.</param>
        /// <response code="201">Created: Professor created successfully</response>
        /// <response code="400">Bad Request: Professor Id can not be changed, Professor requires a positive SubjectId number, or Subject with specified ID does not exist.</response>
        [HttpPost]
        public ActionResult<ProfessorDTO?> Post([FromBody] ProfessorDTO professor)
        {
            if (professor.Id > 0)
            {
                return BadRequest("Professor Id can not be changed.");
            }
            if (professor.SubjectId <= 0)
            {
                return BadRequest("Professor requires a positive SubjectId number.");
            }
            var subject = _subjectService.Get(professor.SubjectId);
            if (subject == null)
            {
                return NotFound("Subject with ID " + professor.SubjectId + " does not exist.");
            }
            ActionStatusDTO result = _professorService.Post(professor);
            professor.Id = result.objectIds[0];
            return Created("",professor);
        }

        // PUT api/<ProfessorController>/Put
        /// <summary>
        /// Update professor
        /// </summary>
        /// <param name="professor">The professor information to be updated.</param>
        /// <response code="200">Success: Update professor</response>
        /// <response code="400">Bad Request: Professor requires a Name or a positive SubjectId number.</response>
        /// <response code="404">Not Found: Subject with specified ID does not exist.</response>
        [HttpPut]
            public ActionResult Put([FromBody] ProfessorDTO professor)
            {
                if (string.IsNullOrEmpty(professor.FullName))
                {
                    return BadRequest("Professor requires Name");
                }
                if (professor.SubjectId == 0)
                {
                    return BadRequest("Professor requires a SubjectId.");
                }
                var subject = _subjectService.Get(professor.SubjectId);
                if (subject == null)
                {
                    return NotFound("Subject with ID " + professor.SubjectId + " does not exist.");
                }
                _professorService.Put(professor);
                return NoContent();
            }

        // Delete api/<ProfessorController>/Delete
        /// <summary>
        /// Delete professor
        /// </summary>
        /// <param name="id">The ID of the professor to delete.</param>
        /// <response code="200">Success: Delete professor</response>
        /// <response code="400">Bad Request: ID must be greater than 0.</response>
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            if (id == 0)
            {
                return BadRequest("Id can not be empty");
            }
            _professorService.Delete(id);
            return NoContent();
        }
    }
}
