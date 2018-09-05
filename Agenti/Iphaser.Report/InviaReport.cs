using CrystalDecisions.CrystalReports.Engine;
//using Iphaser.Services;

/*LEGGE IN AUTOMATICO IL FILE EXCEL E LO BUTTA NELLA TABELLA MOVIMENTI*/
namespace Iphaser.InviaReport
{
    public class InviaReport
    {

        public static void Main(string[] args)
        {
            var strServer = "SRV-DEVELOPMENT\\SQLEXPRESS";
            var strDatabase = "MyMoneyManager";
            var strUserID = "sa";
            var strPwd = "piracy82";

          

            rptMovimentiNGiorni rpt = new rptMovimentiNGiorni();
            rpt.SetParameterValue(0, 30); //settimanale
            
            rpt.DataSourceConnections[0].SetConnection(strServer, strDatabase, strUserID, strPwd);
            rpt.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, "reportSettimanale.pdf");

        }

    }
}
