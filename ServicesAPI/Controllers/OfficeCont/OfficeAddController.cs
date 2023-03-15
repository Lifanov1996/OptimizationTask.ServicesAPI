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
    [Route("api/[controller]")]
    [ApiController]
    public class OfficeAddController : ControllerBase
    {
        private readonly IOffice _office;
        private readonly ILogger<OfficeAddController> _logger;

        public OfficeAddController(IOffice office, ILogger<OfficeAddController> logger)
        {
            _office = office;
            _logger = logger;
            _logger.LogInformation("Init OfficeAddController");
        }


        /// <summary>
        /// Добавление новой услуги
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>        
        [HttpPut]
        [ProducesResponseType(typeof(Offices), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Offices>> AddOfficeAsync(OfficesAdd offices)
        {
            try
            {
                return Ok(await _office.AddOfficeAsync(offices));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
