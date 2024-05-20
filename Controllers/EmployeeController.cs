using Company.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Company.Controllers
{
    public class EmployeeController : Controller
    {
        CompanyContext db = new CompanyContext();
        public IActionResult Index()
        {
            var emps = db.Employees.Include(x => x.Department).Include(x => x.Supervisor).ToList();
            return View(emps);
        }
        public IActionResult Projects(int ssn)
        {
            var emp = db.Employees.Find(ssn);
            var empsProjects = db.EmployeesProjects.Include(x => x.Project)
             .Include(x => x.Employee)
             .Where(x => x.ESSN == ssn)
             .ToList();
            ViewBag.GroupTitle = $" of Employee ({string.Join(separator: " ", "SSN:", emp.SSN, ",", "Name:", emp.FName, emp.MName, emp.LName)})";
            return View("~/Views/EmployeeProject/Index.cshtml", empsProjects);
        }
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Departments = db.Departments.ToList();
            ViewBag.Supervisors = db.Employees.ToList();
            return View();
        }
        [HttpPost]
        public IActionResult Create(Employee emp)
        {
            if (ModelState.IsValid)
            {

                try { db.Employees.Add(emp); db.SaveChanges(); }
                catch
                {
                    ViewBag.Departments = db.Departments.ToList();
                    ViewBag.Supervisors = db.Employees.ToList();
                    TempData["SqlException"] = $"there is an Employee already has The SSN: {emp.SSN}";

                    return View(emp);
                }


                return RedirectToAction("Index");
            }
            ViewBag.Departments = db.Departments.ToList();
            ViewBag.Supervisors = db.Employees.ToList();
            return View(emp);
        }
        [HttpGet]
        public IActionResult Update(int ssn)
        {
            ViewBag.Departments = db.Departments.ToList();
            ViewBag.Supervisors = db.Employees.ToList();
            var emp = db.Employees.Find(ssn);
            TempData["SSN"] = ssn;
            return View(emp);
        }
        [HttpPost]
        public IActionResult Update(Employee updateEmp)
        {
            var ssn = (int)TempData["SSN"];
            bool HtmlViolation = updateEmp.SSN != ssn;
            if (!HtmlViolation && ModelState.IsValid)
            {
                db.Employees.Update(updateEmp);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            if (HtmlViolation)
            {
                TempData["HtmlViolation"] = "HtmlViolation: You can't update SSN value!";
                updateEmp.SSN = ssn;
            }

            ViewBag.Departments = db.Departments.ToList();
            ViewBag.Supervisors = db.Employees.ToList();
            TempData["SSN"] = ssn;
            return View(updateEmp);
        }
        public IActionResult Delete(int ssn)
        {
            var emp = db.Employees.Find(ssn);
            var supervisedEmps = db.Employees.Where(x => x.SupervisorSSN == ssn).ToList();
            foreach (var e in supervisedEmps)
            {
                e.SupervisorSSN = null;
            }
            db.Employees.UpdateRange(supervisedEmps);
            db.Employees.Remove(emp);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
