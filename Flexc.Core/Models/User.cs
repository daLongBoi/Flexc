using System;
namespace Flexc.Core.Models
{
    // Add User roles relevant to your application
    public enum Role { admin, manager, user, guest }
    
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        // User role within application
        public Role Role { get; set; }

    }
}
