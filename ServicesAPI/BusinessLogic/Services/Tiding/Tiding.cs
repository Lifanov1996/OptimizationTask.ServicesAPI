using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using ServicesAPI.BusinessLogic.Contracts;
using ServicesAPI.BusinessLogic.Services.Proejct;
using ServicesAPI.Data.Entity;
using ServicesAPI.Models.Tidings;

namespace ServicesAPI.BusinessLogic.Services.News
{
    public class Tiding : ITiding
    {
        private readonly IImage _image;
        private readonly ContextDB _contextDB;
        private ILogger<Tiding> _logger;
        private readonly IMemoryCache _cache;

        public Tiding(ContextDB contextDB, ILogger<Tiding> logger, IImage image, IMemoryCache memoryCache) 
        {
            _image = image;
            _contextDB = contextDB;
            _logger = logger;
            _cache = memoryCache;
            _logger.LogInformation("Инициализирован Tiding");
        }


        public async Task<Tidings> GetTidingAsync(int tidId)
        {
            try
            {
                Tidings tiding = null;
                if(!_cache.TryGetValue(tidId, out tiding))
                {
                    tiding = await _contextDB.Tidings.FindAsync(tidId);  
                    
                    if(tiding == null)
                    {
                        _logger.LogWarning($"Запрос на новость - {tidId}. Новость не найдена");
                        throw new Exception("Новость не найдена");
                    }
                    _cache.Set(tiding.Id, tiding, new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromDays(1)));
                }
                return tiding;
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


        public async Task<Tidings> AddTidingAsync(TidingsAdd tidingAdd)
        {
            try
            {
                if (tidingAdd.Image == null && tidingAdd.UrlImage == null)
                {
                    throw new Exception("Изображение не загруженно! Добавьте изображение или url- изображения.");
                }

                string nameImage = null;
                string urlImage = null;
                if (tidingAdd.Image != null)
                {
                    nameImage = await _image.AddImageAsync(tidingAdd.Image);
                    urlImage = "https://localhost:7297" + nameImage;
                    _logger.LogInformation($"Загружен файл - {tidingAdd.Image.FileName}");
                }

                Tidings tiding = new Tidings() { DateTimePublication = DateTime.Now,
                                                Header = tidingAdd.Header,
                                                NameImage = nameImage,
                                                UrlImage = urlImage?? tidingAdd.UrlImage,
                                                Description = tidingAdd.Description };

                await _contextDB.Tidings.AddAsync(tiding);
                int isResult = await _contextDB.SaveChangesAsync();

                if (isResult > 0)
                {
                    _cache.Set(tiding.Id, tiding, new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromDays(1)));
                }

                _logger.LogInformation($"Добавлена новая новость - {tiding.Id}");
                return tiding;
            }
            catch(Exception ex)
            { 
                _logger.LogError(ex.Message);
                throw new Exception(ex.Message);
            }
        }


        public async Task<Tidings> UpdateTidingAppAsync(Tidings tiding)
        {
            try
            {
                var isData = await _contextDB.Tidings.FindAsync(tiding.Id);
                if (isData == null)
                {
                    throw new Exception("Новость не найден");
                }

                if (tiding.NameImage == null && tiding.UrlImage == null)
                {
                    throw new Exception("Изображение не загруженно! Добавьте изображение или url- изображения.");
                }

                isData.Header = tiding.Header;

                if (isData.NameImage != null)
                {
                    await _image.DeletImageAsync(isData.NameImage);
                }

                isData.NameImage = tiding.NameImage;

                string urlImage = null;
                if (tiding.NameImage != null)
                {
                    urlImage = "https://localhost:7297" + tiding.NameImage;
                    _logger.LogInformation($"Загружен файл - {tiding.NameImage}");
                }
                isData.UrlImage = urlImage ?? tiding.UrlImage;
                isData.Description = tiding.Description;

                int isResult = await _contextDB.SaveChangesAsync();
                if (isResult > 0)
                {
                    _cache.Remove(tiding.Id);
                    _cache.Set(tiding.Id, tiding, new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromDays(1)));
                }

                _logger.LogInformation($"Изменена новость - {tiding.Id}");
                return isData;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new Exception(ex.Message);
            }
        }


        public async Task<bool> DeleteTidingAsync(int tidId)
        {
            try
            {
                var tiding = await _contextDB.Tidings.FindAsync(tidId);
                if (tiding == null)
                {
                    _logger.LogInformation($"Запрос на удаление новости - {tidId}. Новость не была найден.");
                    throw new Exception("Новость не найден. Ошибка удаления.");
                }

                _contextDB.Tidings.Remove(tiding);

                if (tiding.NameImage != null)
                {
                    await _image.DeletImageAsync(tiding.NameImage);
                    _logger.LogInformation($"Удален файл - {tiding.NameImage}");
                }              

                int isResult = await _contextDB.SaveChangesAsync();
                if (isResult > 0)
                {
                    _cache.Remove(tiding.Id);
                }

                _logger.LogInformation($"Удаление новости - {tidId}");
                return true;               
            }
            catch(Exception ex )
            {
                _logger.LogError(ex.Message);
                throw new Exception(ex.Message);
            }
        }
    }
}
