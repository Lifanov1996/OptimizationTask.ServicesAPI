using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServicesAPI.BusinessLogic.Contracts;
using ServicesAPI.Models.Applications;
using System.Net;

namespace ServicesAPI.Controllers.ApplicationsCont
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class ApplicationController : ControllerBase
    {
        private readonly IApplicationAdmin _appAdmin;
        private readonly ILogger<ContactsController> _logger;

        public ApplicationController(IApplicationAdmin appAdmin, ILogger<ContactsController> logger)
        {
            _appAdmin = appAdmin;
            _logger = logger;
            _logger.LogInformation("Init ApplicationController");
        }


        [HttpGet]
        [ProducesResponseType(typeof(Applications), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Applications>> GetAllApplicationsAsync()
        {
            try
            {
                return Ok(await _appAdmin.GetAllAppAsync());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPut]
        public async Task<ActionResult<Applications>> PutAsync(ApplicationsChange appCh)
        {
            try
            {
                return Ok(await _appAdmin.UpdateStatusAppAsync(appCh));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        public async Task<bool> DeletAsync(int id)
        {
            return await _appAdmin.DeleteAppAsync(id);
        }
    }
}
