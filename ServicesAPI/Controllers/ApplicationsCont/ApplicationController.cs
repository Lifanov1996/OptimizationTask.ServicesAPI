using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServicesAPI.BusinessLogic.Contracts;
using ServicesAPI.Models.Applications;
using ServicesAPI.Models.Users;
using System.Net;

namespace ServicesAPI.Controllers.ApplicationsCont
{
    [Authorize(Roles = UserRoles.Admin)]
    [Route("application")]
    [ApiController]
    public class ApplicationController : ControllerBase
    {
        private readonly IApplicationAdmin _appAdmin;
        private readonly ILogger _logger;

        public ApplicationController(IApplicationAdmin appAdmin, ILogger logger)
        {
            _appAdmin = appAdmin;
            _logger = logger;
            _logger.LogInformation("Init ApplicationController");
        }


        /// <summary>
        /// Получения списка заявок
        /// </summary>
        /// <returns></returns>
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


        /// <summary>
        /// Изменение статуса заявки
        /// </summary>
        /// <param name="appCh"></param>
        /// <returns></returns>
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


        /// <summary>
        /// Удаление заявки
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        public async Task<bool> DeletAsync(int id)
        {
            return await _appAdmin.DeleteAppAsync(id);
        }
    }
}
