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
    
    public partial class presupuesto_cabecera
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public presupuesto_cabecera()
        {
            this.presupuesto_detalle = new HashSet<presupuesto_detalle>();
            this.orden_trabajo_cabecera = new HashSet<orden_trabajo_cabecera>();
        }
    
        public int id_presupuesto_cabecera { get; set; }
        public System.DateTime fecha_presupuesto { get; set; }
        public int id_cliente { get; set; }
        public Nullable<int> id_presupuesto_estado { get; set; }
        public Nullable<System.DateTime> fecha_vencimiento { get; set; }
        public Nullable<System.DateTime> fecha_aprobacion { get; set; }
        public string login_grabacion { get; set; }
        public Nullable<System.DateTime> fecha_grabacion { get; set; }
        public Nullable<decimal> monto_total { get; set; }
        public Nullable<decimal> monto_sub_total { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<presupuesto_detalle> presupuesto_detalle { get; set; }
        public virtual cliente cliente { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<orden_trabajo_cabecera> orden_trabajo_cabecera { get; set; }
        public virtual presupuesto_estado presupuesto_estado { get; set; }
    }
}
