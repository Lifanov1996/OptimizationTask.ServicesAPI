using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServicesAPI.BusinessLogic.Contracts;
using ServicesAPI.Models.Applications;
using ServicesAPI.Models.Headers;
using System.Net;

namespace ServicesAPI.Controllers.HomeCont
{
    [Route("[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly IApplicationClient _appClient;
        private readonly IHeader _header;
        private readonly ILogger<ContactsController> _logger;

        public HomeController (IApplicationClient applicationCl, IHeader header, ILogger<ContactsController> logger)
        {
            _appClient = applicationCl;
            _header = header;
            _logger = logger;
            _logger.LogInformation("Init HomeController");
        }


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


        [Authorize]
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
    }
}
