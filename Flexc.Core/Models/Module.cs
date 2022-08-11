using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Flexc.Core.Models
{
    public class Module
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }
            
        // Navigation property
        IList<UserModule> UserModules { get; set; } = new List<UserModule>();
    }
}
