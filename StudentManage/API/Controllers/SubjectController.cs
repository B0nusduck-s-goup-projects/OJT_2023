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

        // GET api/<SubjectController>/GetByName/?name=name
        /// <summary>
        /// Get subjects by name
        /// </summary>
        /// <response code="200">Success: Get subjects by name</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetByName(string name)
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
        /// </summary>
        /// <response code="200">Success: Get subjects by id</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetById(int id)
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
        /// <response code="200">Success: Get and page subjects</response>
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
        /// <response code="200">Success: Get and page subjects by name</response>
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
        /// <response code="200">Success: Create subject</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Post(SubjectDTO subject)
        {
            if (subject.Id > 0)
            {
                return BadRequest("subject Id can not be changed.");
            }
            return Ok(_service.Post(subject));
        }

        // PUT api/<SubjectController>/Put
        /// <summary>
        /// Update subject 
        /// </summary>
        /// <response code="200">Success: Update subject</response>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Put(SubjectDTO subject)
        {
            return Ok(_service.Put(subject));
        }

        // DELETE api/<SubjectController>/Delete
        /// <summary>
        /// Delete subject 
        /// </summary>
        /// <response code="200">Success: Delete subject</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Delete(int id)
        {
            return Ok(_service.Delete(id));
        }
    }
}
