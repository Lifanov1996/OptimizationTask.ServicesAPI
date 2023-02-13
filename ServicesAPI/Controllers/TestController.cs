using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using ServicesAPI.BusinessLogic.Contracts;
using ServicesAPI.Models.Applications;
using System.Net;

namespace ServicesAPI.Controllers
{
    [Route("api/Test")]
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
                return Ok(await _appAdmin.SeeAllAppAsync());
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
                return Ok(await _appAdmin.SeeOneAppAsync(id));
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
        public async Task<ActionResult<Applications>> PutAsync(Applications data)
        {
            try
            {
                return Ok(await _appAdmin.UpdateStatusAppAsync(data));
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
           
        }


        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        public async Task DeletAsync(int id)
        {
            await _appAdmin.DeleteAppAsync(id);            
        }
    }
}
