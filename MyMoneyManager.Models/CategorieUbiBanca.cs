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
    
    public partial class CategorieUbiBanca
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CategorieUbiBanca()
        {
            this.Movimenti = new HashSet<Movimenti>();
            this.tReportCategorieMeseUbi = new HashSet<tReportCategorieMeseUbi>();
        }
    
        public int ID { get; set; }
        public string Descrizione { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Movimenti> Movimenti { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tReportCategorieMeseUbi> tReportCategorieMeseUbi { get; set; }
    }
}
