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
        private readonly ILogger<ContactsController> _logger;

        public ContactsController(IContact contact, ILogger<ContactsController> logger)
        {
            _contact = contact;
            _logger = logger;
            _logger.LogInformation(1, "NLog injected into HomeController");
        }

        [HttpGet]
        [ProducesResponseType(typeof(Contacts), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Contacts>> GetAsync()
        {
            try
            {
                _logger.LogInformation("Hello, this is the index!");
                return Ok(await _contact.GetContactAsync());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }        
        }

        [HttpPut]
        public async Task<ActionResult<Contacts>> PutAsync(Contacts cont)
        {
            try
            {
                _logger.LogInformation($"Изменение контактной информации");
                return Ok(await _contact.UpdateContactAsync(cont));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ошибка изменения информации - {0}", ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}
