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


        public async Task<Applications> SeeOneAppAsync(int appId)
        {
            var result =  await _contextDB.Applications.FindAsync(appId);    
            if(result == null)
            {
                return null;
            }
            return result;
        }


        public async Task<IEnumerable<Applications>> SeeAllAppAsync()
        {
            return await _contextDB.Applications.AsNoTracking().ToListAsync();
        }


        public async Task<Applications> UpdateStatusAppAsync(Applications applications)
        {
            _contextDB.Applications.Update(applications);
            await _contextDB.SaveChangesAsync();
            return applications;
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
