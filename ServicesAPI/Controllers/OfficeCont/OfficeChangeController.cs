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
    [Authorize(Roles = UserRoles.Admin)]
    [Route("officechange")]
    [ApiController]
    public class OfficeChangeController : ControllerBase
    {
        private readonly IOffice _office;
        private readonly ILogger<OfficeChangeController> _logger;

        public OfficeChangeController(IOffice office, ILogger<OfficeChangeController> logger)
        {
            _office = office;
            _logger = logger;
            _logger.LogInformation("Init OfficeChangeController");
        }


        /// <summary>
        /// Редактировать услугу
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>        
        [HttpPut]
        [ProducesResponseType(typeof(Offices), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Offices>> GetUpdateOfficeAsync(Offices offices)
        {
            try
            {
                return Ok(await _office.UpdateOfficeAsync(offices));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
