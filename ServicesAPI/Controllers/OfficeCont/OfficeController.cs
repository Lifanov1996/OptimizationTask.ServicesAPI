using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServicesAPI.BusinessLogic.Contracts;
using ServicesAPI.Models.Offices;
using ServicesAPI.Models.Users;
using System.Data;
using System.Net;

namespace ServicesAPI.Controllers.OfficeCont
{
    [Route("office")]
    [ApiController]
    public class OfficeController : ControllerBase
    {
        private readonly IOffice _office;
        private readonly ILogger _logger;

        public OfficeController(IOffice office, ILogger logger)
        {
            _office = office;
            _logger = logger;
            _logger.LogInformation("Init OfficeController");
        }


        /// <summary>
        /// Получение списка услуг
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(Offices), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Offices>> GetAllOfficesAsync()
        {
            try
            {
                return Ok(await _office.GetAllOfficesAsync());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        /// <summary>
        /// Редактировать услугу
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [Authorize(Roles = UserRoles.Admin)]
        [HttpPut]
        [ProducesResponseType(typeof(Offices), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Offices>> GetUpdateOfficeAsync(int Id)
        {
            try
            {
                return Ok(await _office.GetOfficeAsync(Id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        /// <summary>
        /// Удаление услуги
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [Authorize(Roles = UserRoles.Admin)]
        [HttpDelete("{Id}")]
        [ProducesResponseType(typeof(Offices), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> DeletOfficeAsync(int Id)
        {
            try
            {
                return Ok(await _office.DeleteOfficeAsync(Id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
