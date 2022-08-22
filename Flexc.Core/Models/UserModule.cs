using System;

namespace Flexc.Core.Models

{
    public class UserModule
    {
        public int Id { get; set; }
        public int Mark { get; set; }

        // Foreign key for related Student model
        public int UserId { get; set; }
        public User User { get; set; }

        // Foreign key for related Module model
        public int ModuleId { get; set; }
        public Module Module { get; set; }
    }
}
