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
    [Route("tidingadd")]
    [ApiController]
    public class TidingAddController : ControllerBase
    {
        private readonly ITiding _tiding;
        private readonly ILogger _logger;

        public TidingAddController(ITiding tiding, ILogger logger)
        {
            _tiding = tiding;
            _logger = logger;
            _logger.LogInformation("Init TidingAddController");
        }


        /// <summary>
        /// Добавление новой новости
        /// </summary>
        /// <param name="projectsAdd"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(Tidings), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Tidings>> AddTidingAsync(TidingsAdd tiding)
        {
            try
            {
                return Ok(await _tiding.AddTidingAsync(tiding));    
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
