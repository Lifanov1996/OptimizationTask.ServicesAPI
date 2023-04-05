namespace ServicesAPI.BusinessLogic.Contracts
{
    public interface IImage
    {
        /// <summary>
        /// Добавление изображение на сервер
        /// </summary>
        /// <param name="formFile"></param>
        /// <returns></returns>
        Task<string> AddImageAsync(IFormFile formFile);

        /// <summary>
        /// Удаление изображения с сервера
        /// </summary>
        /// <param name="imagePath"></param>
        /// <returns></returns>
        Task<bool> DeletImageAsync(string imagePath);
    }
}
