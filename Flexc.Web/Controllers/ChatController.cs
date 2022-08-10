using System;
using System.Linq;
using System.Security.Claims;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Flexc.Core.Models;
using Flexc.Core.Services;
using Flexc.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

using Microsoft.AspNetCore.Authorization;

using Microsoft.AspNetCore.Mvc;

namespace Flexc.Web.Controllers
{
    public class ChatController : BaseController{

    
     public IActionResult Index()
        {
            return View();
        }
    }
}
