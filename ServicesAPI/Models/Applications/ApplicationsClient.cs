using System.ComponentModel.DataAnnotations;

namespace ServicesAPI.Models.Applications
{
    public class ApplicationsClient
    {    
        [StringLength(50, MinimumLength = 3)]
        public string NameClient { get; set; }

        [StringLength(500, MinimumLength = 20)]
        public string DescriptionApp { get; set; }

        [EmailAddress]
        public string EmailClient { get; set; }
    }
}
