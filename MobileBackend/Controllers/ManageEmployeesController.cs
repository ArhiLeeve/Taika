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
    public class ManageEmployeesController : Controller
    {
        private TimesheetEntities db = new TimesheetEntities();

        // GET: ManageEmployees
        public async Task<ActionResult> Index()
        {
            var employees = db.Employees.Include(e => e.Contractors);
            return View(await employees.ToListAsync());
        }

        // GET: ManageEmployees/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employees employees = await db.Employees.FindAsync(id);
            if (employees == null)
            {
                return HttpNotFound();
            }
            return View(employees);
        }

        // GET: ManageEmployees/Create
        public ActionResult Create()
        {
            ViewBag.Contractor_id = new SelectList(db.Contractors, "Contractor_id", "CompanyName");
            return View();
        }

        // POST: ManageEmployees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Employee_id,Contractor_id,Department_id,FirstName,LastName,PhoneNumber,EmailAddress,EmployeeReferences,CreatedAt,LastModifiedAt,DeletedAt,Active,EmployeePicture,PinCode")] Employees employees)
        {
            if (ModelState.IsValid)
            {
                db.Employees.Add(employees);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.Contractor_id = new SelectList(db.Contractors, "Contractor_id", "CompanyName", employees.Contractor_id);
            return View(employees);
        }

        // GET: ManageEmployees/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employees employees = await db.Employees.FindAsync(id);
            if (employees == null)
            {
                return HttpNotFound();
            }
            ViewBag.Contractor_id = new SelectList(db.Contractors, "Contractor_id", "CompanyName", employees.Contractor_id);
            return View(employees);
        }

        // POST: ManageEmployees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Employee_id,Contractor_id,Department_id,FirstName,LastName,PhoneNumber,EmailAddress,EmployeeReferences,CreatedAt,LastModifiedAt,DeletedAt,Active,EmployeePicture,PinCode")] Employees employees)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employees).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.Contractor_id = new SelectList(db.Contractors, "Contractor_id", "CompanyName", employees.Contractor_id);
            return View(employees);
        }

        // GET: ManageEmployees/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employees employees = await db.Employees.FindAsync(id);
            if (employees == null)
            {
                return HttpNotFound();
            }
            return View(employees);
        }

        // POST: ManageEmployees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Employees employees = await db.Employees.FindAsync(id);
            db.Employees.Remove(employees);
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
