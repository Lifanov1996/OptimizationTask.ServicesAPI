using Microsoft.EntityFrameworkCore;
using ServicesAPI.BusinessLogic.Contracts;
using ServicesAPI.Data.Entity;
using ServicesAPI.Models.Applications;
using System.Reflection.Metadata.Ecma335;
using System.Text.Json;

namespace ServicesAPI.BusinessLogic.Services.Application
{
    public class ApplicationAdmin : IApplicationAdmin
    {
        private readonly ContextDB _contextDB;

        public ApplicationAdmin(ContextDB contextDB)
        {
            _contextDB = contextDB;
        }


        public async Task<Applications> GetAppAsync(int appId)
        {
            var application =  await _contextDB.Applications.FindAsync(appId);    
            if(application == null)
            {
                return null;
            }
            return application;
        }


        public async Task<IEnumerable<Applications>> GetAllAppAsync()
        {
            return await _contextDB.Applications.AsNoTracking().ToListAsync();
        }


        public async Task<Applications> UpdateStatusAppAsync(Applications application)
        {
            _contextDB.Applications.Update(application);
            await _contextDB.SaveChangesAsync();
            return application;
        }


        public async Task<bool> DeleteAppAsync(int appId)
        {
            var application = await _contextDB.Applications.SingleOrDefaultAsync(x => x.Id == appId);
            if(application != null) 
            {
                _contextDB.Applications.Remove(application);
                await _contextDB.SaveChangesAsync();
                return true;
            }
            return false;          
        }
    }
}
