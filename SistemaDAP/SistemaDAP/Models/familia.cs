//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SistemaDAP.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class familia
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public familia()
        {
            this.grupo = new HashSet<grupo>();
        }
    
        public int id_familia { get; set; }
        public string descripcion { get; set; }
        public int id_marca { get; set; }
    
        public virtual marca marca { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<grupo> grupo { get; set; }
    }
}
