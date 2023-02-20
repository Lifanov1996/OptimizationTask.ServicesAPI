using Microsoft.EntityFrameworkCore;
using ServicesAPI.BusinessLogic.Contracts;
using ServicesAPI.BusinessLogic.Services.Proejct;
using ServicesAPI.Data.Entity;
using ServicesAPI.Models.Tidings;

namespace ServicesAPI.BusinessLogic.Services.News
{
    public class Tiding : ITiding
    {
        private readonly ContextDB _contextDB;
        private Tidings _model;
        private ILogger<Tiding> _logger;

        public Tiding(ContextDB contextDB, ILogger<Tiding> logger) 
        {
            _contextDB = contextDB;
            _logger = logger;
            _logger.LogInformation("Inir Tiding");
        }


        public async Task<Tidings> GetTidingAsync(int tidId)
        {
            try
            {
                var result = await _contextDB.Tidings.FindAsync(tidId);     
                if(result == null)
                {
                    _logger.LogWarning($"The database does not have fields with id- {tidId}");
                    throw new Exception("Error 400: Tiding for this id was not found");
                }
                return result;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new Exception(ex.Message);
            }
        }


        public async Task<IEnumerable<Tidings>> GetAllTidingsAsync()
        {
            return await _contextDB.Tidings.AsNoTracking().ToListAsync();
        }


        public async Task<Tidings> AddTidingAsync(Tidings tid)
        {
            try
            {
                _model = new Tidings()
                {
                    DateTimePublication = DateTime.Now,
                    Header = tid.Header,
                    Description = tid.Description,
                    File = tid.File
                };

                await _contextDB.Tidings.AddAsync(_model);
                await _contextDB.SaveChangesAsync();

                _logger.LogInformation($"Add project id- {tid.Id}");
                return _model;
            }
            catch(Exception ex)
            {
                _logger.LogError($"Error added tiding - ex {ex.Message}");
                throw new Exception(ex.Message);
            }
        }


        public async Task<Tidings> UpdateTidingAppAsync(Tidings tid)
        {
            try
            {
                _contextDB.Tidings.Update(tid);
                await _contextDB.SaveChangesAsync();

                _logger.LogInformation($"Updated project app: {tid.Id}");
                return tid;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new Exception(ex.Message);
            }
        }


        public async Task<bool> DeleteTidingAsync(int tidId)
        {
            var result = await _contextDB.Tidings.SingleOrDefaultAsync(x => x.Id == tidId);
            if (result != null)
            {
                _contextDB.Tidings.Remove(result);
                await _contextDB.SaveChangesAsync();

                _logger.LogInformation($"Deleted tiding: id- {tidId}");
                return true;
            }
            return false;
        }
    }
}
