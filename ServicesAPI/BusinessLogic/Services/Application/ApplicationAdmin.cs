using Azure.Messaging;
using Microsoft.EntityFrameworkCore;
using ServicesAPI.BusinessLogic.Contracts;
using ServicesAPI.Data.Entity;
using ServicesAPI.Models.Applications;
using System.Data;
using System.Reflection.Metadata.Ecma335;
using System.Text.Json;
using static System.Net.Mime.MediaTypeNames;

namespace ServicesAPI.BusinessLogic.Services.Application
{
    public class ApplicationAdmin : IApplicationAdmin
    {
        private readonly ContextDB _contextDB;
        private ILogger<ApplicationAdmin> _logger;

        public ApplicationAdmin(ContextDB contextDB, ILogger<ApplicationAdmin> logger)
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
                if (application == null)
                {
                    _logger.LogWarning($"The database does not have fields with id- {appId}");
                    throw new Exception("Error: Application for this id was not found");
                }
                return application;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new Exception(ex.Message);
            }
        }


        public async Task<IEnumerable<Applications>> GetAllAppAsync()
        {
            return await _contextDB.Applications.AsNoTracking().ToListAsync();
        }


        public async Task<Applications> UpdateStatusAppAsync(ApplicationsChange appCh)
        {
            string[] status = { "Получена", "В работе", "Выполнена", "Отклонена", "Отменена" };
            try
            {
                if(!status.Contains(appCh.StatusApp))
                {
                    _logger.LogWarning($"Invalid status entered - {appCh.StatusApp}");
                    throw new Exception($"Error : Invalid status entered - {appCh.StatusApp}");
                }

                var application = await _contextDB.Applications.FindAsync(appCh.Id);
                if(application == null)
                {
                    _logger.LogWarning($"The database does not have fields with id- {appCh.Id}");
                    throw new Exception("Error : Application for this id was not found");
                }                

                application.StatusApp = appCh.StatusApp;      
                await _contextDB.SaveChangesAsync();

                _logger.LogInformation($"Update status in application id- {appCh.Id}");
                return application;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
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
