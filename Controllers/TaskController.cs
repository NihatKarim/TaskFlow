using Microsoft.AspNetCore.Mvc;
using Task_Web_Application.Data;
using Task_Web_Application.Models;

namespace Task_Web_Application.Controllers
{
    public class TaskController : Controller
    {
        private readonly AppDatabase _context;

        public TaskController(AppDatabase context)
        {
            _context = context;
        }

        // ================= INDEX =================
        public IActionResult Index()
        {
            var username = HttpContext.Session.GetString("Username");
            var role = HttpContext.Session.GetString("UserType");

            if (string.IsNullOrEmpty(username))
                return RedirectToAction("Login", "Data");

            if (role == "worker")
            {
                var tasks = _context.Tasks
                    .Where(x => x.AssignedTo == null)
                    .ToList();

                return View(tasks);
            }

            if (role == "user")
            {
                return RedirectToAction("MyTasks");
            }

            return View(_context.Tasks.ToList()); // admin
        }

        // ================= CREATE =================
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(TaskItem task)
        {
            var username = HttpContext.Session.GetString("Username");
            var email = HttpContext.Session.GetString("UserEmail");

            if (string.IsNullOrEmpty(username))
                return RedirectToAction("Login", "Data");

            task.CreatedBy = username;
            task.CreatedByEmail = email;

            task.AssignedTo = null;

            task.CreatedDate = DateTime.Now;
            task.Status = "Pending";

            _context.Tasks.Add(task);
            _context.SaveChanges();

            return RedirectToAction("MyTasks","Task");
        }

        // ================= ASSIGN (WORKER CLAIM TASK) =================
        [HttpPost]
        public IActionResult AssignTask(int id)
        {
            var username = HttpContext.Session.GetString("Username");
            var email = HttpContext.Session.GetString("UserEmail");

            if (string.IsNullOrEmpty(username))
                return RedirectToAction("Login", "Data");

            var task = _context.Tasks.FirstOrDefault(x => x.Id == id);

            if (task == null)
                return NotFound();

            // already taken check
            if (task.AssignedTo != null)
                return BadRequest("Task already assigned");

            task.AssignedTo = username;
            task.Status = "InProgress";

            _context.SaveChanges();

            return RedirectToAction("MyTasks");
        }

        // ================= MY TASKS (WORKER) =================
        public IActionResult MyTasks()
        {
            var username = HttpContext.Session.GetString("Username");
            var role = HttpContext.Session.GetString("UserType");

            if (string.IsNullOrEmpty(username))
                return RedirectToAction("Login", "Data");

            List<TaskItem> tasks;

            if (role == "worker")
            {
                tasks = _context.Tasks
                    .Where(x => x.AssignedTo == username)
                    .ToList();
            }
            else if (role == "user")
            {
                tasks = _context.Tasks
                    .Where(x => x.CreatedBy == username)
                    .ToList();
            }
            else // admin
            {
                tasks = _context.Tasks.ToList();
            }

            ViewBag.Username = username;

            return View(tasks);
        }

        // ================= COMPLETE TASK =================
        [HttpPost]
        public IActionResult CompleteTask(int id)
        {
            var username = HttpContext.Session.GetString("Username");

            if (string.IsNullOrEmpty(username))
                return RedirectToAction("Login", "Data");

            var task = _context.Tasks.FirstOrDefault(x => x.Id == id);

            if (task == null)
                return NotFound();

            if (task.AssignedTo != username)
                return Unauthorized();

            task.Status = "Completed";

            _context.SaveChanges();

            return RedirectToAction("MyTasks");
        }
    }
}