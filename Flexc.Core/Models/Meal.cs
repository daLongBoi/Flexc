using System;
namespace Flexc.Core.Models
{
    // Add User roles relevant to your application
  
    public class Meal
    {
         public int Id { get; set; }// primary key
        
        // suitable vehicle properties/relationships
        
        public string Name { get; set; }

        public string mealtime {get; set;}
        public int TotalCalories { get; set; }

        public string PhotoUrl {get; set;}
         public DateTime DateMeal { get ; set;} = DateTime.Now;

         public List<Food> Foods {get; set;}
                         =new List<Food>();




        


    }
}