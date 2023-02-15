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

        public Project(ContextDB contextDB)
        {
            _contextDB = contextDB;
        }


        public async Task<Projects> GetProjectAsync(int prId)
        {
            var project = await _contextDB.Projects.FindAsync(prId);
            if (project == null)
            {
                return null;
            }
            return project;
        }

        public async Task<IEnumerable<Projects>> GetAllProjectsAsync()
        {
            return await _contextDB.Projects.AsNoTracking().ToListAsync();
        }


        public async Task<Projects> UpdateProjectAppAsync(Projects project)
        {
            _contextDB.Projects.Update(project);
            await _contextDB.SaveChangesAsync();
            return project;
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
