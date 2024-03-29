﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServicesAPI.BusinessLogic.Contracts;
using ServicesAPI.Models.Projects;
using ServicesAPI.Models.Users;
using System.Data;
using System.Net;

namespace ServicesAPI.Controllers.ProjectCont
{
    [Authorize(Roles = UserRoles.Admin)]
    [Route("projectchange")]
    [ApiController]
    public class ProjectChangeController : ControllerBase
    {
        private readonly IProject _project;
        private readonly ILogger<ProjectChangeController> _logger;

        public ProjectChangeController(IProject project, ILogger<ProjectChangeController> logger)
        {
            _project = project;
            _logger = logger;
            _logger.LogInformation("Инициализация ProjectChangeController");
        }


        /// <summary>
        /// Редактировать проект
        /// </summary>
        /// <param name="projects"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(typeof(Projects), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Projects>> ChangeProjectAsync(Projects projects)
        {
            try
            {
                return Ok(await _project.UpdateProjectAsync(projects));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
