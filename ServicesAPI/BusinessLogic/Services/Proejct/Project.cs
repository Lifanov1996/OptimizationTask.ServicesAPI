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

        public Project(ContextDB contextDB, 
                       ILogger<Project> logger)
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
                return project;               
            }
            catch(Exception ex)
            {
                _logger.LogError($"Error get project by id- {0}: {1}", prId, ex.Message);
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<Projects>> GetAllProjectsAsync()
        {
            return await _contextDB.Projects.AsNoTracking().ToListAsync();
        }


        public async Task<Projects> AddProjectAsync(Projects project)
        {
            try
            {
                await _contextDB.Projects.AddAsync(project);
                await _contextDB.SaveChangesAsync();
                
                _logger.LogInformation($"Add project id- {0}", project.Id);
                return project;
            }
            catch(Exception ex)
            {
                _logger.LogError($"Error added project - ex {0}", ex.Message);
                throw new Exception(ex.Message);
            }
        }


        public async Task<Projects> UpdateProjectAppAsync(Projects project)
        {
            try
            {
                _contextDB.Projects.Update(project);
                await _contextDB.SaveChangesAsync();

                _logger.LogInformation($"Updated project app: {0}", project.Id);
                return project;
            }
            catch(Exception ex)
            {
                _logger.LogError($"Error update ex- {0}",ex.Message);
                throw new Exception(ex.Message);
            }
        }


        public async Task<bool> DeleteAppAsync(int prId)
        {
            var project = await _contextDB.Projects.SingleOrDefaultAsync(x => x.Id == prId);
            if (project != null)
            {
                _contextDB.Projects.Remove(project);
                await _contextDB.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
