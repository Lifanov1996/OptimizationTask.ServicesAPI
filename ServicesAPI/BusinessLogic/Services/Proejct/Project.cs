using Microsoft.EntityFrameworkCore;
using ServicesAPI.BusinessLogic.Contracts;
using ServicesAPI.Data.Entity;
using ServicesAPI.Models.Projects;
using static System.Net.Mime.MediaTypeNames;

namespace ServicesAPI.BusinessLogic.Services.Proejct
{
    public class Project : IProject
    {
        private readonly ContextDB _contextDB;
        private ILogger<Project> _logger;

        public Project(ContextDB contextDB, ILogger<Project> logger)
        {
            _contextDB = contextDB;
            _logger = logger;
            _logger.LogInformation("Init Project");
        }


        public async Task<Projects> GetProjectAsync(int prId)
        {
            try
            {
                var project = await _contextDB.Projects.FindAsync(prId);      
                if (project == null)
                {
                    _logger.LogWarning($"The database does not have fields with id- {prId}");
                    throw new Exception("Error: Project for this id was not found");
                }
                return project;               
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<Projects>> GetAllProjectsAsync()
        {
            return await _contextDB.Projects.AsNoTracking().ToListAsync();
        }


        public async Task<Projects> AddProjectAsync(ProjectsAdd project)
        {
            try
            {
                Projects model = new Projects { Header = project.Header, File = project.File, Description = project.Description };
                await _contextDB.Projects.AddAsync(model);
                await _contextDB.SaveChangesAsync();
                
                _logger.LogInformation($"Add project id- {model.Id}");
                return model;
            }
            catch(Exception ex)
            {
                _logger.LogError($"Error added project - {ex.Message}");
                throw new Exception(ex.Message);
            }
        }


        public async Task<Projects> UpdateProjectAsync(Projects project)
        {
            try
            {
                _contextDB.Projects.Update(project);
                await _contextDB.SaveChangesAsync();

                _logger.LogInformation($"Updated project app: {project.Id}");
                return project;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new Exception(ex.Message);
            }
        }


        public async Task<bool> DeleteProjectAsync(int prId)
        {
            var project = await _contextDB.Projects.SingleOrDefaultAsync(x => x.Id == prId);
            if (project != null)
            {
                _contextDB.Projects.Remove(project);
                await _contextDB.SaveChangesAsync();

                _logger.LogInformation($"Remove project: id- {prId}");
                return true;
            }
            return false;
        }
    }
}
