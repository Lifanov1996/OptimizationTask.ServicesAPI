using ServicesAPI.Models.Projects;

namespace ServicesAPI.BusinessLogic.Contracts
{
    public interface IProject
    {
        /// <summary>
        /// Получение одного проекта
        /// </summary>
        /// <param name="appId">Номер прокта</param>
        /// <returns></returns>
        Task<Projects> GetProjectAsync(int prId);

        /// <summary>
        /// Получение всех проектов
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Projects>> GetAllProjectsAsync();

        /// <summary>
        /// Изменение проекта
        /// </summary>
        /// <param name="applications"></param>
        /// <returns></returns>
        Task<Projects> UpdateProjectAppAsync(Projects project);

        /// <summary>
        /// Удаление заявки 
        /// </summary>
        /// <param name="appId">Номер проекта</param>
        /// <returns></returns>
        Task<bool> DeleteAppAsync(int prId);
    }
}
