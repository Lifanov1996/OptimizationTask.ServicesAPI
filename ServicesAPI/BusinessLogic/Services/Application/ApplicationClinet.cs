using ServicesAPI.BusinessLogic.Contracts;
using ServicesAPI.Data.Entity;
using ServicesAPI.Models.Applications;

namespace ServicesAPI.BusinessLogic.Services.Application
{
    public class ApplicationClinet : IApplicationClient
    {
        private readonly ContextDB _contextDB;
        private Applications _model;     

        public ApplicationClinet(ContextDB contextDB)
        {
            _contextDB = contextDB;          
        }

        public async Task<Applications> AddAppClientAsync(ApplicationsClient client)
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
            return _model;
        }
    }
}
