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
    public class ManageWorkAssignmentsController : Controller
    {
        private TimesheetEntities db = new TimesheetEntities();

        // GET: ManageWorkAssignments
        public async Task<ActionResult> Index()
        {
            var workAssignments = db.WorkAssignments.Include(w => w.Customers);
            return View(await workAssignments.ToListAsync());
        }

        // GET: ManageWorkAssignments/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorkAssignments workAssignments = await db.WorkAssignments.FindAsync(id);
            if (workAssignments == null)
            {
                return HttpNotFound();
            }
            return View(workAssignments);
        }

        // GET: ManageWorkAssignments/Create
        public ActionResult Create()
        {
            ViewBag.Customer_id = new SelectList(db.Customers, "Customer_id", "CutomerName");
            return View();
        }

        // POST: ManageWorkAssignments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "WorkAssignment_id,Customer_id,Title,Description,Deadline,InProgress,InProgressAt,Comleted,ComletedAt,CreatedAt,LastModifiedAt,DeletedAt,Active")] WorkAssignments workAssignments)
        {
            if (ModelState.IsValid)
            {
                db.WorkAssignments.Add(workAssignments);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.Customer_id = new SelectList(db.Customers, "Customer_id", "CutomerName", workAssignments.Customer_id);
            return View(workAssignments);
        }

        // GET: ManageWorkAssignments/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorkAssignments workAssignments = await db.WorkAssignments.FindAsync(id);
            if (workAssignments == null)
            {
                return HttpNotFound();
            }
            ViewBag.Customer_id = new SelectList(db.Customers, "Customer_id", "CutomerName", workAssignments.Customer_id);
            return View(workAssignments);
        }

        // POST: ManageWorkAssignments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "WorkAssignment_id,Customer_id,Title,Description,Deadline,InProgress,InProgressAt,Comleted,ComletedAt,CreatedAt,LastModifiedAt,DeletedAt,Active")] WorkAssignments workAssignments)
        {
            if (ModelState.IsValid)
            {
                db.Entry(workAssignments).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.Customer_id = new SelectList(db.Customers, "Customer_id", "CutomerName", workAssignments.Customer_id);
            return View(workAssignments);
        }

        // GET: ManageWorkAssignments/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorkAssignments workAssignments = await db.WorkAssignments.FindAsync(id);
            if (workAssignments == null)
            {
                return HttpNotFound();
            }
            return View(workAssignments);
        }

        // POST: ManageWorkAssignments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            WorkAssignments workAssignments = await db.WorkAssignments.FindAsync(id);
            db.WorkAssignments.Remove(workAssignments);
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
