using ServicesAPI.Models.Applications;

namespace ServicesAPI.BusinessLogic.Contracts
{
    public interface IApplicationClient
    {
        /// <summary>
        /// Создание заявки клиентом
        /// </summary>
        /// <param name="NameCl">Имя клиента</param>
        /// <param name="Description">Сообщение заявки</param>
        /// <param name="Email">Элетронная почта клиента</param>
        Task<Guid> AddAppClientAsync(ApplicationsClient client);


        /// <summary>
        /// Получение информации по заявке
        /// </summary>
        /// <param name="numberApp"></param>
        /// <returns></returns>
        Task<IQueryable> GetAppAsync(string numberApp);
    }
}
