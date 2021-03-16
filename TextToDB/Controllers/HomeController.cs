using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TextToDB.Models;
using Hors;
using Hors.Models;


namespace TextToDB.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private List<DateText> dateTexts;
        private DateTextDbContext db;
        public HomeController(ILogger<HomeController> logger, DateTextDbContext context)
        {
            _logger = logger;
            db = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var recs = db.dateTexts.OrderByDescending(x => x.Id).Take(3).ToList(); ;
            ViewBag.Records = recs;
            return View();
        }
        [HttpPost]
        public IActionResult Index(DateText dt)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (dt.Date == null)
                        dt.Date = FindDate(dt.Text);
                    db.dateTexts.Add(dt);
                    db.SaveChanges();
                }
                else
                    throw new Exception("Сообщение не может быть пустым");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Text", ex.Message);
            }
            finally
            {
                var recs = db.dateTexts.OrderByDescending(x => x.Id).Take(3).ToList();
                ViewBag.Records = recs;
            }
            return View();// BadRequest(ex.Message);
        }

        private DateTime FindDate(string text)
        {
            var hors = new HorsTextParser();
            var currentDate = DateTime.Now;
            var result = hors.Parse(text, currentDate);
            if (result.Dates.Any())
                return result.Dates.FirstOrDefault().DateFrom.Date;
            else
                throw new Exception("Необходимо указать дату!");
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
