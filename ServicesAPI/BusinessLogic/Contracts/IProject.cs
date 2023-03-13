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
        /// Добавление проекта
        /// </summary>
        /// <param name="header"></param>
        /// <returns></returns>
        Task<Projects> AddProjectAsync(ProjectsAdd project);

        /// <summary>
        /// Изменение проекта
        /// </summary>
        /// <param name="applications"></param>
        /// <returns></returns>
        Task<Projects> UpdateProjectAsync(Projects project);

        /// <summary>
        /// Удаление заявки 
        /// </summary>
        /// <param name="appId">Номер проекта</param>
        /// <returns></returns>
        Task<bool> DeleteProjectAsync(int prId);
    }
}
