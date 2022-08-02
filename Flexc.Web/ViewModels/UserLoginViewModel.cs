using System.ComponentModel.DataAnnotations;

namespace Flexc.Web.ViewModels
{
    
    public class UserLoginViewModel
    {       
        [Required]
        [EmailAddress]
        public string Email { get; set; }
 
        [Required]
        public string Password { get; set; }

    }
}