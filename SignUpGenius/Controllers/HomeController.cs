using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SignUpGenius.Models;
using SignUpGenius.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace SignUpGenius.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        //Context allows us to access the database
        private SignUpDbContext _context { get; set; }

        public HomeController(ILogger<HomeController> logger, SignUpDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        //Outputs the index view
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult SignUpPage()
        {
            //Allows us to access the Day View Model to filter based on appointment time and whether or not the time it taken.
            DayViewModel dayView = new DayViewModel
            {
                Monday = _context.AppointmentTime
                    .Where(x => x.Day == "Monday" && x.Scheduled == false),
                Tuesday = _context.AppointmentTime
                    .Where(x => x.Day == "Tuesday" && x.Scheduled == false),
                Wednesday = _context.AppointmentTime
                    .Where(x => x.Day == "Wednesday" && x.Scheduled == false),
                Thursday = _context.AppointmentTime
                    .Where(x => x.Day == "Thursday" && x.Scheduled == false),
                Friday = _context.AppointmentTime
                    .Where(x => x.Day == "Friday" && x.Scheduled == false),
                Saturday = _context.AppointmentTime
                    .Where(x => x.Day == "Saturday" && x.Scheduled == false),
                Sunday = _context.AppointmentTime
                    .Where(x => x.Day == "Sunday" && x.Scheduled == false)
            };

            //Allows us to still use the Main page model on the view
            return View(new MainPageModel
            {
                dayViewModel = dayView
            });
        }

        [HttpPost]
        public IActionResult SignUpPage(FormModel appResponse)
        {
            //Gets the the time the user selected
            var appt = _context.AppointmentTime
                    .Where(x => (x.Day + ", " + x.Time) == appResponse.Time)
                    .FirstOrDefault();

            //Sets the schedule to true to make it as taken then updates the database
            appt.Scheduled = true;
            _context.AppointmentTime.Update(appt);
            _context.SaveChanges();

            //Gets the time
            string myTime = appResponse.Time;

            //Redirects to the sign up form to finalize the appointment 
            return RedirectToAction("SignUpForm", "Home", new { appTime = myTime });
        }

        //Outputs the signup form passing the application time
        [HttpGet]
        public IActionResult SignUpForm(string appTime)
        {
            FormModel tempModel = new FormModel();
            tempModel.Time = appTime;
            return View(tempModel);
        }

        //Updates the database with the new appointment
        [HttpPost]
        public IActionResult SignUpForm(FormModel appResponse)
        {
            //Validate the model
            if (ModelState.IsValid)
            {
                _context.Form.Add(appResponse);
                _context.SaveChanges();
                return View("Index", appResponse);
            }
            else
            {
                //List out the validation errors without going to another page yet
                return View(appResponse);
            }
        }

        //Passes the database information to the appointment view
        public IActionResult Appointments()
        {
            return View(_context.Form);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
