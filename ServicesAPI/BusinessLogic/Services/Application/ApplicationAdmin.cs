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
            _logger.LogInformation("Инициализирован ApplicationAdmin");
        }


        public async Task<Applications> GetAppAsync(int appId)
        {
            try
            {
                var application =  await _contextDB.Applications.FindAsync(appId);
                if (application == null)
                {
                    throw new Exception($"Заявка с номером  - {appId} не найдена. Проверте номер заяки!");
                }
                return application;
            }
            catch(Exception ex)
            {
                _logger.LogError($"Запрос зявки с номером - {appId}. Заявка не найдена!");
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
                    _logger.LogWarning($"Введен не валидный статус - {appCh.StatusApp}, заявка {appCh.Id}");
                    throw new Exception($"Статус заявки - {appCh.Id} не изменен : введен не валидный статус - {appCh.StatusApp}");
                }

                var application = await _contextDB.Applications.FindAsync(appCh.Id);
                if(application == null)
                {
                    _logger.LogWarning($"Запрос зявки с номером - {appCh.Id}. Заявка не найдена!");
                    throw new Exception($"Заявка с номером  - {appCh.Id} не найдена. Проверте номер заяки!");
                }                

                application.StatusApp = appCh.StatusApp;      
                await _contextDB.SaveChangesAsync();

                _logger.LogInformation($"Измене статус заявки - {appCh.Id} на {appCh.StatusApp}");
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
            try
            {
                var application = await _contextDB.Applications.SingleOrDefaultAsync(x => x.Id == appId);
                if(application != null) 
                {
                    _contextDB.Applications.Remove(application);
                    await _contextDB.SaveChangesAsync();

                    _logger.LogInformation($"Удалена заявка с номером - {appId}");
                    return true;
                }
                _logger.LogWarning($"Удаление зявки с номером - {appId}. Заявка не найдена!");
                throw new Exception($"Заявка с номером  - {appId} не найдена. Проверте номер заяки!");                       
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new Exception(ex.Message);
            }
        }
    }
}
