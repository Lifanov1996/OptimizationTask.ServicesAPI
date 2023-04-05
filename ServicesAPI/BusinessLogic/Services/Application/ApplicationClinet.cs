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
            _logger.LogInformation("Init ApplicationClinet");
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

                _logger.LogInformation($"Add application id- {_model.Id}");
                return _model.NumberApp;
            }
            catch(Exception ex) 
            {
                _logger.LogError($"Error added application : {ex.Message}");
                throw new Exception(ex.Message);
            }
        }

        public async Task<Applications> GetAppAsync(string numberApp)
        {
            try
            {
                var application = (Applications) _contextDB.Applications.Where(g => g.NumberApp.ToString() == numberApp).Single();
                if (application == null)
                {
                    _logger.LogWarning($"The database does not have fields with numberApp- {numberApp}");
                    throw new Exception("Error: Application for this id was not found");
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
