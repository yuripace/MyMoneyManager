﻿using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ExcelDataReader;
using Iphaser.Agenti.Model;
//using Iphaser.Services;

/*LEGGE IN AUTOMATICO IL FILE EXCEL E LO BUTTA NELLA TABELLA MOVIMENTI*/
namespace Iphaser.Importer
{
    public class Import
    {

        //public static void Main(string[] args)
        //{
        //    DataSet ds;
        //    ds = LeggiFile();
        //    bool start = false;
        //    IphaserEntities context = new IphaserEntities();
        //    foreach (DataRowView item in ds.Tables[0].DefaultView)
        //    {
        //        int i = 0;
        //        foreach (DataRow row in item.DataView.Table.Rows)
        //        {
        //            i++;
        //            if (i<=23)
        //            {
        //                ;
        //            }
        //            else if (start)
        //            {
        //                Movimenti mov = new Movimenti();
        //                //OPERAZ.PAGOBANCOMAT DEL 25 / 04 / 2018 ALLE 18.02 TES.N.04823278 PRESSO 5172973 / 00001(ABI 03069) 
        //                //FASHION & HOME LECCO CORSO CARLO ALBERTO -CAT.MERC. (5651) N.CRO / RRN 811551753363 - ID CARTA 04823278
        //                //Tessera: Carta di credito padre (la mia). IDCarta chi ha effettivamente usato.
        //                if (!row.IsNull(1))
        //                {
        //                    mov.DataContabile = DateTime.Parse(row[1].ToString()).Date;
        //                    mov.DataValuta = DateTime.Parse(row[2].ToString()).Date;
        //                    mov.Importo = Decimal.Parse(row[3].ToString().Replace(".",","));
        //                    mov.Divisa = row[4].ToString();
        //                    mov.Descrizione = row[5].ToString();
        //                    mov.Causale = row[6].ToString();
        //                    context.Movimenti.Add(mov);

        //                }
        //            }
        //            else if (row[1].ToString().Trim().ToLower().Equals("data contabile"))
        //            {
        //                start = true;
        //            }
        //        }
        //        break;
        //    }

        //    context.SaveChanges();
        //}



        static class Program
        {
            static void Main()
            {
                // The input string again.
                string input = "/OPERAZ.PAGOBANCOMAT DEL 25 / 04 / 2018 ALLE 18.02 TES.N.04823278 PRESSO 5172973 / 00001(ABI 03069) FASHION & HOME LECCO CORSO CARLO ALBERTO -CAT.MERC. (5651) N.CRO / RRN 811551753363 - ID CARTA 04823278";
                Regex _regex = new Regex(@"CAT.MERC.\s*(\d$");
                // This calls the static method specified.
                Match match = _regex.Match(input);
                if (match.Success)
                {
                    Console.WriteLine(match.Groups[1].Value);
                }
            }
        }

        public static DataSet LeggiFile()
        {
            DataSet ds;
            var extension = Path.GetExtension("C:\\Saldo_e_Movimenti.xls").ToLower();
            using (var stream = new FileStream("C:\\Saldo_e_Movimenti.xls", FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
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

                // reader.IsFirstRowAsColumnNames = firstRowNamesCheckBox.Checked;
                //var sw = new Stopwatch();
                //sw.Start();
                using (reader)
                {
                    ds = reader.AsDataSet(new ExcelDataSetConfiguration()
                    {
                        ConfigureDataTable = (tableReader) => new ExcelDataTableConfiguration()
                        {
                            /*UseHeaderRow = firstRowNamesCheckBox.Checked*/
                        }
                    });
                }
                return ds;
                //toolStripStatusLabel1.Text = "Elapsed: " + sw.ElapsedMilliseconds.ToString() + " ms";

                // var tablenames = GetTablenames(ds.Tables);
                /* sheetCombo.DataSource = tablenames;

                 if (tablenames.Count > 0)
                     sheetCombo.SelectedIndex = 0;

                 // dataGridView1.DataSource = ds;
                 // dataGridView1.DataMember*/

            }
        }

        /*
        private static IList<string> GetTablenames(DataTableCollection tables)
        {
            var tableList = new List<string>();
            foreach (var table in tables)
            {
                tableList.Add(table.ToString());
            }

            return tableList;
        }*/

    }
}
