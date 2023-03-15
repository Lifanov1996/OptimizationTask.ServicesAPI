using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServicesAPI.BusinessLogic.Contracts;
using ServicesAPI.Models.Contacts;
using ServicesAPI.Models.Users;
using System.Net;

namespace ServicesAPI.Controllers.ContactCont
{
    [Route("contact")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly IContact _contact;
        private readonly ILogger _logger;

        public ContactController(IContact contact, ILogger logger)
        {
            _contact = contact;
            _logger = logger;
            _logger.LogInformation("Init ContactController");
        }


        /// <summary>
        /// Получение контактной информации
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(Contacts), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Contacts>> GetContactAsync()
        {
            try
            {
                return Ok(await _contact.GetContactAsync());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        /// <summary>
        /// Редактирование контактов
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [Authorize(Roles = UserRoles.Admin)]
        [HttpPut]
        [ProducesResponseType(typeof(Contacts), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Contacts>> GetUpdateContactAsync()
        {
            try
            {
                return Ok(await _contact.GetContactAsync());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        /// <summary>
        /// Добавление контактной информации
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = UserRoles.Admin)]
        [HttpPost]
        [ProducesResponseType(typeof(Contacts), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Contacts>> AddContactAsync()
        {
            try
            {
                return Ok(await _contact.AddContactAsync());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
