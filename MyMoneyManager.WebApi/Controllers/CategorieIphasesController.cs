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
    public class CategorieIphasesController : Controller
    {
        private MyMoneyManagerEntities db = new MyMoneyManagerEntities();

        // GET: CategorieIphases
        public async Task<ActionResult> Index()
        {
            return View(await db.CategorieIphase.ToListAsync());
        }

        // GET: CategorieIphases/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CategorieIphase categorieIphase = await db.CategorieIphase.FindAsync(id);
            if (categorieIphase == null)
            {
                return HttpNotFound();
            }
            return View(categorieIphase);
        }

        // GET: CategorieIphases/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CategorieIphases/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "IDVoce,Descrizione,Tipo")] CategorieIphase categorieIphase)
        {
            if (ModelState.IsValid)
            {
                db.CategorieIphase.Add(categorieIphase);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(categorieIphase);
        }

        // GET: CategorieIphases/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CategorieIphase categorieIphase = await db.CategorieIphase.FindAsync(id);
            if (categorieIphase == null)
            {
                return HttpNotFound();
            }
            return View(categorieIphase);
        }

        // POST: CategorieIphases/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "IDVoce,Descrizione,Tipo")] CategorieIphase categorieIphase)
        {
            if (ModelState.IsValid)
            {
                db.Entry(categorieIphase).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(categorieIphase);
        }

        // GET: CategorieIphases/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CategorieIphase categorieIphase = await db.CategorieIphase.FindAsync(id);
            if (categorieIphase == null)
            {
                return HttpNotFound();
            }
            return View(categorieIphase);
        }

        // POST: CategorieIphases/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            CategorieIphase categorieIphase = await db.CategorieIphase.FindAsync(id);
            db.CategorieIphase.Remove(categorieIphase);
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
