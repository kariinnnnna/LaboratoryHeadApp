using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LaboratoryHeadApp.Controllers
{
    public class ScheduleController : Controller
    {

        public IActionResult LessonsRooms(DateTime? date)
        {
            var d = (date ?? DateTime.Today).Date;

            ViewBag.Date = d.ToString("yyyy-MM-dd");
            return View();
        }
        // GET: ScheduleController
        public ActionResult Index()
        {
            return View();
        }
        public IActionResult LessonsTeachers() => View();
        public IActionResult LessonsGroups() => View();
        public IActionResult Duty() => View();

        // GET: ScheduleController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ScheduleController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ScheduleController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ScheduleController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ScheduleController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ScheduleController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ScheduleController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
