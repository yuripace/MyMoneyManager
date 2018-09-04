using MyMoneyManager.Models;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MyMoneyManager.WebApi.Controllers
{
    public class tReportCategorieMeseIpasesController : Controller
    {
        private MyMoneyManagerEntities db = new MyMoneyManagerEntities();

        // GET: tReportCategorieMeseIpases
        public async Task<ActionResult> Index()
        {
            var tReportCategorieMeseIpase = db.tReportCategorieMeseIpase.Include(t => t.CategorieIphase).OrderByDescending(q => q.Anno).ThenByDescending(q => q.Mese);
            return View(await tReportCategorieMeseIpase.ToListAsync());
        }

        // GET: tReportCategorieMeseIpases/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tReportCategorieMeseIpase tReportCategorieMeseIpase = await db.tReportCategorieMeseIpase.FindAsync(id);
            if (tReportCategorieMeseIpase == null)
            {
                return HttpNotFound();
            }
            int? idcat = tReportCategorieMeseIpase.IDCategoria;
            System.DateTime stdate = System.DateTime.Parse("01/" + tReportCategorieMeseIpase.Mese.ToString() + "/" + tReportCategorieMeseIpase.Anno.ToString());

            System.DateTime enddate = System.DateTime.Parse(System.DateTime.DaysInMonth(tReportCategorieMeseIpase.Anno.Value, tReportCategorieMeseIpase.Mese.Value) + "/" + tReportCategorieMeseIpase.Mese.ToString() + "/" + tReportCategorieMeseIpase.Anno.ToString());
            var mov = db.Movimenti.Where(q => q.IDCategoriaIphase == idcat && q.DataContabile>stdate && q.DataContabile < enddate);
            return View(mov);
        }

        // GET: tReportCategorieMeseIpases/Create
        public ActionResult Create()
        {
            ViewBag.IDCategoria = new SelectList(db.CategorieIphase, "IDVoce", "Descrizione");
            return View();
        }

        // POST: tReportCategorieMeseIpases/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,IDCategoria,Anno,Mese,Importo")] tReportCategorieMeseIpase tReportCategorieMeseIpase)
        {
            if (ModelState.IsValid)
            {
                db.tReportCategorieMeseIpase.Add(tReportCategorieMeseIpase);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.IDCategoria = new SelectList(db.CategorieIphase, "IDVoce", "Descrizione", tReportCategorieMeseIpase.IDCategoria);
            return View(tReportCategorieMeseIpase);
        }

        // GET: tReportCategorieMeseIpases/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tReportCategorieMeseIpase tReportCategorieMeseIpase = await db.tReportCategorieMeseIpase.FindAsync(id);
            if (tReportCategorieMeseIpase == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDCategoria = new SelectList(db.CategorieIphase, "IDVoce", "Descrizione", tReportCategorieMeseIpase.IDCategoria);
            return View(tReportCategorieMeseIpase);
        }

        // POST: tReportCategorieMeseIpases/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,IDCategoria,Anno,Mese,Importo")] tReportCategorieMeseIpase tReportCategorieMeseIpase)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tReportCategorieMeseIpase).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.IDCategoria = new SelectList(db.CategorieIphase, "IDVoce", "Descrizione", tReportCategorieMeseIpase.IDCategoria);
            return View(tReportCategorieMeseIpase);
        }

        // GET: tReportCategorieMeseIpases/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tReportCategorieMeseIpase tReportCategorieMeseIpase = await db.tReportCategorieMeseIpase.FindAsync(id);
            if (tReportCategorieMeseIpase == null)
            {
                return HttpNotFound();
            }
            return View(tReportCategorieMeseIpase);
        }

        // POST: tReportCategorieMeseIpases/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            tReportCategorieMeseIpase tReportCategorieMeseIpase = await db.tReportCategorieMeseIpase.FindAsync(id);
            db.tReportCategorieMeseIpase.Remove(tReportCategorieMeseIpase);
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
