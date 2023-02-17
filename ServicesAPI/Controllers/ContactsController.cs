using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServicesAPI.BusinessLogic.Contracts;
using ServicesAPI.Models.Contacts;
using System.Net;

namespace ServicesAPI.Controllers
{
    [Route("contacts")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly IContact _contact;

        public ContactsController(IContact contact)
        {
            _contact = contact;
        }

        [HttpGet]
        [ProducesResponseType(typeof(Contacts), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Contacts>> GetAsync()
        {
            try
            {
                return Ok(await _contact.GetContactAsync());
            }
            catch
            {
                return Ok(await _contact.AddContactAsync());
            }
        }

        [HttpPut]
        public async Task<ActionResult<Contacts>> PutAsync(Contacts cont)
        {
            try
            {
                return Ok(await _contact.UpdateContactAsync(cont));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
