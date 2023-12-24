using BusinessLayer.DTO;
using BusinessLayer.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProfessorController : Controller
    {
        private readonly IProfessorService _ProfessorService;
        private readonly ISubjectService _SubjectService;
        public ProfessorController(IProfessorService service, ISubjectService subjectService)
        {
            _ProfessorService = service;
            _SubjectService = subjectService;
        }

        // GET api/<ProfessorController>/Get/
        /// <summary>
        /// Get professors
        /// </summary>
        /// <response code="200">Success: Get professors</response>
        [HttpGet]
        public ActionResult<ProfessorDTO> Get()
        {
            if (!_ProfessorService.Get().Any())
            {
                return NotFound("The professor list is empty!");
            }
            return Ok(_ProfessorService.Get());
        }

        // GET api/<ProfessorController>/GetByName/?name=name
        /// <summary>
        /// Get professors by name
        /// </summary>
        /// <response code="200">Success: Get professors by name</response>
        /// 
        [HttpGet("{name}")]
        public ActionResult<List<ProfessorDTO>> GetByName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return BadRequest("Professor name cannot be empty.");
            }
            List<ProfessorDTO> professors = _ProfessorService.Get(name);
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
        /// <response code="200">Success: Get professors by subject</response>
        [HttpGet("bysubject/{id}")]
        public ActionResult<List<ProfessorDTO>> GetBySubject(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Subject ID must be greater than 0.");
            }

            List<ProfessorDTO> professor = _ProfessorService.GetBySubject(id);
            if (professor == null)
            {
                return NotFound("There is no professor teaching subject: " + id);
            }

            return Ok(professor);
        }


        // GET api/<ProfessorController>/GetById/5
        /// <summary>
        /// Get professors by id
        /// </summary>
        /// <response code="200">Success: Get professors by id</response>
        [HttpGet("ProfessorId/{id}")]
        public ActionResult<ProfessorDTO?> GetById(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Professor ID must be greater than 0.");
            }

            ProfessorDTO? professor = _ProfessorService.Get(id);
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
        /// <response code="200">Success: Get and page professors</response>
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

            List<ProfessorDTO> professors = _ProfessorService.Get(pageNum, pageLength);

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
        /// <response code="200">Success: Get and page professors by name</response>
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
            List<ProfessorDTO> professors = _ProfessorService.Get(pageNum, pageLength, name);

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
        /// <response code="200">Success: Create professor</response>
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
            var subject = _SubjectService.Get(professor.SubjectId);
            if (subject == null)
            {
                return NotFound("Subject with ID " + professor.SubjectId + " does not exist.");
            }
            ActionStatusDTO result = _ProfessorService.Post(professor);
            professor.Id = result.objectIds[0];
            return Created("",professor);
        }

        // PUT api/<ProfessorController>/Put
        /// <summary>
        /// Update professor
        /// </summary>
        /// <response code="200">Success: Update professor</response>
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
            var subject = _SubjectService.Get(professor.SubjectId);
            if (subject == null)
            {
                return NotFound("Subject with ID " + professor.SubjectId + " does not exist.");
            }
            _ProfessorService.Put(professor);
            return NoContent();
        }

        // Delete api/<ProfessorController>/Delete
        /// <summary>
        /// Delete professor
        /// </summary>
        /// <response code="200">Success: Delete professor</response>
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            if (id == 0)
            {
                return BadRequest("Id can not be empty");
            }
            _ProfessorService.Delete(id);
            return NoContent();
        }
    }
}
