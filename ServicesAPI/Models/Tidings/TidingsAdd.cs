namespace ServicesAPI.Models.Tidings
{
    public class TidingsAdd
    {
        public DateTime DateTimePublication { get; set; }
        public string Header { get; set; }
        public string File { get; set; }
        public string Description { get; set; }
    }
}
