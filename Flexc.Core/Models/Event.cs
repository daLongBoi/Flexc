using System;

namespace Flexc.Core.Models
{
    // Add User roles relevant to your application

    public class Event
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string StartDate { get; set; }



        public User User { get; set; }
        public int UserId { get; set; }

    }
}