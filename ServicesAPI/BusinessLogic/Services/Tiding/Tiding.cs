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

        public Tiding(ContextDB contextDB) 
        {
            _contextDB = contextDB;
        }


        public async Task<Tidings> GetTidingAsync(int tidId)
        {
            var result = await _contextDB.Tidings.FindAsync(tidId);
            if(result == null)
            {
                return null;
            }
            return result;
        }


        public async Task<IEnumerable<Tidings>> GetAllTidingsAsync()
        {
            return await _contextDB.Tidings.AsNoTracking().ToListAsync();
        }


        public async Task<Tidings> AddTidingAsync(Tidings tid)
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
            return _model;
        }


        public async Task<Tidings> UpdateTidingAppAsync(Tidings tid)
        {
            _contextDB.Tidings.Update(tid);
            await _contextDB.SaveChangesAsync();
            return tid;
        }


        public async Task<bool> DeleteTidingAsync(int tidId)
        {
            var result = await _contextDB.Tidings.SingleOrDefaultAsync(x => x.Id == tidId);
            if (result != null)
            {
                _contextDB.Tidings.Remove(result);
                await _contextDB.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
