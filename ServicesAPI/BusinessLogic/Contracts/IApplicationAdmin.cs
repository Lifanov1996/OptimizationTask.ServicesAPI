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
        Task<Applications> SeeOneApp(string appId);

        /// <summary>
        /// Получение всех заявкок
        /// </summary>
        /// <returns></returns>
        Task<List<Applications>> SeeAllApp();

        /// <summary>
        /// Изменение статуса заявки
        /// </summary>
        /// <param name="applications"></param>
        /// <returns></returns>
        Task ChangStatusApp(Applications applications);

        /// <summary>
        /// Удаление заявки 
        /// </summary>
        /// <param name="appId">Номер заявки</param>
        /// <returns></returns>
        Task DeleteApp(string appId);
    }
}
