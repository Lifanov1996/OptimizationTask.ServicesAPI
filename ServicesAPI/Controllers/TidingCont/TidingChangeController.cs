using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServicesAPI.BusinessLogic.Contracts;
using ServicesAPI.Models.Tidings;
using ServicesAPI.Models.Users;
using System.Net;

namespace ServicesAPI.Controllers.TidingCont
{
    [Authorize(Roles = UserRoles.Admin)]
    [Route("tidingchange")]
    [ApiController]
    public class TidingChangeController : ControllerBase
    {
        private readonly ITiding _tiding;
        private readonly ILogger<TidingChangeController> _logger;

        public TidingChangeController(ITiding tiding, ILogger<TidingChangeController> logger)
        {
            _tiding = tiding;
            _logger = logger;
            _logger.LogInformation("Инициализирован TidingChangeController");
        }


        /// <summary>
        /// Редактировать новость
        /// </summary>
        /// <param name="projects"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(typeof(Tidings), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Tidings>> ChangeProjectAsync(Tidings tiding)
        {
            try
            {
                return Ok(await _tiding.UpdateTidingAppAsync(tiding));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
