//------------------------------------------------------------------------------
// <auto-generated>
//     Codice generato da un modello.
//
//     Le modifiche manuali a questo file potrebbero causare un comportamento imprevisto dell'applicazione.
//     Se il codice viene rigenerato, le modifiche manuali al file verranno sovrascritte.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Iphaser.Agenti.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class Movimenti
    {
        public int ID { get; set; }
        public Nullable<int> IDContoCorrente { get; set; }
        public Nullable<System.DateTime> DataContabile { get; set; }
        public Nullable<System.DateTime> DataValuta { get; set; }
        public Nullable<decimal> Importo { get; set; }
        public string Divisa { get; set; }
        public string Descrizione { get; set; }
        public string Causale { get; set; }
        public Nullable<int> IDCategoria { get; set; }
        public Nullable<int> IDCategoriaIphase { get; set; }
        public string IDCarta { get; set; }
        public Nullable<int> IDMovimentoRipetitivo { get; set; }
    
        public virtual Carte Carte { get; set; }
        public virtual CategorieUbiBanca CategorieUbiBanca { get; set; }
        public virtual ContiCorrente ContiCorrente { get; set; }
        public virtual CategorieIphase CategorieIphase { get; set; }
        public virtual MovimentiRipetitivi MovimentiRipetitivi { get; set; }
    }
}
