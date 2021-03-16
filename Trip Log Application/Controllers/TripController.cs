using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Trip_Log_Application.Models;

namespace Trip_Log_Application.Controllers
{
    public class TripController : Controller
    {
        private TripLogContext context { get; set; }

        public TripController(TripLogContext ctx) => context = ctx;
        public RedirectToActionResult Cancel() //cancel operation
        {
            TempData.Clear(); // purge temp data
            return RedirectToAction("Index","Home");
        }
        [HttpGet]
        public ViewResult Add(string id="") // add action/get responding to is proucing the models that are viewed on pages 1,2,3
        {
            var vm = new TripViewModel();

            if (id == "Page2")
            {
                var accomodation = TempData[nameof(Trip.Accomodation)]?.ToString(); // where we store accomodation after page 1 submitted
                if (string.IsNullOrEmpty(accomodation))
                {
                    vm.PageNumber = 3;
                    var destination = TempData[nameof(Trip.Destination)].ToString();
                    vm.Trip = new Trip { Destination = destination };
                    return View("Add3", vm);
                }
                else
                {
                    vm.PageNumber = 2;
                    vm.Trip = new Trip { Accomodation = accomodation };
                    TempData.Keep(nameof(Trip.Accomodation));
                    return View("Add2", vm);
                }
            }
            else if(id == "Page3")
            {
                vm.PageNumber = 3;
                vm.Trip = new Trip { Destination = TempData.Peek(nameof(Trip.Destination)).ToString() };
                return View("Add3", vm);
            }
            else
            {
                vm.PageNumber = 1;
                return View("Add1", vm);
            }
        }
        [HttpPost]
        public IActionResult Add(TripViewModel vm) // add action responding to the submission of the forms
        {
            if(vm.PageNumber==1)
            {
                if (ModelState.IsValid)
                {
                    TempData[nameof(Trip.Destination)] = vm.Trip.Destination;
                    TempData[nameof(Trip.Accomodation)] = vm.Trip.Accomodation;
                    TempData[nameof(Trip.StartDate)] = vm.Trip.StartDate;
                    TempData[nameof(Trip.EndDate)] = vm.Trip.EndDate;
                    return RedirectToAction("Add", new { id = "Page2" });
                }
                else
                {
                    return View("Add1",vm);
                }
            }
            else if (vm.PageNumber == 2)
            {
                TempData[nameof(Trip.AccomodationPhone)] = vm.Trip.AccomodationPhone;
                TempData[nameof(Trip.AccomodationEmail)] = vm.Trip.AccomodationEmail;
                return RedirectToAction("Add", new { id = "Page3" });

            }
            else if (vm.PageNumber == 3)
            {
                vm.Trip.Destination= TempData[nameof(Trip.Destination)].ToString();
                vm.Trip.Accomodation = TempData[nameof(Trip.Accomodation)].ToString();
                vm.Trip.StartDate = (DateTime)TempData[nameof(Trip.StartDate)];
                vm.Trip.EndDate = (DateTime)TempData[nameof(Trip.EndDate)];
                vm.Trip.AccomodationPhone = TempData[nameof(Trip.AccomodationPhone)].ToString();
                vm.Trip.AccomodationEmail = TempData[nameof(Trip.AccomodationEmail)].ToString();

                context.Trips.Add(vm.Trip);
                context.SaveChanges();
                TempData["message"] = $"Trip to {vm.Trip.Destination} added";
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
    }
}
