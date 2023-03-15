

using ServicesAPI.Models.Contacts;

namespace ServicesAPI.BusinessLogic.Contracts
{
    public interface IContact
    {
        /// <summary>
        /// Получение контактной информации
        /// </summary>
        /// <param name="appId">Номер прокта</param>
        /// <returns></returns>
        Task<Contacts> GetContactAsync();
     
        /// <summary>
        /// Добавление контактной информации
        /// </summary>
        /// <param name="header"></param>
        /// <returns></returns>
        Task<Contacts> AddContactAsync();

        /// <summary>
        /// Изменение контактной информации
        /// </summary>
        /// <param name="applications"></param>
        /// <returns></returns>
        Task<Contacts> UpdateContactAsync(Contacts cont);     
    }
}
