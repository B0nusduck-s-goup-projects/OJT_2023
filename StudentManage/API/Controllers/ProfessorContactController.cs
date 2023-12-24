using BusinessLayer.DTO;
using BusinessLayer.Service;
using BusinessLayer.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProfessorContactController : Controller
    {
        private readonly IProfessorContactService _service;
        private readonly IProfessorService _ProfService;
        public ProfessorContactController(IProfessorContactService service, IProfessorService profservice)
        {
            _service = service;
            _ProfService = profservice;
        }

        // GET api/<ProfessorContactController>/Get/
        /// <summary>
        /// Get professor contacts
        /// </summary>
        /// <response code="200">Success: Get professor contacts</response>
        [HttpGet]
        public ActionResult<List<ProfessorContactDTO>> Get()
        {
            if (!_service.Get().Any())
            {
                return NotFound("The contact list is empty!");
            }
            return Ok(_service.Get());
        }

        // GET api/<ProfessorContactController>/GetByUser/5
        /// <summary>
        /// Get professor contacts by professor id
        /// </summary>
        ///<response code="200">Success: Get professor contacts by professor id</response>
        [HttpGet]
        public ActionResult<ProfessorContactDTO> GetByUser(int id)
        {
            if (id <= 0)
            {
                return BadRequest("User ID must be greater than 0.");
            }

            ProfessorContactDTO? professor = _service.GetByUser(id);
            if (professor == null)
            {
                return NotFound("There is no professor with user ID: " + id);
            }

            return Ok(_service.GetByUser(id));
        }

        // GET api/<ProfessorContactController>/GetById/5
        /// <summary>
        /// Get professor contacts by id
        /// </summary>
        /// <response code="200">Success: Get professor contacts by id </response>
        [HttpGet]
        public ActionResult<ProfessorContactDTO> GetById(int id)
        {
            if (id <= 0)
            {
                return BadRequest("User ID must be greater than 0.");
            }

            ProfessorContactDTO? professor = _service.Get(id);
            if (professor == null)
            {
                return NotFound("There is no professor with id: " + id);
            }

            return Ok(_service.Get(id));
        }

        // GET api/<ProfessorContactController>/GetPage/?pageNum=5&pageLength=5
        /// <summary>
        /// Get and page professor contacts
        /// </summary>
        /// <response code="200">Success: Get and page professor contacts </response>
        [HttpGet]
        public ActionResult<List<ProfessorContactDTO>> GetPage(int pageNum, int pageLength)
        {
            if (pageNum < 0)
            {
                return BadRequest("Page number must be greater than or equal to 1.");
            }

            if (pageLength <= 0)
            {
                return BadRequest("Page length must be a positive value.");
            }

            List<ProfessorContactDTO> professors = _service.Get(pageNum, pageLength);

            if (professors == null || professors.Count == 0)
            {
                return NotFound("There is no contact from the page number: " + pageNum + ", length: " + pageLength);
            }
            return Ok(professors);
        }

        // POST api/<ProfessorContactController>/Post
        /// <summary>
        /// Create professor contact
        /// </summary>
        /// <response code="200">Success: Create professor contact</response>
        [HttpPost]
        public ActionResult<ProfessorContactDTO> Post([FromBody] ProfessorContactDTO professorContact)
        {
            if (professorContact.Id > 0)
            {
                return BadRequest("Professor Id can not be changed.");
            }
            if (professorContact.UserId <= 0)
            {
                return BadRequest("Professor requires a positive UserId number.");
            }
            var userid = _ProfService.Get(professorContact.UserId);
            if (userid == null)
            {
                return NotFound("Professor with ID " + professorContact.UserId + " does not exist.");
            }
            if (!string.IsNullOrEmpty(professorContact.Phone) && !Regex.IsMatch(professorContact.Phone, @"^\d+$"))
            {
                return BadRequest("Phone number must contain only digits.");
            }
            if (!string.IsNullOrEmpty(professorContact.Email) && !professorContact.Email.EndsWith("@gmail.com"))
            {
                return BadRequest("Email address must end with @gmail.com.");
            }

            ActionStatusDTO result = _service.Post(professorContact);
            professorContact.Id = result.objectIds[0];
            return Created("", professorContact);
        }

        // PUT api/<ProfessorContactController>/Put
        /// <summary>
        /// Update professor contact
        /// </summary>
        /// <response code="200">Success: Update professor contact</response>
        [HttpPut]
        public ActionResult Put([FromBody] ProfessorContactDTO professorContact)
        {
            if (professorContact.Id > 0)
            {
                return BadRequest("Professor Id can not be changed.");
            }
            if (professorContact.UserId <= 0)
            {
                return BadRequest("Professor requires a UserId.");
            }
            var userid = _ProfService.Get(professorContact.UserId);
            if (userid == null)
            {
                return NotFound("Professor with ID " + professorContact.UserId + " does not exist.");
            }
            if (!string.IsNullOrEmpty(professorContact.Phone) && !Regex.IsMatch(professorContact.Phone, @"^\d+$"))
            {
                return BadRequest("Phone number must contain only digits.");
            }
            if (!string.IsNullOrEmpty(professorContact.Email) && !professorContact.Email.EndsWith("@gmail.com"))
            {
                return BadRequest("Email address must end with @gmail.com.");
            }
            _service.Put(professorContact);
            return NoContent();
        }

        // DELETE api/<ProfessorContactController>/Delete
        /// <summary>
        /// Delete professor contact
        /// </summary>
        /// <response code="200">Success: Delete professor contact</response>
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
