using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SistemaDAP.Models
{
    public class ArticuloOrdenTrabajoCabecera : articulo
    {
        [Display(Name = "Cantidad")]
        public int cantidadArticulo { set; get; }

        [Display(Name = "Total")]
        public decimal partial { get { return precio_venta * cantidad; } }
    }
}