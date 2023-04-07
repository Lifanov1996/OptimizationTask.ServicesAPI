using Microsoft.EntityFrameworkCore;
using ServicesAPI.BusinessLogic.Contracts;
using ServicesAPI.Data.Entity;
using ServicesAPI.Models.Projects;
using static System.Net.Mime.MediaTypeNames;

namespace ServicesAPI.BusinessLogic.Services.Proejct
{
    public class Project : IProject
    {
        private readonly ContextDB _contextDB;
        private readonly IImage _image;
        private ILogger<Project> _logger;

        public Project(ContextDB contextDB, ILogger<Project> logger, IImage image)
        {
            _image = image;
            _contextDB = contextDB;
            _logger = logger;
            _logger.LogInformation("Инициализация Project");
        }


        public async Task<Projects> GetProjectAsync(int prId)
        {
            try
            {
                var project = await _contextDB.Projects.FindAsync(prId);      
                if (project == null)
                {
                    _logger.LogWarning($"Запрос на проект - {prId}. Проект не найдена");
                    throw new Exception("Проект не найден");
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
    

        public async Task<Projects> AddProjectAsync(ProjectsAdd project)
        {
            try
            {
                if(project.Image == null && project.UrlImage== null)
                {
                    throw new Exception("Изображение не загруженно! Добавьте изображение или url- изображения.");
                }

                string nameImage = null;
                string urlImage = null;

                if(project.Image != null)
                {
                    nameImage = await _image.AddImageAsync(project.Image);
                    urlImage = "https://localhost:7297" + nameImage;
                    _logger.LogInformation($"Загружен файл - {project.Image.FileName}");
                }

                Projects model = new Projects { Header = project.Header,
                                                NameImage = nameImage,
                                                UrlImage = urlImage?? project.UrlImage,
                                                Description = project.Description };

                await _contextDB.Projects.AddAsync(model);
                await _contextDB.SaveChangesAsync();
                
                _logger.LogInformation($"Добавлен новый проект - {model.Id}");
                return model;
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

                await _contextDB.SaveChangesAsync();

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

                await _contextDB.SaveChangesAsync();

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
