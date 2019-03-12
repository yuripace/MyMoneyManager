using MyMoneyManager.Models;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MyMoneyManager.WebApi.Controllers
{
    public class MovimentisController : Controller
    {
        private MyMoneyManagerEntities db = new MyMoneyManagerEntities();

        // GET: Movimentis
        public async Task<ActionResult> Index()
        {
            var movimenti = db.Movimenti.Include(m => m.Carte).Include(m => m.CategorieUbiBanca).Include(m => m.ContiCorrente).Include(m => m.CategorieIphase).Include(m => m.TipoMovimento);
            return View(await movimenti.ToListAsync());
        }

        // GET: Movimentis
        public async Task<ActionResult> DaCompilareIndex()
        {
            var movimenti = db.Movimenti.Where(m => m.IDCategoriaIphase == -1).Include(m => m.Carte).Include(m => m.CategorieUbiBanca).Include(m => m.ContiCorrente).Include(m => m.CategorieIphase).Include(m => m.TipoMovimento);
            return View(await movimenti.ToListAsync());
        }

        // GET: Movimentis/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movimenti movimenti = await db.Movimenti.FindAsync(id);
            if (movimenti == null)
            {
                return HttpNotFound();
            }
            return View(movimenti);
        }

        // GET: Movimentis/Create
        public ActionResult Create()
        {
            ViewBag.IDCarta = new SelectList(db.Carte, "ID", "IDContoCorrente");
            ViewBag.IDCategoria = new SelectList(db.CategorieUbiBanca, "ID", "Descrizione");
            ViewBag.IDContoCorrente = new SelectList(db.ContiCorrente, "ID", "Descrizione");
            ViewBag.IDCategoriaIphase = new SelectList(db.CategorieIphase, "IDVoce", "Descrizione");
            ViewBag.IDTipoMovimento = new SelectList(db.TipoMovimento, "IDTipoMovimento", "Descrizione");
            return View();
        }

        // POST: Movimentis/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,IDContoCorrente,DataContabile,DataValuta,Importo,Divisa,Descrizione,Causale,IDCategoria,IDCategoriaIphase,IDCarta")] Movimenti movimenti)
        {
            if (ModelState.IsValid)
            {
                db.Movimenti.Add(movimenti);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.IDCarta = new SelectList(db.Carte, "ID", "IDContoCorrente", movimenti.IDCarta);
            ViewBag.IDCategoria = new SelectList(db.CategorieUbiBanca, "ID", "Descrizione", movimenti.IDCategoria);
            ViewBag.IDContoCorrente = new SelectList(db.ContiCorrente, "ID", "Descrizione", movimenti.IDContoCorrente);
            ViewBag.IDCategoriaIphase = new SelectList(db.CategorieIphase, "IDVoce", "Descrizione", movimenti.IDCategoriaIphase);
            ViewBag.IDTipoMovimento = new SelectList(db.TipoMovimento, "IDTipoMovimento", "Descrizione", movimenti.IDTipoMovimento);
            return View(movimenti);
        }

        // GET: Movimentis/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movimenti movimenti = await db.Movimenti.FindAsync(id);
            if (movimenti == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDCarta = new SelectList(db.Carte, "ID", "IDContoCorrente", movimenti.IDCarta);
            ViewBag.IDCategoria = new SelectList(db.CategorieUbiBanca, "ID", "Descrizione", movimenti.IDCategoria);
            ViewBag.IDContoCorrente = new SelectList(db.ContiCorrente, "ID", "Descrizione", movimenti.IDContoCorrente);
            ViewBag.IDCategoriaIphase = new SelectList(db.CategorieIphase, "IDVoce", "Descrizione", movimenti.IDCategoriaIphase);
            ViewBag.IDTipoMovimento = new SelectList(db.TipoMovimento, "IDTipoMovimento", "Descrizione", movimenti.IDTipoMovimento);
            ViewBag.id = new SelectList(db.CategorieIphase, "IDVoce", "Descrizione", movimenti.IDCategoriaIphase);
            return View(movimenti);
        }

        // POST: Movimentis/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,IDContoCorrente,DataContabile,DataValuta,Importo,Divisa,Descrizione,Causale,IDCategoria,IDCategoriaIphase,IDCarta,IDTipoMovimento")] Movimenti movimenti)
        {
            if (ModelState.IsValid)
            {
                db.Entry(movimenti).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.IDCarta = new SelectList(db.Carte, "ID", "IDContoCorrente", movimenti.IDCarta);
            ViewBag.IDCategoria = new SelectList(db.CategorieUbiBanca, "ID", "Descrizione", movimenti.IDCategoria);
            ViewBag.IDContoCorrente = new SelectList(db.ContiCorrente, "ID", "Descrizione", movimenti.IDContoCorrente);
            ViewBag.IDCategoriaIphase = new SelectList(db.CategorieIphase, "IDVoce", "Descrizione", movimenti.IDCategoriaIphase);
            ViewBag.IDTipoMovimento = new SelectList(db.TipoMovimento, "IDTipoMovimento", "Descrizione", movimenti.IDTipoMovimento);
            return View(movimenti);
        }

        // GET: Movimentis/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movimenti movimenti = await db.Movimenti.FindAsync(id);
            if (movimenti == null)
            {
                return HttpNotFound();
            }
            return View(movimenti);
        }

        // POST: Movimentis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Movimenti movimenti = await db.Movimenti.FindAsync(id);
            db.Movimenti.Remove(movimenti);
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
