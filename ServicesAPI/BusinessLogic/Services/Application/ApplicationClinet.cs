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

        public ApplicationClinet(ContextDB contextDB, 
                                 ILogger<ApplicationClinet> logger)
        {
            _contextDB = contextDB;
            _logger = logger;
            _logger.LogInformation("Init ApplicationClinet");
        }

        public async Task<Applications> AddAppClientAsync(ApplicationsClient client)
        {
            try
            {
                _model = new Applications()
                {
                    DateTimeCreatApp = DateTime.Now,
                    NameClient = client.NameClient,
                    DescriptionApp = client.DescriptionApp,
                    EmailClient = client.EmailClient,
                    StatusApp = "Получена"               
                };
            
                await _contextDB.Applications.AddAsync(_model);
                await _contextDB.SaveChangesAsync();

                _logger.LogInformation($"Add application");
                return _model;
            }
            catch(Exception ex) 
            {
                _logger.LogError($"Error added application : {0}", ex.Message);
                throw new Exception(ex.Message);
            }
        }
    }
}
