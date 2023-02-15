using ServicesAPI.Models.Offices;

namespace ServicesAPI.BusinessLogic.Contracts
{
    public interface IOffice
    {
        /// <summary>
        /// Получени одной услуги
        /// </summary>
        /// <param name="prId"></param>
        /// <returns></returns>
        Task<Offices> GetOfficeAsync(int offId);

        /// <summary>
        /// Получение всех услуг
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Offices>> GetAllOfficesAsync();

        /// <summary>
        /// Изменение услуги
        /// </summary>
        /// <param name="applications"></param>
        /// <returns></returns>
        Task<Offices> UpdateOfficeAppAsync(Offices office);

        /// <summary>
        /// Удаление услуги 
        /// </summary>
        /// <param name="appId">Номер проекта</param>
        /// <returns></returns>
        Task<bool> DeleteOfficeAsync(int offId);
    }
}
