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
            _logger.LogInformation("Инициализирован Office");
        }


        public async Task<Offices> GetOfficeAsync(int offId)
        {
            try
            {
                var office = await _contextDB.Offices.FindAsync(offId);
                if (office == null)
                {
                    _logger.LogWarning($"Запрос на услугу - {offId}. Услуга не найдена");
                    throw new Exception("Услуга не найдена.");
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
                Offices model = new Offices { Header = office.Header, 
                                              Description = office.Description };

                await _contextDB.Offices.AddAsync(model);
                await _contextDB.SaveChangesAsync();

                _logger.LogInformation($"Добавлена услуга - {model.Id}");
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
                var isData = await _contextDB.Offices.AnyAsync(x => x.Id == office.Id);
                if (!isData) 
                {
                    throw new Exception("Услуга не найдена.");
                }

                _contextDB.Offices.Update(office);
                await _contextDB.SaveChangesAsync();

                _logger.LogInformation($"Изменена услуга - {office.Id}");
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
            try
            {
                var office = await _contextDB.Offices.FindAsync(offId);
                if (office == null)
                {
                    throw new Exception("Услуга не найдена.");
                }
                
                _contextDB.Offices.Remove(office);
                await _contextDB.SaveChangesAsync();

                _logger.LogInformation($"Удалена услуга - {offId}");
                return true;     
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new Exception(ex.Message);
            }
        }
    }
}
