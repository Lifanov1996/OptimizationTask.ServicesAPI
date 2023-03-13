using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServicesAPI.BusinessLogic.Contracts;
using ServicesAPI.Models.Applications;
using ServicesAPI.Models.Offices;
using System.Net;

namespace ServicesAPI.Controllers
{
    [Route("Offices")]
    [ApiController]
    public class OfficesController : ControllerBase
    {
        private readonly IOffice _office;

        public OfficesController(IOffice office)
        {
            _office = office;
        }


        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Offices), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Offices>> GetId(int id)
        {
            try
            {
                return Ok(await _office.GetOfficeAsync(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet]
        [ProducesResponseType(typeof(Offices), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Offices>> GetAll()
        {
            try
            {
                return Ok(await _office.GetAllOfficesAsync());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost]
        [ProducesResponseType(typeof(Offices), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Offices>> Post(OfficesAdd data)
        {
            try
            {
                return Ok(await _office.AddOfficeAsync(data));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPut]
        public async Task<ActionResult<Offices>> PutAsync(Offices data)
        {
            try
            {
                return Ok(await _office.UpdateOfficeAsync(data));
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
            return await _office.DeleteOfficeAsync(id);
        }
    }
}
