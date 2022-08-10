using Flexc.Core.Models;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
namespace Flexc.Web.ViewModels
{
    public class MessageSearchViewModel
    {
        // result set
        public IList<Message> Messages { get; set;} = new List<Message>();

        // search options        
        public string Query { get; set; } = "";
        public TicketRange Range { get; set; } = TicketRange.OPEN;
    }
}
