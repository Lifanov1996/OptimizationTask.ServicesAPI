namespace ServicesAPI.Models.Tidings
{
    public class Tidings
    {
        public int Id { get; set; }
        public DateTime DateTimePublication { get; set; }
        public string Header { get; set; }
        public string NameImage { get; set; }
        public string UrlImage { get; set; }
        public string Description { get; set; } 
    }
}
