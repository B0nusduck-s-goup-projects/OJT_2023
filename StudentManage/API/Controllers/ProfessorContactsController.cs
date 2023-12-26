using BusinessLayer.DTO;
using BusinessLayer.Service;
using BusinessLayer.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProfessorContactsController : Controller
    {
        private readonly IProfessorContactService _professorContactservice;
        private readonly IProfessorService _professorService;
        public ProfessorContactsController(IProfessorContactService professorContactservice, IProfessorService professorService)
        {
            _professorContactservice = professorContactservice;
            _professorService = professorService;
        }

        // GET api/<ProfessorContactController>/Get/
        /// <summary>
        /// Get professor contacts
        /// </summary>
        /// <response code="200">Success: Get professor contacts</response>
        /// <response code="404">Not found: The contact list is empty!</response>
        [HttpGet]
        public ActionResult<List<ProfessorContactDTO>> Get()
        {
            if (!_professorContactservice.Get().Any())
            {
                return NotFound("The contact list is empty!");
            }
            return Ok(_professorContactservice.Get());
        }

        // GET api/<ProfessorContactController>/GetByUser/5
        /// <summary>
        /// Get professor contacts by professor id
        /// </summary>
        /// <response code="200">Success: Get professor contacts by professor id</response>
        /// <response code="400">Bad Request: User ID must be greater than 0.</response>
        /// <response code="404">Not Found: There is no professor with user ID.</response>
        [HttpGet]
        public ActionResult<ProfessorContactDTO> GetByUser(int id)
        {
            if (id <= 0)
            {
                return BadRequest("User ID must be greater than 0.");
            }

            ProfessorContactDTO? professor = _professorContactservice.GetByUser(id);
            if (professor == null)
            {
                return NotFound("There is no professor with user ID: " + id);
            }

            return Ok(_professorContactservice.GetByUser(id));
        }

        // GET api/<ProfessorContactController>/GetById/5
        /// <summary>
        /// Get professor contacts by id
        /// </summary>
        /// <response code="200">Success: Get professor contacts by id</response>
        /// <response code="400">Bad Request: User ID must be greater than 0.</response>
        /// <response code="404">Not Found: There is no professor with ID.</response>
        [HttpGet]
        public ActionResult<ProfessorContactDTO> GetById(int id)
        {
            if (id <= 0)
            {
                return BadRequest("User ID must be greater than 0.");
            }

            ProfessorContactDTO? professor = _professorContactservice.Get(id);
            if (professor == null)
            {
                return NotFound("There is no professor with id: " + id);
            }

            return Ok(_professorContactservice.Get(id));
        }

        // GET api/<ProfessorContactController>/GetPage/?pageNum=5&pageLength=5
        /// <summary>
        /// Get and page professor contacts
        /// </summary>
        /// <param name="pageNum">The page number (starting from 1).</param>
        /// <param name="pageLength">The number of items per page (positive value).</param>
        /// <response code="200">Success: Get and page professor contacts</response>
        /// <response code="400">Bad Request: Page number must be greater than or equal to 1, or page length must be a positive value.</response>
        /// <response code="404">Not Found: There is no contact from the page number: {pageNum}, length: {pageLength}</response>
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

            List<ProfessorContactDTO> professors = _professorContactservice.Get(pageNum, pageLength);

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
        /// <response code="201">Created: Professor contact created successfully</response>
        /// <response code="400">Bad Request: Professor Id can not be changed, Professor requires a positive UserId number, Phone number must contain only digits, or Email address must end with @gmail.com.</response>
        /// <response code="404">Not Found: Professor with specified UserId does not exist.</response>
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
            var userid = _professorService.Get(professorContact.UserId);
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

            ActionStatusDTO result = _professorContactservice.Post(professorContact);
            professorContact.Id = result.objectIds[0];
            return Created("", professorContact);
        }

        // PUT api/<ProfessorContactController>/Put
        /// <summary>
        /// Update professor contact
        /// </summary>
        /// <response code="200">Success: Update professor contact</response>
        /// <response code="400">Bad Request: Professor Id can not be changed, Professor requires a UserId, Phone number must contain only digits, or Email address must end with @gmail.com.</response>
        /// <response code="404">Not Found: Professor with specified UserId does not exist.</response>
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
            var userid = _professorService.Get(professorContact.UserId);
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
            _professorContactservice.Put(professorContact);
            return NoContent();
        }

        // DELETE api/<ProfessorContactController>/Delete
        /// <summary>
        /// Delete professor contact
        /// </summary>
        /// <param name="id">The ID of the professor contact to delete.</param>
        /// <response code="200">Success: Delete professor contact</response>
        /// <response code="400">Bad Request: ID must be greater than 0.</response>
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            if (id == 0)
            {
                return BadRequest("Id can not be empty");
            }
            _professorContactservice.Delete(id);
            return NoContent();
        }
    }
}
