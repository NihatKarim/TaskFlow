using Microsoft.AspNetCore.Mvc;
using Task_Web_Application.Data;
using Task_Web_Application.Models;

namespace Task_Web_Application.Controllers
{
    public class DataController : Controller
    {
        public AppDatabase _db;

        public DataController(AppDatabase db)
        {
            _db = db;
        }

        // ================= REGISTER =================

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterModel obj)
        {
            if (!obj.AcceptTerms)
            {
                ModelState.AddModelError("AcceptTerms",
                    "You must accept the Terms & Conditions");
            }

            if (!ModelState.IsValid)
            {
                return View(obj);
            }

            bool emailExists = _db.registerModels
                .Any(x => x.Email.ToLower() == obj.Email.ToLower());

            if (emailExists)
            {
                ModelState.AddModelError(
                    "Email",
                    "This email is already registered."
                );

                return View(obj);
            }

            _db.registerModels.Add(obj);

            _db.SaveChanges();

            return RedirectToAction("Login");
        }


        // ================= LOGIN =================

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginModel obj)
        {
            if (ModelState.IsValid)
            {
                var user = _db.registerModels
                              .FirstOrDefault(x => x.Email == obj.Email
                                                && x.Password == obj.Password);

                if (user != null)
                {
                    HttpContext.Session.SetString("UserEmail", user.Email);
                    HttpContext.Session.SetString("Username", user.Username);
                    HttpContext.Session.SetString("UserType", user.Type);

                    if (user.Type == "admin")
                        return RedirectToAction("AdminHomepage");

                    if (user.Type == "user")
                        return RedirectToAction("UserHomepage");

                    if (user.Type == "worker")
                        return RedirectToAction("WorkerHomepage");
                }

                ViewBag.Error = "Invalid Email or Password";
            }

            return View(obj);
        }

        // ================= ADMIN HOMEPAGE =================

        public IActionResult AdminHomepage()
        {
            string type = HttpContext.Session.GetString("UserType");

            if (type != "admin")
                return RedirectToAction("Login");

            string adminUsername = HttpContext.Session.GetString("Username");

            var tasks = _db.Tasks
                           .Where(x => x.CreatedBy == adminUsername)
                           .ToList();

            int totalTasks = tasks.Count;
            int completed = tasks.Count(x => x.Status == "Completed");
            int pending = tasks.Count(x => x.Status == "Pending");
            int totalUsers = _db.registerModels.Count(x => x.Type == "user");

            double completionRate = totalTasks == 0
                ? 0
                : (double)completed / totalTasks * 100;

            var model = new AdminDashboardViewModel
            {
                TotalTasks = totalTasks,
                CompletedTasks = completed,
                PendingTasks = pending,
                TotalUsers = totalUsers,
                CompletionRate = completionRate
            };

            return View(model);
        }

        // ================= USER HOMEPAGE =================

        public IActionResult UserHomepage()
        {
            string type = HttpContext.Session.GetString("UserType");

            if (type != "user")
                return RedirectToAction("Login");

            ViewBag.Username = HttpContext.Session.GetString("Username");
            return View();
        }

        // ================= ADD TASK =================

        [HttpGet]
        public IActionResult AddTask()
        {
            string type = HttpContext.Session.GetString("UserType");

            if (type != "admin")
                return RedirectToAction("Login");

            return View();
        }

        [HttpPost]
        public IActionResult AddTask(TaskItem obj)
        {
            string type = HttpContext.Session.GetString("UserType");

            if (type != "admin")
                return RedirectToAction("Login");

            obj.CreatedBy = HttpContext.Session.GetString("Username");
            obj.CreatedByEmail = HttpContext.Session.GetString("UserEmail");
            obj.CreatedDate = DateTime.Now;
            obj.Status = "Pending";

            _db.Tasks.Add(obj);
            _db.SaveChanges();

            return RedirectToAction("AdminHomepage");
        }

        // ================= WORKER HOMEPAGE =================
        public IActionResult WorkerHomepage()
        {
            string type = HttpContext.Session.GetString("UserType");

            if (type != "worker")
                return RedirectToAction("Login");

            return View();
        }
        // ================= VIEW TASK =================

        public IActionResult ViewTask()
        {
            string type = HttpContext.Session.GetString("UserType");

            if (type == null)
                return RedirectToAction("Login");

            // expose current username to the view so UI can hide/show actions
            ViewBag.Username = HttpContext.Session.GetString("Username");

            if (type == "admin")
            {
                return View(_db.Tasks.ToList());
            }

            if (type == "user")
            {
                string username = HttpContext.Session.GetString("Username");

                var userTasks = _db.Tasks
                                   .Where(x => x.AssignedTo == username)
                                   .ToList();

                return View(userTasks);
            }

            return RedirectToAction("Login");
        }

        // ================= UPDATE STATUS (UNDO SUPPORTED) =================

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateStatus(int id)
        {
            var username = HttpContext.Session.GetString("Username");

            if (string.IsNullOrEmpty(username))
                return RedirectToAction("Login");

            var task = _db.Tasks.FirstOrDefault(x => x.Id == id);

            if (task == null)
                return NotFound();

            if (!string.Equals(task.AssignedTo?.Trim(), username.Trim(),
                StringComparison.OrdinalIgnoreCase))
                return RedirectToAction("ViewTask");

            task.Status = task.Status == "Completed" ? "Pending" : "Completed";

            _db.SaveChanges();

            return RedirectToAction("ViewTask");
        }

        // ================= COMPLETED TASKS (ADMIN - OWN ONLY) =================

        public IActionResult CompletedTasks()
        {
            var type = HttpContext.Session.GetString("UserType");

            if (type != "admin")
                return RedirectToAction("Login");

            var adminUsername = HttpContext.Session.GetString("Username");

            var completedTasks = _db.Tasks
                .Where(x => x.Status == "Completed" &&
                            x.CreatedBy == adminUsername)
                .ToList();

            return View(completedTasks);
        }

        [HttpPost]
        public IActionResult ApproveTask(int id)
        {
            string type = HttpContext.Session.GetString("UserType");

            if (type != "admin")
                return RedirectToAction("Login");

            string adminUsername = HttpContext.Session.GetString("Username");

            var task = _db.Tasks
                          .FirstOrDefault(x => x.Id == id &&
                                               x.CreatedBy == adminUsername &&
                                               x.Status == "Completed");

            if (task == null)
                return Unauthorized();

            var history = new TaskHistory
            {
                TaskId = task.Id,
                Action = "Approved",
                ChangedBy = adminUsername,
                ChangedAt = DateTime.Now
            };

            _db.TaskHistories.Add(history);

            _db.Tasks.Remove(task);

            _db.SaveChanges();

            return RedirectToAction("CompletedTasks");
        }
        public IActionResult History()
        {
            string type = HttpContext.Session.GetString("UserType");

            if (type != "admin")
                return RedirectToAction("Login");

            string adminUsername = HttpContext.Session.GetString("Username");

            var history = _db.TaskHistories
                             .Where(x => x.ChangedBy == adminUsername)
                             .OrderByDescending(x => x.ChangedAt)
                             .ToList();

            return View(history);
        }

        [HttpPost]
        public IActionResult ClearHistory()
        {
            string type = HttpContext.Session.GetString("UserType");

            if (type != "admin")
                return RedirectToAction("Login");

            string adminUsername = HttpContext.Session.GetString("Username");

            var history = _db.TaskHistories
                             .Where(x => x.ChangedBy == adminUsername)
                             .ToList();

            _db.TaskHistories.RemoveRange(history);
            _db.SaveChanges();

            return RedirectToAction("History");
        }



        // ================= LOGOUT =================


        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
