using System;
namespace Flexc.Core.Models
{
    // Add User roles relevant to your application
  
    public class Food
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int  Weight { get; set; }
        public int  Calories { get; set; }
        public string FoodPhotoUrl {get; set;}

        public int MealId { get; set;}
        public Meal Meal  {get; set; }

         //forgien key
        
           public int UserId {get;set;}

            public User User {get;set;}
      

    }
}
