using Microsoft.EntityFrameworkCore;
using ServicesAPI.BusinessLogic.Contracts;
using ServicesAPI.Data.Entity;
using ServicesAPI.Models.Applications;

namespace ServicesAPI.BusinessLogic.Services.Application
{
    public class ApplicationClinet : IApplicationClient
    {
        private readonly ContextDB _contextDB;
        private Applications _model;     
        private ILogger<ApplicationClinet> _logger;

        public ApplicationClinet(ContextDB contextDB, ILogger<ApplicationClinet> logger)
        {
            _contextDB = contextDB;
            _logger = logger;
            _logger.LogInformation("Инициализирован ApplicationClinet");
        }

        public async Task<Guid> AddAppClientAsync(ApplicationsClient client)
        {
            try
            {
                _model = new Applications()
                {
                    NumberApp = Guid.NewGuid(),
                    DateTimeCreatApp = DateTime.Now,
                    NameClient = client.NameClient,
                    DescriptionApp = client.DescriptionApp,
                    EmailClient = client.EmailClient,
                    StatusApp = "Получена"
                };

                await _contextDB.Applications.AddAsync(_model);
                await _contextDB.SaveChangesAsync();

                _logger.LogInformation($"Создана заявка, номер - {_model.Id}");
                return _model.NumberApp;
            }
            catch(Exception ex) 
            {
                _logger.LogError(ex.Message);
                throw new Exception(ex.Message);
            }
        }

        public async Task<Applications> GetAppAsync(string numberApp)
        {
            try
            {
                var application = await _contextDB.Applications.Where(g => g.NumberApp.ToString() == numberApp).SingleOrDefaultAsync();   
                if( application == null )
                {
                    _logger.LogInformation($"Запрос на заявку с номером - {numberApp}. Заявка не найдена");
                    throw new Exception($"Заявка с номер - {numberApp} не найдена. Проверте номер заявки!");
                }
                return application;
            }           
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new Exception(ex.Message);
            }
        }
    }
}
