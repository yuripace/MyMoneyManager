using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MyMoneyManager.Models;

namespace MyMoneyManager.WebApi.Controllers
{
    public class tReportCategorieMeseUbisController : Controller
    {
        private MyMoneyManagerEntities db = new MyMoneyManagerEntities();

        // GET: tReportCategorieMeseUbis
        public async Task<ActionResult> Index()
        {
            var tReportCategorieMeseUbi = db.tReportCategorieMeseUbi.Include(t => t.CategorieUbiBanca);
            return View(await tReportCategorieMeseUbi.ToListAsync());
        }

        // GET: tReportCategorieMeseUbis/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tReportCategorieMeseUbi tReportCategorieMeseUbi = await db.tReportCategorieMeseUbi.FindAsync(id);
            if (tReportCategorieMeseUbi == null)
            {
                return HttpNotFound();
            }
            return View(tReportCategorieMeseUbi);
        }

        // GET: tReportCategorieMeseUbis/Create
        public ActionResult Create()
        {
            ViewBag.IDCategoria = new SelectList(db.CategorieUbiBanca, "ID", "Descrizione");
            return View();
        }

        // POST: tReportCategorieMeseUbis/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,IDCategoria,Anno,Mese,Importo")] tReportCategorieMeseUbi tReportCategorieMeseUbi)
        {
            if (ModelState.IsValid)
            {
                db.tReportCategorieMeseUbi.Add(tReportCategorieMeseUbi);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.IDCategoria = new SelectList(db.CategorieUbiBanca, "ID", "Descrizione", tReportCategorieMeseUbi.IDCategoria);
            return View(tReportCategorieMeseUbi);
        }

        // GET: tReportCategorieMeseUbis/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tReportCategorieMeseUbi tReportCategorieMeseUbi = await db.tReportCategorieMeseUbi.FindAsync(id);
            if (tReportCategorieMeseUbi == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDCategoria = new SelectList(db.CategorieUbiBanca, "ID", "Descrizione", tReportCategorieMeseUbi.IDCategoria);
            return View(tReportCategorieMeseUbi);
        }

        // POST: tReportCategorieMeseUbis/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,IDCategoria,Anno,Mese,Importo")] tReportCategorieMeseUbi tReportCategorieMeseUbi)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tReportCategorieMeseUbi).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.IDCategoria = new SelectList(db.CategorieUbiBanca, "ID", "Descrizione", tReportCategorieMeseUbi.IDCategoria);
            return View(tReportCategorieMeseUbi);
        }

        // GET: tReportCategorieMeseUbis/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tReportCategorieMeseUbi tReportCategorieMeseUbi = await db.tReportCategorieMeseUbi.FindAsync(id);
            if (tReportCategorieMeseUbi == null)
            {
                return HttpNotFound();
            }
            return View(tReportCategorieMeseUbi);
        }

        // POST: tReportCategorieMeseUbis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            tReportCategorieMeseUbi tReportCategorieMeseUbi = await db.tReportCategorieMeseUbi.FindAsync(id);
            db.tReportCategorieMeseUbi.Remove(tReportCategorieMeseUbi);
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
