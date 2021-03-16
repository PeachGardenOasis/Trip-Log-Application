using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Trip_Log_Application.Models;

namespace Trip_Log_Application.Controllers
{
    public class HomeController : Controller
    {
        private TripLogContext context { get; set; }

        public HomeController(TripLogContext ctx) => context = ctx;

        public ViewResult Index()
        {
            var trips = context.Trips.OrderBy(t => t.StartDate).ToList();
            return View(trips);
        }
    }
}
