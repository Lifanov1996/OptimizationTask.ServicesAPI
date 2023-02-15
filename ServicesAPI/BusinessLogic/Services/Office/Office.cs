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

        public Office(ContextDB contextDB)
        {
            _contextDB = contextDB;
        }


        public async Task<Offices> GetOfficeAsync(int offId)
        {
            var office = await _contextDB.Offices.FindAsync(offId);
            if (office == null)
            {
                return null;
            }
            return office;
        }


        public async Task<IEnumerable<Offices>> GetAllOfficesAsync()
        {
            return await _contextDB.Offices.AsNoTracking().ToListAsync();
        }


        public async Task<Offices> UpdateOfficeAppAsync(Offices project)
        {
            _contextDB.Offices.Update(project);
            await _contextDB.SaveChangesAsync();
            return project;
        }


        public async Task<bool> DeleteOfficeAsync(int offId)
        {
            var office = await _contextDB.Offices.SingleOrDefaultAsync(x => x.Id == offId);
            if (office != null)
            {
                _contextDB.Offices.Remove(office);
                await _contextDB.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
