using Company.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Company.Controllers
{
    public class ProjectController : Controller
    {
        CompanyContext db = new CompanyContext();
        public IActionResult Index()
        {
            var projects = db.Projects.Include(x => x.Department).ToList();
            return View(projects);
        }
        public IActionResult Employees(int number)
        {
            var project = db.Projects.Find(number);
            var empsProjects = db.EmployeesProjects.Include(x => x.Project)
            .Include(x => x.Employee)
            .Where(x => x.PNo == number)
            .ToList();
            ViewBag.GroupTitle = $" of Project ({string.Join(" ", "Number:", project?.Number, ',', "Name:", project?.Name)})";
            return View("~/Views/EmployeeProject/Index.cshtml", empsProjects);
        }
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Departments = db.Departments.ToList();
            return View();
        }
        [HttpPost]
        public IActionResult Create(Project project)
        {
            var htmlViolation = project.Number != 0;

            if (ModelState.IsValid && !htmlViolation)
            {

                db.Projects.Add(project);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            if (htmlViolation)
            {
                project.Number = 0;
                TempData["HtmlViolation"] = "HtmlViolation: You can't fill Project Number value manually !";
            }
            ViewBag.Departments = db.Departments.ToList();
            return View(project);
        }
        [HttpGet]
        public IActionResult Update(int number)
        {
            ViewBag.Departments = db.Departments.ToList();
            var projcet = db.Projects.Find(number);
            TempData["Number"] = number;
            return View(projcet);
        }
        [HttpPost]
        public IActionResult Update(Project updateProject)
        {
            var number = (int)TempData["Number"];
            bool htmlViolation = updateProject.Number != number;

            if (!htmlViolation && ModelState.IsValid)
            {
                db.Projects.Update(updateProject);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            if (htmlViolation)
            {
                TempData["HtmlViolation"] = "HtmlViolation: You can't update Project Number value!";
                updateProject.Number = number;
            }

            ViewBag.Departments = db.Departments.ToList();
            TempData["Number"] = number;
            return View(updateProject);
        }
        public IActionResult Delete(int number)
        {
            var project = db.Projects.Find(number);
            db.Projects.Remove(project);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}

