namespace ServicesAPI.Models.Projects
{
    public class ProjectsAdd
    {
        public string Header { get; set; }
        public IFormFile Image { get; set; }
        public string UrlImage { get; set; }
        public string Description { get; set; }
    }
}
