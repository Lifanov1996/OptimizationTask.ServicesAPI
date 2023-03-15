using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServicesAPI.BusinessLogic.Contracts;
using ServicesAPI.Models.Contacts;
using System.Net;

namespace ServicesAPI.Controllers.ContactCont
{
    [Route("contactchange")]
    [ApiController]
    public class ContactChangeController : ControllerBase
    {
        private readonly IContact _contact;
        private readonly ILogger<ContactChangeController> _logger;

        public ContactChangeController(IContact contact, ILogger<ContactChangeController> logger)
        {
            _contact = contact;
            _logger = logger;
            _logger.LogInformation("Init ContactChangeController");
        }


        /// <summary>
        /// Редактировать контактной информации
        /// </summary>
        /// <param name="projects"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(typeof(Contacts), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Contacts>> ChangeContactAsync(Contacts contact)
        {
            try
            {
                return Ok(await _contact.UpdateContactAsync(contact));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
