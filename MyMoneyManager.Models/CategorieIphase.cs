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
    
    public partial class CategorieIphase
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CategorieIphase()
        {
            this.tReportCategorieMeseIpase = new HashSet<tReportCategorieMeseIpase>();
            this.Movimenti = new HashSet<Movimenti>();
            this.Keywords = new HashSet<Keywords>();
        }
    
        public int IDVoce { get; set; }
        public string Descrizione { get; set; }
        public Nullable<int> Tipo { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tReportCategorieMeseIpase> tReportCategorieMeseIpase { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Movimenti> Movimenti { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Keywords> Keywords { get; set; }
    }
}
