using ExcelDataReader;
using Iphaser.Agenti.Model;
using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
//using Iphaser.Services;

/*LEGGE IN AUTOMATICO IL FILE EXCEL E LO BUTTA NELLA TABELLA MOVIMENTI*/
namespace Iphaser.Importer
{
    public class Import
    {

        public static void Main(string[] args)
        {

            ElaboraMovimenti(4923);
            ElaboraMovimenti(1047);

            ElaboraCartaCredito("8470");
            ElaboraCartaCredito("0517");

        }

        public static void ElaboraCartaCredito(string IDConto)
        {
            MyMoneyManagerEntities context = new MyMoneyManagerEntities();
            DataSet ds;
            ds = LeggiCartaCredito(IDConto);
            bool start = false;


            #region Import Movimenti
            foreach (DataRowView item in ds.Tables[0].DefaultView)
            {
                int i = 0;
                foreach (DataRow row in item.DataView.Table.Rows)
                {
                    i++;
                    if (i <= 20)
                    {
                        ;
                    }
                    else if (start)
                    {
                        Movimenti mov = new Movimenti();

                        if (!row.IsNull(1))
                        {
                            mov.Descrizione = row[3].ToString();
                            if (mov.Descrizione.ToLower().Trim().Equals("PAGAMENTO CON ADDEBITO") == false)
                            {
                                mov.DataContabile = DateTime.Parse(row[1].ToString()).Date;
                                mov.DataValuta = DateTime.Parse(row[1].ToString()).Date;
                                mov.IDContoCorrente = 1047;
                                mov.Importo = -(Decimal.Parse(row[2].ToString().Replace(".", ",")));
                                mov.Divisa = "EUR";

                                mov.Causale = "";

                                if (context.Keywords.Where(q => mov.Descrizione.ToLower().Trim().Contains(q.Keyword.Trim().ToLower())).Count() == 1)
                                {
                                    mov.IDCategoriaIphase = context.Keywords.Where(q => mov.Descrizione.Trim().ToLower().Contains(q.Keyword.Trim().ToLower())).First().IDVoce_Code;
                                }
                                else
                                    mov.IDCategoriaIphase = -1;

                                context.Movimenti.Add(mov);
                            }
                        }
                    }
                    else if (row[1].ToString().Trim().ToLower().Equals("data contabile"))
                    {
                        start = true;
                    }
                }
                break;
            }
            #endregion
            context.SaveChanges();
        }

