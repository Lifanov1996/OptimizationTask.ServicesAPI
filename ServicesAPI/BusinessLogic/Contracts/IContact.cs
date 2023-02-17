

using ServicesAPI.Models.Contacts;

namespace ServicesAPI.BusinessLogic.Contracts
{
    public interface IContact
    {
        /// <summary>
        /// Получение контактов
        /// </summary>
        /// <param name="appId">Номер прокта</param>
        /// <returns></returns>
        Task<Contacts> GetContactAsync();
     
        /// <summary>
        /// Добавление новости
        /// </summary>
        /// <param name="header"></param>
        /// <returns></returns>
        Task<Contacts> AddContactAsync();

        /// <summary>
        /// Изменение новости
        /// </summary>
        /// <param name="applications"></param>
        /// <returns></returns>
        Task<Contacts> UpdateContactAsync(Contacts cont);     
    }
}
