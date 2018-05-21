using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ExcelDataReader;
using Iphaser.Agenti.Model;
//using Iphaser.Services;

/*LEGGE IN AUTOMATICO IL FILE EXCEL E LO BUTTA NELLA TABELLA MOVIMENTI*/
namespace Iphaser.CalcolaCategorie
{
    public class CalcolaCategorie
    {

        public static void Main(string[] args)
        {
            MyMoneyManagerEntities context = new MyMoneyManagerEntities();

            #region Calcolo report IPASE
            //var calcolo = context.Movimenti.Where(q => q.IDCategoriaIphase != null).GroupBy(q => q.IDCategoriaIphase).Sum(q => new { q. }q.Sum(e => e.Importo)).;
            var calcoloipase = context.Movimenti.Where(q => q.IDCategoriaIphase != null).GroupBy(x => new { x.IDCategoriaIphase, DbFunctions.CreateDateTime(x.DataValuta.Value.Year, x.DataValuta.Value.Month, x.DataValuta.Value.Day, 0, 0, 0).Value.Year, DbFunctions.CreateDateTime(x.DataValuta.Value.Year, x.DataValuta.Value.Month, x.DataValuta.Value.Day, 0, 0, 0).Value.Month }).SelectMany(rr => rr.Select(r => new { IDCategoria = rr.Key.IDCategoriaIphase, Anno = rr.Key.Year, Mese = rr.Key.Month, Totale = rr.Sum(t => t.Importo) }));
            foreach (var item in calcoloipase)
            {
                tReportCategorieMeseIpase rep = new tReportCategorieMeseIpase();
                rep.IDCategoria = item.IDCategoria;
                rep.Anno = item.Anno;
                rep.Mese = item.Mese;
                rep.Importo = item.Totale;
                context.tReportCategorieMeseIpase.Add(rep);
            }
            context.SaveChanges();
            #endregion


            #region Calcolo report UBI
            //var calcolo = context.Movimenti.Where(q => q.IDCategoriaIphase != null).GroupBy(q => q.IDCategoriaIphase).Sum(q => new { q. }q.Sum(e => e.Importo)).;
            var calcoloubi = context.Movimenti.Where(q => q.IDCategoria != null).GroupBy(x => new { x.IDCategoria, DbFunctions.CreateDateTime(x.DataValuta.Value.Year, x.DataValuta.Value.Month, x.DataValuta.Value.Day, 0, 0, 0).Value.Year, DbFunctions.CreateDateTime(x.DataValuta.Value.Year, x.DataValuta.Value.Month, x.DataValuta.Value.Day, 0, 0, 0).Value.Month }).SelectMany(rr => rr.Select(r => new { IDCategoria = rr.Key.IDCategoria, Anno = rr.Key.Year, Mese = rr.Key.Month, Totale = rr.Sum(t => t.Importo) }));
            foreach (var item in calcoloubi)
            {
                tReportCategorieMeseUbi rep = new tReportCategorieMeseUbi();
                rep.IDCategoria = item.IDCategoria;
                rep.Anno = item.Anno;
                rep.Mese = item.Mese;
                rep.Importo = item.Totale;
                context.tReportCategorieMeseUbi.Add(rep);
            }
            context.SaveChanges();
            #endregion

        }

    }
}
