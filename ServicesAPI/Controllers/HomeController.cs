using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServicesAPI.BusinessLogic.Contracts;
using ServicesAPI.Models.Applications;
using ServicesAPI.Models.Headers;
using System.Net;

namespace ServicesAPI.Controllers
{
    [Route("Home")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly IHeader _headers;

        public HomeController(IHeader header)
        {
            _headers = header;
        }


        [HttpGet]
        [ProducesResponseType(typeof(Headers), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Headers>> GetAsync()
        {
            try
            {
                return Ok(await _headers.GetHeaderAsync());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost]
        [ProducesResponseType(typeof(Headers), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Headers>> PostAsync(Headers data)
        {
            try
            {
                return Ok(await _headers.AddHeaderAsync(data));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPut]
        public async Task<ActionResult<Headers>> PutAsync(Headers data)
        {
            try
            {
                return Ok(await _headers.UpdateDescrHeaderAsync(data));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpDelete]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        public async Task<bool> DeletAsync()
        {
            return await _headers.DeletHeaderAsync();
        }
    }
}
