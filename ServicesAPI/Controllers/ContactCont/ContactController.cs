﻿using Microsoft.AspNetCore.Authorization;
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
        private readonly ILogger<ContactController> _logger;

        public ContactController(IContact contact, ILogger<ContactController> logger)
        {
            _contact = contact;
            _logger = logger;
            _logger.LogInformation("Инициализирован ContactController");
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
