using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Flexc.Web.ViewModels
{
    public class UserModuleViewModel
    {
        // selectlist of modules (id, title)       
        public SelectList Modules { set; get; }

        public int UserId { get; set; }

        public int Id {get;set;}
       
        [Required]
        [Display(Name = "Select Module")]
        public int ModuleId { get; set; }
        
        [Range(0,100)]
        public int Mark { get; set; }
    }

}
