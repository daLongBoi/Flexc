using System;
namespace Flexc.Core.Models
{
    // Add User roles relevant to your application
  
    public class Workout
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Creator { get; set; }
         public DateTime DateWorkout { get ; set;} = DateTime.Now;

         public List<Exersize> Exersizes {get; set;}
                         =new List<Exersize>();
        
        //forgien key
            public int UserId {get;set;}

            public User User {get;set;}

                         



    }
}