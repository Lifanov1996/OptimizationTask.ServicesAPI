using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServicesAPI.BusinessLogic.Contracts;
using ServicesAPI.Models.Applications;
using ServicesAPI.Models.Headers;
using ServicesAPI.Models.Users;
using System.Net;

namespace ServicesAPI.Controllers.HomeCont
{
    [Route("home")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly IApplicationClient _appClient;
        private readonly IHeader _header;
        private readonly ILogger<HomeController> _logger;

        public HomeController (IApplicationClient applicationCl, IHeader header, ILogger<HomeController> logger)
        {
            _appClient = applicationCl;
            _header = header;
            _logger = logger;
            _logger.LogInformation("Init HomeController");
        }


        /// <summary>
        /// Получения загаловка главной страници
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(Headers), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Headers>> GetHeaderAsync()
        {
            try
            {
                return Ok(await _header.GetHeaderAsync());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        /// <summary>
        /// Изменение загаловка
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [Authorize(Roles = UserRoles.Admin)]
        [HttpPut]
        public async Task<ActionResult<Headers>> PutHeaderAsync(Headers data)
        {
            try
            {
                return Ok(await _header.UpdateDescrHeaderAsync(data));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        /// <summary>
        /// Отправка заявки
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(ApplicationsClient), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Applications>> PostAppClientAsync(ApplicationsClient data)
        {
            try
            {
                return Ok(await _appClient.AddAppClientAsync(data));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
