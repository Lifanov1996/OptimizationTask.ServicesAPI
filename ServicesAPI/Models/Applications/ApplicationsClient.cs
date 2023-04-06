using System.ComponentModel.DataAnnotations;

namespace ServicesAPI.Models.Applications
{
    public class ApplicationsClient
    {    
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Недопустимая длина имени")]
        public string NameClient { get; set; }

        [StringLength(500, MinimumLength = 20, ErrorMessage = "Недопустимая длина сообщения")]
        public string DescriptionApp { get; set; }

        [EmailAddress(ErrorMessage = "Не верно указан Email")]
        public string EmailClient { get; set; }
    }
}
