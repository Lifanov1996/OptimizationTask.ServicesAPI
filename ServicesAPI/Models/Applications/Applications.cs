namespace ServicesAPI.Models.Applications
{
    public class Applications
    {
        public int Id { get; set; }
        public DateTime DateTimeCreatApp { get; set; }
        public string NameClient { get; set; }
        public string DescriptionApp { get; set; }
        public string StatusApp { get; set; }
        public string EmailClient { get; set; }
    }
}
