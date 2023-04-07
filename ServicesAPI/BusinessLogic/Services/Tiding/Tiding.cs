using Microsoft.EntityFrameworkCore;
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

        public Tiding(ContextDB contextDB, ILogger<Tiding> logger, IImage image) 
        {
            _image = image;
            _contextDB = contextDB;
            _logger = logger;
            _logger.LogInformation("Инициализирован Tiding");
        }


        public async Task<Tidings> GetTidingAsync(int tidId)
        {
            try
            {
                var result = await _contextDB.Tidings.FindAsync(tidId);     
                if(result == null)
                {
                    _logger.LogWarning($"Запрос на новость - {tidId}. Новость не найдена");
                    throw new Exception("Новость не найдена");
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


        public async Task<Tidings> AddTidingAsync(TidingsAdd tid)
        {
            try
            {
                if (tid.Image == null && tid.UrlImage == null)
                {
                    throw new Exception("Изображение не загруженно! Добавьте изображение или url- изображения.");
                }

                string nameImage = null;
                string urlImage = null;
                if (tid.Image != null)
                {
                    nameImage = await _image.AddImageAsync(tid.Image);
                    urlImage = "https://localhost:7297" + nameImage;
                    _logger.LogInformation($"Загружен файл - {tid.Image.FileName}");
                }

                Tidings model = new Tidings() { DateTimePublication = DateTime.Now,
                                                Header = tid.Header,
                                                NameImage = nameImage,
                                                UrlImage = urlImage?? tid.UrlImage,
                                                Description = tid.Description };

                await _contextDB.Tidings.AddAsync(model);
                await _contextDB.SaveChangesAsync();

                _logger.LogInformation($"Добавлена новая новость - {model.Id}");
                return model;
            }
            catch(Exception ex)
            { 
                _logger.LogError(ex.Message);
                throw new Exception(ex.Message);
            }
        }


        public async Task<Tidings> UpdateTidingAppAsync(Tidings tid)
        {
            try
            {
                var isData = await _contextDB.Tidings.FindAsync(tid.Id);
                if (isData == null)
                {
                    throw new Exception("Новость не найден");
                }

                if (tid.NameImage == null && tid.UrlImage == null)
                {
                    throw new Exception("Изображение не загруженно! Добавьте изображение или url- изображения.");
                }

                isData.Header = tid.Header;

                if (isData.NameImage != null)
                {
                    await _image.DeletImageAsync(isData.NameImage);
                }

                isData.NameImage = tid.NameImage;

                string urlImage = null;
                if (tid.NameImage != null)
                {
                    urlImage = "https://localhost:7297" + tid.NameImage;
                    _logger.LogInformation($"Загружен файл - {tid.NameImage}");
                }
                isData.UrlImage = urlImage ?? tid.UrlImage;
                isData.Description = tid.Description;

                await _contextDB.SaveChangesAsync();

                _logger.LogInformation($"Изменена новость - {tid.Id}");
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
                var result = await _contextDB.Tidings.FindAsync(tidId);
                if (result == null)
                {
                    _logger.LogInformation($"Запрос на удаление новости - {tidId}. Новость не была найден.");
                    throw new Exception("Новость не найден. Ошибка удаления.");
                }

                _contextDB.Tidings.Remove(result);

                if (result.NameImage != null)
                {
                    await _image.DeletImageAsync(result.NameImage);
                    _logger.LogInformation($"Удален файл - {result.NameImage}");
                }              

                await _contextDB.SaveChangesAsync();

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
