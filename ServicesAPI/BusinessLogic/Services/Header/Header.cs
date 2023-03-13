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
        private ILogger<Header> _logger;

        public Header(ContextDB contextDB, ILogger<Header> logger)
        {
            _contextDB = contextDB;
            _logger = logger;
            _logger.LogInformation("Init Header");
        }


        public async Task<Headers> GetHeaderAsync()
        {
            return await _contextDB.Headers.FirstAsync();
        }


        public async Task<Headers> AddHeaderAsync(HeadersAdd header)
        {
            try
            {
                Headers model = new Headers { Descriotion = header.Descriotion };
                await _contextDB.Headers.AddAsync(model);
                await _contextDB.SaveChangesAsync();

                _logger.LogInformation($"Added header");
                return model;
            }
            catch(Exception ex)
            {
                _logger.LogError($"Error added {ex.Message}");
                throw new Exception(ex.Message);
            }
        }


        public async Task<Headers> UpdateDescrHeaderAsync(Headers header)
        {
            try
            {
                _contextDB.Headers.Update(header);
                await _contextDB.SaveChangesAsync();

                _logger.LogInformation("Update header");
                return header;
            }
            catch(Exception ex)
            {
                _logger.LogError($"Error update header: {ex.Message}");
                throw new Exception(ex.Message);
            }
        }


        public async Task<bool> DeletHeaderAsync()
        {
            var head = await _contextDB.Headers.FirstAsync();
            if (head != null)
            {
                _contextDB.Headers.Remove(head);
                await _contextDB.SaveChangesAsync();

                _logger.LogInformation($"Remove header");
                return true;
            }
            return false;
        }
    }
}
