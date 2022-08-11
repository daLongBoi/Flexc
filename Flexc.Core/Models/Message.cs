using System;

namespace Flexc.Core.Models
{
    // Add User roles relevant to your application
  public enum TicketRange { OPEN, READ, ALL }
    public class Message
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
        public string Creator { get; set; }

        public string Context {get;set;}

        public string Resolution {get;set;}
         public DateTime MessageTime { get ; set;} = DateTime.Now;

          public DateTime CreatedOn { get; set; } = DateTime.Now;
        public DateTime RepliedOn { get; set; } = DateTime.MinValue;
        
        public bool Active { get; set; } = true;

        // ticket owned by a student
        public int UserId { get; set; }      // foreign key

        public User User {get; set;}// navigation property
        
       
        public Meal Meal { get; set; }    // navigation property

         public Workout Workout { get; set; }    // navigation property







    }
}