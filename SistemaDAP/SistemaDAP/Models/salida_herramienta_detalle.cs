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
    
    public partial class salida_herramienta_detalle
    {
        public int id_salida_herramienta_det { get; set; }
        public int id_salida_herramienta_cab { get; set; }
        public int id_herramienta { get; set; }
        public Nullable<int> cantidad { get; set; }
        public string observacion { get; set; }
    
        public virtual herramienta herramienta { get; set; }
        public virtual salida_herramienta_cabecera salida_herramienta_cabecera { get; set; }
    }
}