using Microsoft.AspNetCore.Http;
using ServicesAPI.BusinessLogic.Contracts;
using System.Data;

namespace ServicesAPI.BusinessLogic.Services.Image
{
    public class Image : IImage
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private string path = "/Files/";

        public Image(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }
        public async Task<string> AddImageAsync(IFormFile formFile)
        {          
            path += formFile.FileName;
            
            using (var fileStream = new FileStream(_webHostEnvironment.WebRootPath + path, FileMode.Create))
            {
                await formFile.CopyToAsync(fileStream);
            }
            return path;
        }

        public async Task<bool> DeletImageAsync(string imagePath)
        {
            try
            {
                if(File.Exists(imagePath))
                {
                    File.Delete(imagePath);
                    return true;
                }
                throw new Exception("Не верно указан путь к файлу");
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
