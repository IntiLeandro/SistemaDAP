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
    
    public partial class articulo
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public articulo()
        {
            this.presupuesto_detalle = new HashSet<presupuesto_detalle>();
            this.orden_trabajo_detalle = new HashSet<orden_trabajo_detalle>();
            this.compra_detalle = new HashSet<compra_detalle>();
            this.factura_detalle = new HashSet<factura_detalle>();
            this.ajuste = new HashSet<ajuste>();
        }
    
        public int id_articulo { get; set; }
        public string descripcion { get; set; }
        public decimal precio_venta { get; set; }
        public int cantidad { get; set; }
        public Nullable<int> id_tipo_articulo { get; set; }
        public Nullable<int> id_iva { get; set; }
        public Nullable<decimal> precio_costo { get; set; }
        public Nullable<int> id_grupo { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<presupuesto_detalle> presupuesto_detalle { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<orden_trabajo_detalle> orden_trabajo_detalle { get; set; }
        public virtual tipo_articulo tipo_articulo { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<compra_detalle> compra_detalle { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<factura_detalle> factura_detalle { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ajuste> ajuste { get; set; }
        public virtual grupo grupo { get; set; }
        public virtual impuesto impuesto { get; set; }
    }
}