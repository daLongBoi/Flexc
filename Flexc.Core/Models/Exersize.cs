using System;
namespace Flexc.Core.Models
{
  
    
    public class Exersize
    {
        public int Id { get; set; }
        
        // suitable mot attributes / relationships
      
        public string ExName { get; set; }

        public string MuscleGroup { get;set;}

        public int Reps {get;set;}

        public int Sets {get;set;}

        public int Weight {get;set;}

        public string ExPhotoUrl {get; set;}

    
       
       
        // forgein key
    
        public int workoutID{ get ; set;}
        public Workout Workout { get; set; }

         //forgien key
        
           public int UserId {get;set;}

            public User User {get;set;}

    }
}
