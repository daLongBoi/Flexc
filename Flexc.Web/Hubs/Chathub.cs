using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Flexc.Core.Models;
using Flexc.Core.Services;
using Flexc.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.SignalR;

namespace Flexc.Web.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
    }
}