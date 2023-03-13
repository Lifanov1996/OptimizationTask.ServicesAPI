using Microsoft.EntityFrameworkCore;
using ServicesAPI.BusinessLogic.Contracts;
using ServicesAPI.BusinessLogic.Services.Header;
using ServicesAPI.Data.Entity;
using ServicesAPI.Models.Offices;

namespace ServicesAPI.BusinessLogic.Services.Office
{
    public class Office : IOffice
    {
        private readonly ContextDB _contextDB;
        private ILogger<Office> _logger;

        public Office(ContextDB contextDB, ILogger<Office> logger)
        {
            _contextDB = contextDB;
            _logger = logger;
            _logger.LogInformation("Init Office");
        }


        public async Task<Offices> GetOfficeAsync(int offId)
        {
            try
            {
                var office = await _contextDB.Offices.FindAsync(offId);
                if (office == null)
                {
                    _logger.LogWarning($"The database does not have fields with id- {offId}");
                    throw new Exception("Error: Office for this id was not found");
                }
                return office;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new Exception(ex.Message);
            }
        }


        public async Task<IEnumerable<Offices>> GetAllOfficesAsync()
        {
            return await _contextDB.Offices.AsNoTracking().ToListAsync();
        }


        public async Task<Offices> AddOfficeAsync(OfficesAdd office)
        {
            try
            {
                Offices model = new Offices { Header= office.Header, Description = office.Description };
                await _contextDB.Offices.AddAsync(model);
                await _contextDB.SaveChangesAsync();

                _logger.LogInformation($"Added office id- {model.Id}");
                return model;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new Exception(ex.Message);
            }
        }
        

        public async Task<Offices> UpdateOfficeAsync(Offices office)
        {
            try
            {               
                _contextDB.Offices.Update(office);
                await _contextDB.SaveChangesAsync();

                _logger.LogInformation($"Update office id- {office.Id}");
                return office;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new Exception(ex.Message);
            }
        }


        public async Task<bool> DeleteOfficeAsync(int offId)
        {
            var office = await _contextDB.Offices.SingleOrDefaultAsync(x => x.Id == offId);
            if (office != null)
            {
                _contextDB.Offices.Remove(office);
                await _contextDB.SaveChangesAsync();

                _logger.LogInformation($"Remove office: id- {offId}");
                return true;
            }
            return false;
        }
    }
}
