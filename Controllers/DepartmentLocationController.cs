using Company.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Company.Controllers
{
    public class DepartmentLocationController : Controller
    {
        CompanyContext db = new CompanyContext();
        public IActionResult Index(string? location)
        {
            var locations = db.DepartmentsLocations.Include(x => x.Department);
            if (string.IsNullOrEmpty(location))
                return View(locations.ToList());
            TempData["Location"] = location;
            return View(locations.Where(x => x.Location.Contains(location)).ToList());
        }
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Departments = db.Departments.ToList();
            return View();
        }
        [HttpPost]
        public IActionResult Create(DepartmentLocation deptloc)
        {
            if (ModelState.IsValid)
            {

                try { db.DepartmentsLocations.Add(deptloc); db.SaveChanges(); }
                catch
                {
                    ViewBag.Departments = db.Departments.ToList();
                    TempData["SqlException"] = $"Department {string.Join(" ", "SSN: ", deptloc.DNumber, ',', "Name: ", deptloc.Department?.Name)} already has this location !";
                    return View(deptloc);
                }
                return RedirectToAction("Index");
            }
            ViewBag.Departments = db.Departments.ToList();
            return View(deptloc);
        }

        public IActionResult Delete(int dNumber, string location)
        {
            var deptloc = db.DepartmentsLocations.Find(dNumber, location);
            db.DepartmentsLocations.Remove(deptloc);
            db.SaveChanges();
            return RedirectToAction("Index", TempData["Locations"]);
        }
    }
}
