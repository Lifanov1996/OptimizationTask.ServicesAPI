using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using ServicesAPI.BusinessLogic.Contracts;
using ServicesAPI.Models.Applications;
using System.Net;

namespace ServicesAPI.Controllers
{
    [Route("Test")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly IApplicationAdmin _appAdmin;
        private readonly IApplicationClient _appClient;      

        public TestController(IApplicationAdmin appAdmin, IApplicationClient appClient)
        {
            _appAdmin = appAdmin;
            _appClient = appClient;
        }


        [HttpGet]
        [ProducesResponseType(typeof(Applications), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Applications>> GetAll()
        {
            try
            {
                return Ok(await _appAdmin.GetAllAppAsync());
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Applications), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Applications>> Get(int id)
        {
            try
            {
                return Ok(await _appAdmin.GetAppAsync(id));
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

       
        [HttpPost]
        [ProducesResponseType(typeof(ApplicationsClient), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Applications>> Post(ApplicationsClient data)
        {
            try
            {
                return Ok(await _appClient.AddAppClientAsync(data));            
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    

        [HttpPut]
        public async Task<ActionResult<Applications>> PutAsync(int id, string status)
        {
            try
            {
                return Ok(await _appAdmin.UpdateStatusAppAsync(id, status));
            }
            catch(Exception ex)
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
