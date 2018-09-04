using PdfSharp.Drawing;
using PdfSharp.Pdf;
using System.Diagnostics;
//using Iphaser.Services;

/*LEGGE IN AUTOMATICO IL FILE EXCEL E LO BUTTA NELLA TABELLA MOVIMENTI*/
namespace Iphaser.CalcolaCategorie
{
    public class CalcolaCategorie
    {

        public static void Main(string[] args)
        {
            PdfDocument document = new PdfDocument();
            document.Info.Title = "Report Settimanale";
            PdfPage page = document.AddPage();
            XGraphics gfx = XGraphics.FromPdfPage(page);
            XFont font = new XFont("Verdana", 20, XFontStyle.BoldItalic);
            gfx.DrawString("Hello, World!", font, XBrushes.Black,new XRect(0, 0, page.Width, page.Height),XStringFormats.Center);
            const string filename = "HelloWorld.pdf";
            document.Save(filename);
            Process.Start(filename);
            //MyMoneyManagerEntities context = new MyMoneyManagerEntities();

            //crea un report giornaliero inviato tramite mail

            //#region Calcolo report IPASE
            ////var calcolo = context.Movimenti.Where(q => q.IDCategoriaIphase != null).GroupBy(q => q.IDCategoriaIphase).Sum(q => new { q. }q.Sum(e => e.Importo)).;
            ////var calcoloipase = context.Movimenti.Where(q => q.IDCategoriaIphase != null).GroupBy(x => new { x.IDCategoriaIphase, DbFunctions.CreateDateTime(x.DataValuta.Value.Year, x.DataValuta.Value.Month, x.DataValuta.Value.Day, 0, 0, 0).Value.Year, DbFunctions.CreateDateTime(x.DataValuta.Value.Year, x.DataValuta.Value.Month, x.DataValuta.Value.Day, 0, 0, 0).Value.Month }).SelectMany(rr => rr.Select(r => new { IDCategoria = rr.Key.IDCategoriaIphase, Anno = rr.Key.Year, Mese = rr.Key.Month, Totale = rr.Sum(t => t.Importo) }));
            ////foreach (var item in calcoloipase)
            ////{
            ////    tReportCategorieMeseIpase rep = new tReportCategorieMeseIpase();
            ////    rep.IDCategoria = item.IDCategoria;
            ////    rep.Anno = item.Anno;
            ////    rep.Mese = item.Mese;
            ////    rep.Importo = item.Totale;
            ////    context.tReportCategorieMeseIpase.Add(rep);
            ////}
            //var collection = context.vReportCategorieUbi;
            //foreach (var item in collection)
            //{
            //    tReportCategorieMeseUbi rep = new tReportCategorieMeseUbi();
            //    rep.IDCategoria = item.IDCategoria;
            //    rep.Anno = item.Anno;
            //    rep.Mese = item.Mese;
            //    rep.Importo = item.Totale;
            //    context.tReportCategorieMeseUbi.Add(rep);
            //}
            //context.SaveChanges();

            //var collection2 = context.v_ReportCategorieIpase;
            //foreach (var item in collection2)
            //{
            //    tReportCategorieMeseIpase rep1 = new tReportCategorieMeseIpase();
            //    rep1.IDCategoria = item.IDCategoriaIphase;
            //    rep1.Anno = item.Anno;
            //    rep1.Mese = item.Mese;
            //    rep1.Importo = item.Totale;
            //    context.tReportCategorieMeseIpase.Add(rep1);
            //}
            //context.SaveChanges();
            //#endregion

        }

    }
}
