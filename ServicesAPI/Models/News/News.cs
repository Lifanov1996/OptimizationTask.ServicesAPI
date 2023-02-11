namespace ServicesAPI.Models.News
{
    public class News
    {
        public int Id { get; set; }
        public DateTime DateTimePublication { get; set; }
        public string Header { get; set; }
        public string File { get; set; }
        public string Description { get; set; } 
    }
}
