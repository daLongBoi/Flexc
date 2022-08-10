using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Flexc.Web.ViewModels
{
    public class MealsViewModel
    {
        public SelectList Meals { set; get;}
        [Required]
        [Display(Name = "Select Meal")]
        public int MealId { get; set; }
        
        [Required]
        [StringLength(100, MinimumLength =5)]
        public int Report { get; set; }
    }
}
