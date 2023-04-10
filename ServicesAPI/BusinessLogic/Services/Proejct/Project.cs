using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using ServicesAPI.BusinessLogic.Contracts;
using ServicesAPI.BusinessLogic.Services.Office;
using ServicesAPI.Data.Entity;
using ServicesAPI.Models.Offices;
using ServicesAPI.Models.Projects;
using static System.Net.Mime.MediaTypeNames;

namespace ServicesAPI.BusinessLogic.Services.Proejct
{
    public class Project : IProject
    {
        private readonly ContextDB _contextDB;
        private readonly IImage _image;
        private ILogger<Project> _logger;
        private readonly IMemoryCache _cache;

        public Project(ContextDB contextDB, ILogger<Project> logger, IImage image, IMemoryCache memoryCache)
        {
            _image = image;
            _contextDB = contextDB;
            _logger = logger;
            _cache = memoryCache;
            _logger.LogInformation("Инициализация Project");
        }


        public async Task<Projects> GetProjectAsync(int prId)
        {
            try
            {
                Projects project = null;
                if(!_cache.TryGetValue(prId, out project))
                {
                    project = await _contextDB.Projects.FindAsync(prId);      

                    if (project == null)
                    {
                        _logger.LogWarning($"Запрос на проект - {prId}. Проект не найдена");
                        throw new Exception("Проект не найден");
                    }
                    _cache.Set(project.Id, project, new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromDays(1)));
                }
                return project;               
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<Projects>> GetAllProjectsAsync()
        {
            return await _contextDB.Projects.AsNoTracking().ToListAsync();          
        }
    

        public async Task<Projects> AddProjectAsync(ProjectsAdd projectAdd)
        {
            try
            {
                if(projectAdd.Image == null && projectAdd.UrlImage== null)
                {
                    throw new Exception("Изображение не загруженно! Добавьте изображение или url- изображения.");
                }

                string nameImage = null;
                string urlImage = null;

                if(projectAdd.Image != null)
                {
                    nameImage = await _image.AddImageAsync(projectAdd.Image);
                    urlImage = "https://localhost:7297" + nameImage;
                    _logger.LogInformation($"Загружен файл - {projectAdd.Image.FileName}");
                }

                Projects project = new Projects { Header = projectAdd.Header,
                                                NameImage = nameImage,
                                                UrlImage = urlImage?? projectAdd.UrlImage,
                                                Description = projectAdd.Description };

                await _contextDB.Projects.AddAsync(project);
                int isResult = await _contextDB.SaveChangesAsync();

                if (isResult > 0)
                {
                    _cache.Set(project.Id, project, new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromDays(1)));
                }

                _logger.LogInformation($"Добавлен новый проект - {project.Id}");
                return project;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new Exception(ex.Message);
            }
        }


        public async Task<Projects> UpdateProjectAsync(Projects project)
        {
            try
            {
                var isData = await _contextDB.Projects.FindAsync(project.Id);
                if (isData == null)
                {
                    throw new Exception("Проект не найден");
                }

                if (project.NameImage == null && project.UrlImage == null)
                {
                    throw new Exception("Изображение не загруженно! Добавьте изображение или url- изображения.");
                }

                isData.Header = project.Header;

                if(isData.NameImage != null)
                {
                    await _image.DeletImageAsync(isData.NameImage);
                }

                isData.NameImage = project.NameImage;

                string urlImage = null;
                if (project.NameImage != null)
                {
                    urlImage = "https://localhost:7297" + project.NameImage;
                    _logger.LogInformation($"Загружен файл - {project.NameImage}");
                }
                isData.UrlImage = urlImage?? project.UrlImage;
                isData.Description = project.Description;

                int isResult = await _contextDB.SaveChangesAsync();
                if (isResult > 0)
                {
                    _cache.Remove(project.Id);
                    _cache.Set(project.Id, project, new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromDays(1)));
                }

                _logger.LogInformation($"Изменен проект - {project.Id}");
                return project;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new Exception(ex.Message);
            }
        }


        public async Task<bool> DeleteProjectAsync(int prId)
        {
            try
            {             
                var project = await _contextDB.Projects.FindAsync(prId);

                if (project == null)
                {
                    _logger.LogInformation($"Запрос на удаление проекта - {prId}. Проект не был найден.");
                    throw new Exception("Проект не найден. Ошибка удаления.");
                }
               
                _contextDB.Projects.Remove(project);

                if(project.NameImage != null)
                {
                    await _image.DeletImageAsync(project.NameImage);
                    _logger.LogInformation($"Удален файл - {project.NameImage}");
                }

                int isResult = await _contextDB.SaveChangesAsync();
                if (isResult > 0)
                {
                    _cache.Remove(project.Id);                 
                }

                _logger.LogInformation($"Удален проект - {prId}");
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
