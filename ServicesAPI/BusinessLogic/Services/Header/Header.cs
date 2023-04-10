using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
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

        public Header(ContextDB contextDB, ILogger<Header> logger, IMemoryCache memoryCache)
        {
            _contextDB = contextDB;
            _logger = logger;
            _logger.LogInformation("Инициализирован Header");
        }


        public async Task<Headers> GetHeaderAsync()
        {
            try
            {
                return await _contextDB.Headers.FirstAsync();
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new Exception("Ошибка загрузки");
            }
        }


        public async Task<Headers> AddHeaderAsync(HeadersAdd header)
        {
            try
            {
                var isData = await _contextDB.Headers.SingleOrDefaultAsync();
                if(isData != null)
                {
                    _logger.LogError("Добавление второго заголовка");
                    throw new Exception("Заголовок уже добавлен! Измените существующий заголовок");
                }
                Headers model = new Headers { Descriotion = header.Descriotion };
                await _contextDB.Headers.AddAsync(model);
                await _contextDB.SaveChangesAsync();

                _logger.LogInformation($"Добавлен заголовок главной странице");
                return model;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new Exception(ex.Message);
            }
        }


        public async Task<Headers> UpdateDescrHeaderAsync(Headers header)
        {
            try
            {
                var isData = await _contextDB.Headers.FindAsync(header.Id);
                if(isData == null)
                {
                    throw new Exception("Ошибка изменения! Заголовок не найден");
                }

                isData.Descriotion = header.Descriotion;
                //_contextDB.Headers.Update(header);
                await _contextDB.SaveChangesAsync();

                _logger.LogInformation("Изменен заголовок главной страницы");
                return header;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new Exception("Ошибка изменения заголовка");
            }
        }


        public async Task<bool> DeletHeaderAsync()
        {
            var head = await _contextDB.Headers.FirstAsync();
            if (head != null)
            {
                _contextDB.Headers.Remove(head);
                await _contextDB.SaveChangesAsync();

                _logger.LogInformation($"Заголовок главной странице удален");
                return true;
            }
            return false;
        }
    }
}
