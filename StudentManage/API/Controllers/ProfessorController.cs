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
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Get()
        {
            if (!_ProfessorService.Get().Any())
            {
                return NotFound("The professor list is empty!");
            }
            return Ok(_ProfessorService.Get());
        }

        // GET api/<ProfessorController>/GetByName/?name=name
        [HttpGet("{name}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetByName(string name)
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
        [HttpGet("bysubject/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetBySubject(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Subject ID must be greater than 0.");
            }

            ProfessorDTO? professor = _ProfessorService.GetBySubject(id);
            if (professor == null )
            {
                return NotFound("There is no professor teaching subject: " + id);
            }

            return Ok(professor);
        }

        // GET api/<ProfessorController>/GetById/5
        [HttpGet("ProfessorId/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetById(int id)
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

            List<ProfessorDTO> professors = _ProfessorService.Get(pageNum, pageLength);

            if (professors == null || professors.Count == 0)
            {
                return NotFound("There is no professors from the page number: " + pageNum + ", length: " + pageLength);
            }
            return Ok(professors);
        }

        // GET api/<ProfessorController>/GetPageByName/?pageNum=5&pageLength=5&name=name
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
            List<ProfessorDTO> professors = _ProfessorService.Get(pageNum, pageLength, name);

            if (professors == null || professors.Count == 0)
            {
                return NotFound("There is no professors from the page number: " + pageNum + ", length: " + pageLength + " or with the name: " + name);
            }
            return Ok(professors);
        }

        // POST api/<ProfessorController>/Post
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Post([FromBody]ProfessorDTO professor)
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

            return Ok(_ProfessorService.Post(professor));
        }

        // POST api/<ProfessorController>/Put
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Put([FromBody]ProfessorDTO professor)
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
            return Ok(_ProfessorService.Put(professor));
        }

        // POST api/<ProfessorController>/Delete
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Delete(int id)
        {
            if (id == 0)
            {
                return BadRequest("Id can not be empty");
            }
            return Ok(_ProfessorService.Delete(id));
        }
    }
}
