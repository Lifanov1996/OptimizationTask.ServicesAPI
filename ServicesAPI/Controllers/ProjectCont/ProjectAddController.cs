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
    [Authorize(Roles = UserRoles.Admin)]
    [Route("projectadd")]
    [ApiController]
    public class ProjectAddController : ControllerBase
    {
        private readonly IProject _project;
        private readonly ILogger _logger;

        public ProjectAddController(IProject project, ILogger logger)
        {
            _project = project;
            _logger = logger;
            _logger.LogInformation("Init ProjectAddController");
        }


        [HttpPost]
        [ProducesResponseType(typeof(Projects), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Projects>> AddProjectAsync(ProjectsAdd projectsAdd)
        {
            try
            {
                return Ok(await _project.AddProjectAsync(projectsAdd));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
