//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GestionHopital
{
    using System;
    using System.Collections.Generic;
    
    public partial class Lit
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Lit()
        {
            this.Admissions = new HashSet<Admission>();
        }
    
        public int numeroLit { get; set; }
        public int occupe { get; set; }
        public int idType { get; set; }
        public int idDEpartement { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Admission> Admissions { get; set; }
        public virtual Departement Departement { get; set; }
        public virtual TypeLit TypeLit { get; set; }
    }
}
