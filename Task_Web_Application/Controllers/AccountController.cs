using Microsoft.AspNetCore.Mvc;
using Task_Web_Application.Data;
using Task_Web_Application.Models;

namespace Task_Web_Application.Controllers
{
    public class AccountController : Controller
    {
        private readonly AppDatabase _db;

        public AccountController(AppDatabase db)
        {
            _db = db;
        }
        [HttpGet]
        public IActionResult ChangePassword()
        {
            if (HttpContext.Session.GetString("UserEmail") == null)
                return RedirectToAction("Login", "Data");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ChangePassword(string currentPassword, string newPassword)
        {
            string email = HttpContext.Session.GetString("UserEmail");

            if (email == null)
                return RedirectToAction("Login", "Data");

            var user = _db.registerModels.FirstOrDefault(x => x.Email == email);

            if (user == null)
                return RedirectToAction("Login", "Data");

            if (user.Password != currentPassword)
            {
                ViewBag.Error = "Current password is incorrect";
                return View();
            }

            user.Password = newPassword;
            _db.SaveChanges();

            return RedirectToAction("Login", "Data");
        }
        [HttpGet]
        public IActionResult DeleteAccount()
        {
            if (HttpContext.Session.GetString("UserEmail") == null)
                return RedirectToAction("Login", "Data");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteAccount(string password)
        {
            string email = HttpContext.Session.GetString("UserEmail");
            string username = HttpContext.Session.GetString("Username");

            if (email == null)
                return RedirectToAction("Login", "Data");

            var user = _db.registerModels.FirstOrDefault(x => x.Email == email);

            if (user == null)
                return RedirectToAction("Login", "Data");

            if (user.Password != password)
            {
                ViewBag.Error = "Incorrect password.";
                return View();
            }

            // 🧹 NEW TASK MODEL CLEANUP
            var tasks = _db.Tasks
                .Where(x => x.CreatedBy == username || x.AssignedTo == username)
                .ToList();

            _db.Tasks.RemoveRange(tasks);

            // 🧹 HISTORY CLEANUP (updated field assumption)
            var history = _db.TaskHistories
                .Where(x => x.ChangedBy == username)
                .ToList();

            _db.TaskHistories.RemoveRange(history);

            // 🗑 Delete user
            _db.registerModels.Remove(user);

            _db.SaveChanges();  

            HttpContext.Session.Clear();

            return RedirectToAction("Login", "Data");
        }

    }
}