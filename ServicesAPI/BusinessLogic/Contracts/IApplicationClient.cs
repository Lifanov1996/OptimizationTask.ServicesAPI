using ServicesAPI.Models.Applications;

namespace ServicesAPI.BusinessLogic.Contracts
{
    public interface IApplicationClient
    {
        /// <summary>
        /// Отправка заявки клиентом
        /// </summary>
        /// <param name="NameCl">Имя клиента</param>
        /// <param name="Description">Сообщение заявки</param>
        /// <param name="Email">Элетронная почта клиента</param>
        Task<Applications> AddAppClientAsync(ApplicationsClient client);
    }
}
