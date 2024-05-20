using Company.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Company.Controllers
{
    public class DepartmentController : Controller
    {
        CompanyContext db = new CompanyContext();

        public IActionResult Index()
        {
            var depts = db.Departments.Include(x => x.Manager).ToList();

            return View(depts);

        }
        public IActionResult Projects(int number)
        {
            var dept = db.Departments.Find(number);
            var emps = db.Projects.Include(x => x.Department)
            .Where(x => x.DNum == number)
            .ToList();
            ViewBag.GroupTitle = $" of Department ({string.Join(" ", "Number:", dept.Number, ',', "Name:", dept.Name)})";
            return View("~/Views/Project/Index.cshtml", emps);
        }
        public IActionResult Employees(int number)
        {
            var dept = db.Departments.Find(number);
            var emps = db.Employees.Include(x => x.Department)
            .Include(x => x.Supervisor)
            .Where(x => x.DNo == number)
            .ToList();
            ViewBag.GroupTitle = $" of Department ({string.Join(" ", "Number:", dept.Number, ',', "Name:", dept.Name)})";
            return View("~/Views/Employee/Index.cshtml", emps);
        }
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Managers = db.Employees.ToList();
            return View();
        }
        [HttpPost]
        public IActionResult Create(Department dept)
        {
            bool dependencyViolation = ModelState.IsValid && dept.ManagerSSN == null && dept.MGRStartDate != null;
            var htmlViolation = dept.Number != 0;

            if (ModelState.IsValid && !dependencyViolation && !htmlViolation)
            {
                var missingMGRStartDate = dept.ManagerSSN != null && dept.MGRStartDate == null;

                if (missingMGRStartDate)
                    dept.MGRStartDate = DateTime.Now;

                db.Departments.Add(dept);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            if (dependencyViolation)
                TempData["DependencyViolation"] = "You Should Select a Manager too.";
            if (htmlViolation)
            {
                dept.Number = 0;
                TempData["HtmlViolation"] = "HtmlViolation: You can't fill Department Number value manually !";
            }
            ViewBag.Managers = db.Employees.ToList();
            return View(dept);
        }
        [HttpGet]
        public IActionResult Update(int number)
        {
            ViewBag.Managers = db.Employees.ToList();
            var dept = db.Departments.Find(number);
            TempData["Number"] = number;
            return View(dept);
        }
        [HttpPost]
        public IActionResult Update(Department updateDept)
        {
            var number = (int)TempData["Number"];
            bool htmlViolation = updateDept.Number != number;
            bool dependencyViolation = updateDept.ManagerSSN == null && updateDept.MGRStartDate != null;

            if (!dependencyViolation && !htmlViolation && ModelState.IsValid)
            {
                if (updateDept.ManagerSSN != null && updateDept.MGRStartDate == null)
                    updateDept.MGRStartDate = DateTime.Now;
                db.Departments.Update(updateDept);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            if (dependencyViolation)
                TempData["DependencyViolation"] = "You Should Select a Manager too.";
            if (htmlViolation)
            {
                TempData["HtmlViolation"] = "HtmlViolation: You can't update Department Number value!";
                updateDept.Number = number;
            }

            ViewBag.Managers = db.Employees.ToList();
            TempData["Number"] = number;
            return View(updateDept);
        }
        public IActionResult Delete(int number)
        {
            var dept = db.Departments.Find(number);
            var emps = db.Employees.Where(x => x.DNo == number).ToList();
            var projects = db.Projects.Where(x => x.DNum == number).ToList();
            foreach (var e in emps)
                e.DNo = null;
            foreach (var p in projects)
                p.DNum = null;

            db.Employees.UpdateRange(emps);
            db.Departments.Remove(dept);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}

