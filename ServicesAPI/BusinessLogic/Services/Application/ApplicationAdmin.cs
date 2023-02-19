using Microsoft.EntityFrameworkCore;
using ServicesAPI.BusinessLogic.Contracts;
using ServicesAPI.Data.Entity;
using ServicesAPI.Models.Applications;
using System.Data;
using System.Reflection.Metadata.Ecma335;
using System.Text.Json;

namespace ServicesAPI.BusinessLogic.Services.Application
{
    public class ApplicationAdmin : IApplicationAdmin
    {
        private readonly ContextDB _contextDB;
        private ILogger<ApplicationAdmin> _logger;

        public ApplicationAdmin(ContextDB contextDB, 
                                ILogger<ApplicationAdmin> logger)
        {
            _contextDB = contextDB;
            _logger = logger;
            _logger.LogInformation("Init ApplicationAdmin");
        }


        public async Task<Applications> GetAppAsync(int appId)
        {
            try
            {
                var application =  await _contextDB.Applications.FindAsync(appId);              
                return application;
            }
            catch(Exception ex)
            {
                _logger.LogInformation($"The database does not have fields with id- {0}", appId);
                throw new Exception(ex.Message);
            }
        }


        public async Task<IEnumerable<Applications>> GetAllAppAsync()
        {
            return await _contextDB.Applications.AsNoTracking().ToListAsync();
        }


        public async Task<Applications> UpdateStatusAppAsync(Applications application)
        {
            try
            {
                _contextDB.Applications.Update(application);
                await _contextDB.SaveChangesAsync();

                _logger.LogInformation($"Update status in application id- {0}", application.Id);
                return application;
            }
            catch(Exception ex)
            {
                _logger.LogError($"Data change error: {0}", ex.Message);
                throw new Exception(ex.Message);
            }
        }


        public async Task<bool> DeleteAppAsync(int appId)
        {
            var application = await _contextDB.Applications.SingleOrDefaultAsync(x => x.Id == appId);
            if(application != null) 
            {
                _contextDB.Applications.Remove(application);
                await _contextDB.SaveChangesAsync();
                return true;
            }
            return false;          
        }
    }
}
