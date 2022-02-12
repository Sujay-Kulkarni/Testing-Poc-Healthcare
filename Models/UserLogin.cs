using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Testing_Poc_Healthcare.Models
{
    [NotMapped]
    public class UserLogin
    {
        [Required]
        [EmailAddress(ErrorMessage ="Incorrect email address")]
        public string EmailId { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
