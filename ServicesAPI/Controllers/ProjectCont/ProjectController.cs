using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServicesAPI.BusinessLogic.Contracts;
using ServicesAPI.Models.Projects;
using ServicesAPI.Models.Users;
using System.Data;
using System.Net;

namespace ServicesAPI.Controllers.ProjectCont
{
    [Route("project")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IProject _project;
        private readonly ILogger<ProjectController> _logger;

        public ProjectController(IProject project, ILogger<ProjectController> logger)
        {
            _project = project;
            _logger = logger;
            _logger.LogInformation("Инициализирован ProjectController");
        }


        /// <summary>
        /// Получение списка проектов
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(Projects), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Projects>> GetAllProjectsAsync()
        {
            try
            {
                return Ok(await _project.GetAllProjectsAsync());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        /// <summary>
        /// Полная информация о проекте
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet("{Id}")]
        [ProducesResponseType(typeof(Projects), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Projects>> GetProjectAsync(int Id)
        {
            try
            {
                return Ok(await _project.GetProjectAsync(Id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        /// <summary>
        /// Редактировать проект
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [Authorize(Roles = UserRoles.Admin)]
        [HttpPut]
        [ProducesResponseType(typeof(Projects), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Projects>> GetUpdateProjectAsync(int Id)
        {
            try
            {
                return Ok(await _project.GetProjectAsync(Id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        /// <summary>
        /// Удаление проекта
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [Authorize(Roles = UserRoles.Admin)]
        [HttpDelete("{Id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult> DeletProjectAsync(int Id)
        {
            try
            {
                await _project.DeleteProjectAsync(Id);
                return Ok($"Проект номер - {Id} удален!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
