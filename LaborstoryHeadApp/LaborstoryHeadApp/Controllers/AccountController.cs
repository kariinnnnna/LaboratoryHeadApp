using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LaborstoryHeadApp.Controllers
{
    public class AccountController : Controller
    {
        // GET: /Account/Login
        public IActionResult Login()
        {
            return View();
        }

        // POST: /Account/Login
        [HttpPost]
        public IActionResult Login(string login, string password)
        {

            return RedirectToAction("Index", "Home");
        }
    }
}
