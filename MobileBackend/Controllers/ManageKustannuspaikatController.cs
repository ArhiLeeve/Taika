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
    public class ManageKustannuspaikatController : Controller
    {
        private TimesheetEntities db = new TimesheetEntities();

        // GET: ManageKustannuspaikat
        public async Task<ActionResult> Index()
        {
            return View(await db.Kustannuspaikat.ToListAsync());
        }

        // GET: ManageKustannuspaikat/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kustannuspaikat kustannuspaikat = await db.Kustannuspaikat.FindAsync(id);
            if (kustannuspaikat == null)
            {
                return HttpNotFound();
            }
            return View(kustannuspaikat);
        }

        // GET: ManageKustannuspaikat/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ManageKustannuspaikat/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Kustannuspaikka_id,Nimi,Contractor_id,Department_id,Comments")] Kustannuspaikat kustannuspaikat)
        {
            if (ModelState.IsValid)
            {
                db.Kustannuspaikat.Add(kustannuspaikat);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(kustannuspaikat);
        }

        // GET: ManageKustannuspaikat/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kustannuspaikat kustannuspaikat = await db.Kustannuspaikat.FindAsync(id);
            if (kustannuspaikat == null)
            {
                return HttpNotFound();
            }
            return View(kustannuspaikat);
        }

        // POST: ManageKustannuspaikat/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Kustannuspaikka_id,Nimi,Contractor_id,Department_id,Comments")] Kustannuspaikat kustannuspaikat)
        {
            if (ModelState.IsValid)
            {
                db.Entry(kustannuspaikat).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(kustannuspaikat);
        }

        // GET: ManageKustannuspaikat/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kustannuspaikat kustannuspaikat = await db.Kustannuspaikat.FindAsync(id);
            if (kustannuspaikat == null)
            {
                return HttpNotFound();
            }
            return View(kustannuspaikat);
        }

        // POST: ManageKustannuspaikat/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Kustannuspaikat kustannuspaikat = await db.Kustannuspaikat.FindAsync(id);
            db.Kustannuspaikat.Remove(kustannuspaikat);
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
