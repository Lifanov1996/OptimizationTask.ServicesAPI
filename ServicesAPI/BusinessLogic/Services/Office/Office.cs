using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using ServicesAPI.BusinessLogic.Contracts;
using ServicesAPI.BusinessLogic.Services.Header;
using ServicesAPI.Data.Entity;
using ServicesAPI.Models.Offices;
using System.Security.Cryptography;

namespace ServicesAPI.BusinessLogic.Services.Office
{
    public class Office : IOffice
    {
        private readonly ContextDB _contextDB;
        private ILogger<Office> _logger;
        private readonly IMemoryCache _cache;

        public Office(ContextDB contextDB, ILogger<Office> logger, IMemoryCache memoryCache)
        {
            _contextDB = contextDB;
            _logger = logger;
            _cache = memoryCache;
            _logger.LogInformation("Инициализирован Office");
        }


        public async Task<Offices> GetOfficeAsync(int offId)
        {
            try
            {
                Offices office = null;
                if(!_cache.TryGetValue(offId, out office))
                {
                    office = await _contextDB.Offices.FindAsync(offId);

                    if (office == null)
                    {
                        _logger.LogWarning($"Запрос на услугу - {offId}. Услуга не найдена");
                        throw new Exception("Услуга не найдена.");
                    }
                    _cache.Set(office.Id, office, new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromDays(1)));
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


        public async Task<Offices> AddOfficeAsync(OfficesAdd officeAdd)
        {
            try
            {
                Offices office = new Offices { Header = officeAdd.Header, 
                                              Description = officeAdd.Description };

                await _contextDB.Offices.AddAsync(office);
                int isResult = await _contextDB.SaveChangesAsync();

                if (isResult > 0)
                {
                    _cache.Set(office.Id, office, new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromDays(1)));
                }

                _logger.LogInformation($"Добавлена услуга - {office.Id}");
                return office;
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
                int isResult = await _contextDB.SaveChangesAsync();
                if (isResult > 0)
                {
                    _cache.Remove(office.Id);
                    _cache.Set(office.Id, office, new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromDays(1)));
                }

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
                int isResult = await _contextDB.SaveChangesAsync();
                if(isResult > 0)
                {
                    _cache.Remove(offId);                   
                }

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
