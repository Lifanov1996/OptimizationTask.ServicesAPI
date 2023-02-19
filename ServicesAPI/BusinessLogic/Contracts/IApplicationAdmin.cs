using Microsoft.AspNetCore.Mvc;
using ServicesAPI.Models.Applications;

namespace ServicesAPI.BusinessLogic.Contracts
{
    public interface IApplicationAdmin
    {
        /// <summary>
        /// Получение одной заявки
        /// </summary>
        /// <param name="appId">Номер заявки</param>
        /// <returns></returns>
        Task<Applications> GetAppAsync(int appId);

        /// <summary>
        /// Получение всех заявкок
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Applications>> GetAllAppAsync();

        /// <summary>
        /// Изменение статуса заявки
        /// </summary>
        /// <param name="applications"></param>
        /// <returns></returns>
        Task<Applications> UpdateStatusAppAsync(int appId, string status);

        /// <summary>
        /// Удаление заявки 
        /// </summary>
        /// <param name="appId">Номер заявки</param>
        /// <returns></returns>
        Task<bool> DeleteAppAsync(int appId);
    }
}
