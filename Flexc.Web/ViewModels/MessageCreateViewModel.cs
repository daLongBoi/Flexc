using Flexc.Core.Models;


using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace Flexc.Web.ViewModels{
    public class MessageCreateViewModel
    {
        // selectlist of students (id, name)       
        public SelectList Users { set; get; }

        // Collecting UserID and ontext in Form
        [Required(ErrorMessage = "Please select a User")]
        [Display(Name = "Select User")]
        public int UserId { get; set; }

        [Required]

        [StringLength(200, MinimumLength = 5)]
        public string Context { get; set; }

          public string Name {get;set;}
    }

}