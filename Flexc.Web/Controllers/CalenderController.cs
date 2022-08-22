using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Flexc.Web.ViewModels;

using Flexc.Core.Models;
using Flexc.Core.Services;

namespace Flexc.Web.Controllers
{
    public class CalenderController : Controller
    {

        private readonly IConfiguration _config;
        private readonly IUserService _svc;

        public CalenderController(IUserService svc, IConfiguration config)
        {
            _config = config;
            _svc = svc;
        }



        public IActionResult Calender()
        {
            ViewData["events"] = new[]
            {
                
                new Event{Id = 1, Title = "Session with Phil", StartDate = "2022-08-15"},
                 new Event{Id = 2, Title = "Session with Tony", StartDate = "2022-08-16"}
            };
            return View();
        }
    }
}