using Microsoft.EntityFrameworkCore;
using ServicesAPI.BusinessLogic.Contracts;
using ServicesAPI.Data.Entity;
using ServicesAPI.Models.Applications;
using ServicesAPI.Models.Headers;

namespace ServicesAPI.BusinessLogic.Services.Header
{
    public class Header : IHeader
    {
        private readonly ContextDB _contextDB;

        public Header(ContextDB contextDB)
        {
            _contextDB = contextDB;
        }


        public async Task<Headers> GetHeaderAsync()
        {
            return await _contextDB.Headers.FirstAsync();
        }


        public async Task<Headers> AddHeaderAsync(Headers header)
        {
            await _contextDB.Headers.AddAsync(header);
            await _contextDB.SaveChangesAsync();
            return header;
        }


        public async Task<Headers> UpdateDescrHeaderAsync(Headers header)
        {
            _contextDB.Headers.Update(header);
            await _contextDB.SaveChangesAsync();
            return header;
        }


        public async Task<bool> DeletHeaderAsync()
        {
            var head = await _contextDB.Headers.FirstAsync();
            if (head != null)
            {
                _contextDB.Headers.Remove(head);
                await _contextDB.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
