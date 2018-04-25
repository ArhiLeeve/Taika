using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MobileBackend.DataAccess;

namespace MobileBackend.Controllers
{
    public class ManageDepartmentsController : Controller
    {
        private TimesheetEntities db = new TimesheetEntities();

        // GET: ManageDepartments
        public async Task<ActionResult> Index()
        {
            var departments = db.Departments.Include(d => d.Employees).Include(d => d.Kustannuspaikat);
            return View(await departments.ToListAsync());
        }

        // GET: ManageDepartments/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Departments departments = await db.Departments.FindAsync(id);
            if (departments == null)
            {
                return HttpNotFound();
            }
            return View(departments);
        }

        // GET: ManageDepartments/Create
        public ActionResult Create()
        {
            ViewBag.Employee_id = new SelectList(db.Employees, "Employee_id", "FirstName");
            ViewBag.Kustannuspaikka_id = new SelectList(db.Kustannuspaikat, "Kustannuspaikka_id", "Nimi");
            return View();
        }

        // POST: ManageDepartments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Department_id,Employee_id,Customer_id,Contractor_id,Kustannuspaikka_id,Name,Description")] Departments departments)
        {
            if (ModelState.IsValid)
            {
                db.Departments.Add(departments);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.Employee_id = new SelectList(db.Employees, "Employee_id", "FirstName", departments.Employee_id);
            ViewBag.Kustannuspaikka_id = new SelectList(db.Kustannuspaikat, "Kustannuspaikka_id", "Nimi", departments.Kustannuspaikka_id);
            return View(departments);
        }

        // GET: ManageDepartments/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Departments departments = await db.Departments.FindAsync(id);
            if (departments == null)
            {
                return HttpNotFound();
            }
            ViewBag.Employee_id = new SelectList(db.Employees, "Employee_id", "FirstName", departments.Employee_id);
            ViewBag.Kustannuspaikka_id = new SelectList(db.Kustannuspaikat, "Kustannuspaikka_id", "Nimi", departments.Kustannuspaikka_id);
            return View(departments);
        }

        // POST: ManageDepartments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Department_id,Employee_id,Customer_id,Contractor_id,Kustannuspaikka_id,Name,Description")] Departments departments)
        {
            if (ModelState.IsValid)
            {
                db.Entry(departments).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.Employee_id = new SelectList(db.Employees, "Employee_id", "FirstName", departments.Employee_id);
            ViewBag.Kustannuspaikka_id = new SelectList(db.Kustannuspaikat, "Kustannuspaikka_id", "Nimi", departments.Kustannuspaikka_id);
            return View(departments);
        }

        // GET: ManageDepartments/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Departments departments = await db.Departments.FindAsync(id);
            if (departments == null)
            {
                return HttpNotFound();
            }
            return View(departments);
        }

        // POST: ManageDepartments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Departments departments = await db.Departments.FindAsync(id);
            db.Departments.Remove(departments);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
