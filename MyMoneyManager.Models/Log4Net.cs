//------------------------------------------------------------------------------
// <auto-generated>
//     Codice generato da un modello.
//
//     Le modifiche manuali a questo file potrebbero causare un comportamento imprevisto dell'applicazione.
//     Se il codice viene rigenerato, le modifiche manuali al file verranno sovrascritte.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MyMoneyManager.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Log4Net
    {
        public int ID { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public string Level { get; set; }
        public string User { get; set; }
        public string Logger { get; set; }
        public string Message { get; set; }
        public string Params { get; set; }
        public string Exception { get; set; }
    }
}