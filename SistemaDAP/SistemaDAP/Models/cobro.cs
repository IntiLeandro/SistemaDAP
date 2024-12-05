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
    
    public partial class cobro
    {
        public int id_cobro { get; set; }
        public Nullable<decimal> monto_cobro { get; set; }
        public Nullable<System.DateTime> fecha_cobro { get; set; }
        public string login_grabacion { get; set; }
        public Nullable<System.DateTime> fecha_grabacion { get; set; }
        public Nullable<int> id_cliente { get; set; }
        public Nullable<int> id_factura { get; set; }
        public Nullable<int> id_banco { get; set; }
        public Nullable<int> id_tipo_cobro { get; set; }
    
        public virtual banco banco { get; set; }
        public virtual cliente cliente { get; set; }
        public virtual factura factura { get; set; }
        public virtual tipo_cobro tipo_cobro { get; set; }
    }
}