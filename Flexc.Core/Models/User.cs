using System;
namespace Flexc.Core.Models
{
    // Add User roles relevant to your application
    public enum Role { admin, manager, user, guest }
    
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";

        public int targetId {get;set;}
        public int Grade {get;set;}
        
        
        public string Email { get; set; }
        public string Password { get; set; }

        public string PhotoUrl {get;set;}

        // User role within application
        public Role Role { get; set; }
         public List<Message> Messages {get; set;}
                         =new List<Message>();
        public List<Workout> Workouts {get; set;}
                        =new List<Workout>();


         // Relationship M:M Students - Modules
        public IList<UserModule> UserModules {get; set;} = new List<UserModule>();

        public List<Meal> Meals {get; set;}
                        =new List<Meal>();
        public List<Food> Foods {get; set;}
                        =new List<Food>();

    }
}
