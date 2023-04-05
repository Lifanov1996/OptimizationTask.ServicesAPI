namespace ServicesAPI.Models.Tidings
{
    public class TidingsAdd
    {
        public string Header { get; set; }
        public IFormFile Image { get; set; }
        public string UrlImage { get; set; }
        public string Description { get; set; }
    }
}
