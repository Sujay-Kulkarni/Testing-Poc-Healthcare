using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Testing_Poc_Healthcare.Models
{
    [NotMapped]
    public class UserLogin
    {
        [Required, DefaultValue("admin@gmail.com")]
        [EmailAddress(ErrorMessage ="Incorrect email address")]
        public string EmailId { get; set; }
        [Required,DefaultValue("admin123")]
        public string Password { get; set; }
    }
}
