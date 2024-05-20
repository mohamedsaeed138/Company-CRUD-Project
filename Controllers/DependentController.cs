using Company.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Company.Controllers
{
    public class DependentController : Controller
    {
        CompanyContext db = new CompanyContext();
        public IActionResult Index()
        {
            var dependents = db.Dependents.Include(x => x.Employee).ToList();
            return View(dependents);
        }
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Employees = db.Employees.ToList();
            return View();
        }
        [HttpPost]
        public IActionResult Create(Dependent dependent)
        {
            if (ModelState.IsValid)
            {

                try { db.Dependents.Add(dependent); db.SaveChanges(); }
                catch
                {
                    ViewBag.Employees = db.Employees.ToList();
                    TempData["SqlException"] = $"The Employee {string.Join(" ", "SSN:", dependent.ESSN, ",", "Name:", dependent.Employee?.FName, dependent.Employee?.MName, dependent.Employee?.LName)} already has dependent with the same name:{dependent.Name} !";
                    return View(dependent);
                }
                return RedirectToAction("Index");
            }
            ViewBag.Employees = db.Employees.ToList();
            return View(dependent);
        }
        [HttpGet]
        public IActionResult Update(int essn, string name)
        {
            ViewBag.Employees = db.Employees.ToList();
            var dependent = db.Dependents.Find(essn, name);
            TempData["ESSN"] = essn;
            TempData["Name"] = name;
            return View(dependent);
        }
        [HttpPost]
        public IActionResult Update(Dependent updateDependent)
        {
            var essn = (int)TempData["ESSN"];
            var name = (string)TempData["Name"];

            bool HtmlViolation = updateDependent.ESSN != essn || updateDependent.Name != name;

            if (!HtmlViolation
                 && ModelState.IsValid)
            {
                db.Dependents.Update(updateDependent);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            if (HtmlViolation)
            {
                TempData["HtmlViolation"] = "HtmlViolation: You can't update Name or Employee SSN values!";
                updateDependent.ESSN = essn;
                updateDependent.Name = name;
            }

            ViewBag.Employees = db.Employees.ToList();
            TempData["ESSN"] = essn;
            TempData["Name"] = name;

            return View(updateDependent);
        }
        public IActionResult Delete(int essn, string name)
        {
            var dependent = db.Dependents.Find(essn, name);
            db.Dependents.Remove(dependent);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
