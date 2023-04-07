using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServicesAPI.BusinessLogic.Contracts;
using ServicesAPI.Models.Projects;
using ServicesAPI.Models.Tidings;
using ServicesAPI.Models.Users;
using System.Data;
using System.Net;

namespace ServicesAPI.Controllers.TidingCont
{
    [Route("tiding")]
    [ApiController]
    public class TidingController : ControllerBase
    {
        private readonly ITiding _tiding;
        private readonly ILogger<TidingController> _logger;

        public TidingController(ITiding tiding, ILogger<TidingController> logger)
        {
            _tiding = tiding;
            _logger = logger;
            _logger.LogInformation("Инициализирован TidingController");
        }


        /// <summary>
        /// Получение списка новостей
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<Tidings>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Tidings>> GetAllTidingsAsync()
        {
            try
            {
                return Ok(await _tiding.GetAllTidingsAsync());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        /// <summary>
        /// Полная информация о новости
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet("{Id}")]
        [ProducesResponseType(typeof(Tidings), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Tidings>> GetTidingAsync(int Id)
        {
            try
            {
                return Ok(await _tiding.GetTidingAsync(Id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        /// <summary>
        /// Редактировать новость
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [Authorize(Roles = UserRoles.Admin)]
        [HttpPut]
        [ProducesResponseType(typeof(Tidings), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Tidings>> GetUpdateTidingAsync(int Id)
        {
            try
            {
                return Ok(await _tiding.GetTidingAsync(Id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        /// <summary>
        /// Удаление новости
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [Authorize(Roles = UserRoles.Admin)]
        [HttpDelete("{Id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult> DeletTidingAsync(int Id)
        {
            try
            {
                await _tiding.DeleteTidingAsync(Id);
                return Ok($"Новость - {Id} удален!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
