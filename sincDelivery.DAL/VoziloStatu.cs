//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace sincDelivery.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class VoziloStatu
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public VoziloStatu()
        {
            this.Voziloes = new HashSet<Vozilo>();
        }
    
        public int ID { get; set; }
        public string Naziv { get; set; }
        public Nullable<bool> Aktivan { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Vozilo> Voziloes { get; set; }
    }
}
