using ServicesAPI.Models.Headers;

namespace ServicesAPI.BusinessLogic.Contracts
{
    public interface IHeader
    {
        /// <summary>
        /// Прочтение хэдера
        /// </summary>
        /// <returns></returns>
        Task<Headers> GetHeaderAsync();

        /// <summary>
        /// Добавление хэдера
        /// </summary>
        /// <param name="header"></param>
        /// <returns></returns>
        Task<Headers> AddHeaderAsync(Headers header);

        /// <summary>
        /// Изменение хэдера
        /// </summary>
        /// <param name="header"></param>
        /// <returns></returns>
        Task<Headers> UpdateDescrHeaderAsync(Headers header);

        /// <summary>
        /// Удаление хэдера
        /// </summary>
        /// <param name="headers">Id хэдера</param>
        /// <returns></returns>
        Task<bool> DeletHeaderAsync(int idHead);
    }
}