        public static void ElaboraMovimenti(int IDConto)
        {
            MyMoneyManagerEntities context = new MyMoneyManagerEntities();
            DataSet ds;
            ds = LeggiFile(IDConto);
            bool start = false;


            #region Import Movimenti
            foreach (DataRowView item in ds.Tables[0].DefaultView)
            {
                int i = 0;
                foreach (DataRow row in item.DataView.Table.Rows)
                {
                    i++;
                    if (i <= 23)
                    {
                        ;
                    }
                    else if (start)
                    {
                        Movimenti mov = new Movimenti();
                        //OPERAZ.PAGOBANCOMAT DEL 25 / 04 / 2018 ALLE 18.02 TES.N.04823278 PRESSO 5172973 / 00001(ABI 03069) 
                        //FASHION & HOME LECCO CORSO CARLO ALBERTO -CAT.MERC. (5651) N.CRO / RRN 811551753363 - ID CARTA 04823278
                        //Tessera: Carta di credito padre (la mia). IDCarta chi ha effettivamente usato.
                        if (!row.IsNull(1))
                        {

                            mov.DataContabile = DateTime.Parse(row[1].ToString()).Date;
                            mov.DataValuta = DateTime.Parse(row[2].ToString()).Date;
                            mov.IDContoCorrente = IDConto;
                            mov.Importo = Decimal.Parse(row[3].ToString().Replace(".", ","));
                            mov.Divisa = row[4].ToString();
                            mov.Descrizione = row[5].ToString();
                            mov.Causale = row[6].ToString();

                            #region Categorie UBI
                            Regex _regex = new Regex(@"CAT.\s*MERC.\s*\(\d+");
                            // This calls the static method specified.
                            Match match = _regex.Match(mov.Descrizione);
                            if (match.Success)
                            {
                                //Console.WriteLine("Value:" + match.Groups[0].Value.ToString().Replace("CAT.MERC. (", ""));
                                string id = Regex.Replace(match.Groups[0].Value.ToString(), "CAT.\\s*MERC.\\s*\\(", "");
                                int iid = int.Parse(id);
                                if (iid > 0)
                                {
                                    mov.IDCategoria = iid;
                                    if (context.CategorieUbiBanca.Where(q => q.ID == mov.IDCategoria).Count() <= 0 && context.CategorieUbiBanca.Local.Where(q => q.ID == mov.IDCategoria).Count() <= 0)
                                    {
                                        CategorieUbiBanca ubicat = new CategorieUbiBanca();
                                        ubicat.ID = mov.IDCategoria.Value;
                                        context.CategorieUbiBanca.Add(ubicat);
                                    }
                                }
                            }
                            #endregion

                            #region Tes. N.
                            Regex _regex2 = new Regex(@"TES.\s*N.\s*\s*([^\s]+)");
                            // This calls the static method specified.
                            Match match2 = _regex2.Match(mov.Descrizione);
                            if (match2.Success)
                            {
                                //Console.WriteLine("Value:" + match.Groups[0].Value.ToString().Replace("CAT.MERC. (", ""));
                                string id = Regex.Replace(match2.Groups[0].Value.ToString(), "TES.\\s*N.\\s*", "");

                                if (!String.IsNullOrEmpty(id))
                                {
                                    mov.IDCarta = id;
                                }
                            }
                            #endregion
                            if (mov.Causale.Contains("Prelievi"))
                            {
                                mov.IDCategoriaIphase = 90;
                            }
                            else if (context.Keywords.Where(q => mov.Descrizione.ToLower().Trim().Contains(q.Keyword.Trim().ToLower())).Count() == 1)
                            {
                                mov.IDCategoriaIphase = context.Keywords.Where(q => mov.Descrizione.Trim().ToLower().Contains(q.Keyword.Trim().ToLower())).First().IDVoce_Code;
                            }
                            else if (mov.Causale.Contains("Pagamenti POS"))
                            {
                                mov.IDCategoriaIphase = 92;
                            }
                            else
                                mov.IDCategoriaIphase = -1;

                            context.Movimenti.Add(mov);

                        }
                    }
                    else if (row[1].ToString().Trim().ToLower().Equals("data contabile"))
                    {
                        start = true;
                    }
                }
                break;
            }
            #endregion
            context.SaveChanges();
        }

        public static DataSet LeggiCartaCredito(string IDConto)
        {
            DataSet ds;
            var extension = Path.GetExtension("C:\\Movimenti_Hybrid_" + IDConto.ToString() + ".xls").ToLower();
            using (var stream = new FileStream("C:\\Movimenti_Hybrid_" + IDConto.ToString() + ".xls", FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                IExcelDataReader reader = null;
                if (extension == ".xls")
                {
                    reader = ExcelReaderFactory.CreateBinaryReader(stream);
                }
                else if (extension == ".xlsx")
                {
                    reader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                }
                else if (extension == ".csv")
                {
                    reader = ExcelReaderFactory.CreateCsvReader(stream);
                }

                if (reader == null)
                    return null;

                using (reader)
                {
                    ds = reader.AsDataSet(new ExcelDataSetConfiguration()
                    {
                        ConfigureDataTable = (tableReader) => new ExcelDataTableConfiguration()
                        {
                        }
                    });
                }
                return ds;
            }
        }

        public static DataSet LeggiFile(int IDConto)
        {
            DataSet ds;
            var extension = Path.GetExtension("C:\\Saldo_e_Movimenti_" + IDConto.ToString() + ".xls").ToLower();
            using (var stream = new FileStream("C:\\Saldo_e_Movimenti_" + IDConto.ToString() + ".xls", FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                IExcelDataReader reader = null;
                if (extension == ".xls")
                {
                    reader = ExcelReaderFactory.CreateBinaryReader(stream);
                }
                else if (extension == ".xlsx")
                {
                    reader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                }
                else if (extension == ".csv")
                {
                    reader = ExcelReaderFactory.CreateCsvReader(stream);
                }

                if (reader == null)
                    return null;
                using (reader)
                {
                    ds = reader.AsDataSet(new ExcelDataSetConfiguration()
                    {
                        ConfigureDataTable = (tableReader) => new ExcelDataTableConfiguration()
                        {
                        }
                    });
                }
                return ds;
            }
        }

        public static 
    }
}
