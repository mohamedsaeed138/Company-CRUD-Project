using Company.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Company.Controllers
{
    public class EmployeeProjectController : Controller
    {
        CompanyContext db = new CompanyContext();

        public IActionResult Index()
        {
            var empsProjects = db.EmployeesProjects.Include(x => x.Employee)
            .Include(x => x.Project).ToList();
            return View(empsProjects);
        }
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Employees = db.Employees.ToList();
            ViewBag.Projects = db.Projects.ToList();
            return View();
        }
        [HttpPost]
        public IActionResult Create(EmployeeProject empProject)
        {
            if (ModelState.IsValid)
            {

                try { db.EmployeesProjects.Add(empProject); db.SaveChanges(); }
                catch
                {
                    ViewBag.Employees = db.Employees.ToList();
                    ViewBag.Projects = db.Projects.ToList();
                    TempData["SqlException"] = $"Employee ({string.Join(" ", "SSN:", empProject.ESSN, ',', "Name:", empProject.Employee?.FName, empProject.Employee?.MName, empProject.Employee?.LName)}) already work in this location !";
                    return View(empProject);
                }
                return RedirectToAction("Index");
            }
            ViewBag.Employees = db.Employees.ToList();
            ViewBag.Projects = db.Projects.ToList();
            return View(empProject);
        }
        [HttpGet]
        public IActionResult Update(int essn, int pNo)
        {
            ViewBag.Employees = db.Employees.ToList();
            ViewBag.Projects = db.Projects.ToList();
            var empProject = db.EmployeesProjects.Find(essn, pNo);
            TempData["ESSN"] = essn;
            TempData["PNo"] = pNo;
            return View(empProject);
        }
        [HttpPost]
        public IActionResult Update(EmployeeProject updateEmpProject)
        {
            var essn = (int)TempData["ESSN"];
            var pNo = (int)TempData["PNo"];

            bool HtmlViolation = updateEmpProject.ESSN != essn || updateEmpProject.PNo != pNo;

            if (!HtmlViolation
                 && ModelState.IsValid)
            {
                db.EmployeesProjects.Update(updateEmpProject);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            if (HtmlViolation)
            {
                TempData["HtmlViolation"] = "HtmlViolation: You can't update Employee or Project values!";
                updateEmpProject.ESSN = essn;
                updateEmpProject.PNo = pNo;
            }

            ViewBag.Employees = db.Employees.ToList();
            ViewBag.Projects = db.Projects.ToList();
            TempData["ESSN"] = essn;
            TempData["PNo"] = pNo;
            return View(updateEmpProject);
        }
        public IActionResult Delete(int essn, int pNo)
        {
            var empProject = db.EmployeesProjects.Find(essn, pNo);
            db.EmployeesProjects.Remove(empProject);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
