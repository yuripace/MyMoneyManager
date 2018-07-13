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
    public class KeywordsController : Controller
    {
        private MyMoneyManagerEntities db = new MyMoneyManagerEntities();

        // GET: Keywords
        public async Task<ActionResult> Index()
        {
            var keywords = db.Keywords.Include(k => k.CategorieIphase);
            return View(await keywords.ToListAsync());
        }

        // GET: Keywords/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Keywords keywords = await db.Keywords.FindAsync(id);
            if (keywords == null)
            {
                return HttpNotFound();
            }
            return View(keywords);
        }

        // GET: Keywords/Create
        public ActionResult Create()
        {
            ViewBag.IDVoce_Code = new SelectList(db.CategorieIphase, "IDVoce", "Descrizione");
            return View();
        }

        // POST: Keywords/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Keyword,IDVoce_Code,id")] Keywords keywords)
        {
            if (ModelState.IsValid)
            {
                db.Keywords.Add(keywords);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.IDVoce_Code = new SelectList(db.CategorieIphase, "IDVoce", "Descrizione", keywords.IDVoce_Code);
            return View(keywords);
        }

        // GET: Keywords/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Keywords keywords = await db.Keywords.FindAsync(id);
            if (keywords == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDVoce_Code = new SelectList(db.CategorieIphase, "IDVoce", "Descrizione", keywords.IDVoce_Code);
            return View(keywords);
        }

        // POST: Keywords/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Keyword,IDVoce_Code,id")] Keywords keywords)
        {
            if (ModelState.IsValid)
            {
                db.Entry(keywords).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.IDVoce_Code = new SelectList(db.CategorieIphase, "IDVoce", "Descrizione", keywords.IDVoce_Code);
            return View(keywords);
        }

        // GET: Keywords/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Keywords keywords = await db.Keywords.FindAsync(id);
            if (keywords == null)
            {
                return HttpNotFound();
            }
            return View(keywords);
        }

        // POST: Keywords/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Keywords keywords = await db.Keywords.FindAsync(id);
            db.Keywords.Remove(keywords);
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
