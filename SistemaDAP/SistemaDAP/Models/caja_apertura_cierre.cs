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
    
    public partial class caja_apertura_cierre
    {
        public int id_apertura_cierre { get; set; }
        public Nullable<System.DateTime> fecha_apertura { get; set; }
        public string monto_inicial { get; set; }
        public Nullable<System.DateTime> fecha_cierre { get; set; }
        public string monto_final { get; set; }
        public string estado_apertura_cierre { get; set; }
        public string id_usuario { get; set; }
        public Nullable<int> id_caja { get; set; }
    
        public virtual AspNetUsers AspNetUsers { get; set; }
        public virtual caja caja { get; set; }
    }
}
