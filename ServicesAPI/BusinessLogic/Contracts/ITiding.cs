using ServicesAPI.Models.Tidings;

namespace ServicesAPI.BusinessLogic.Contracts
{
    public interface ITiding
    {
        /// <summary>
        /// Получение одной новости
        /// </summary>
        /// <param name="appId">Номер прокта</param>
        /// <returns></returns>
        Task<Tidings> GetTidingAsync(int tidId);

        /// <summary>
        /// Получение всех новостей
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Tidings>> GetAllTidingsAsync();

        /// <summary>
        /// Добавление новости
        /// </summary>
        /// <param name="header"></param>
        /// <returns></returns>
        Task<Tidings> AddTidingAsync(Tidings tid);

        /// <summary>
        /// Изменение новости
        /// </summary>
        /// <param name="applications"></param>
        /// <returns></returns>
        Task<Tidings> UpdateTidingAppAsync(Tidings tid);

        /// <summary>
        /// Удаление новости 
        /// </summary>
        /// <param name="appId">Номер проекта</param>
        /// <returns></returns>
        Task<bool> DeleteTidingAsync(int tidId);

    }
}
