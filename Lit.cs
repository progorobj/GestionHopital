//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré à partir d'un modèle.
//
//     Des modifications manuelles apportées à ce fichier peuvent conduire à un comportement inattendu de votre application.
//     Les modifications manuelles apportées à ce fichier sont remplacées si le code est régénéré.
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
