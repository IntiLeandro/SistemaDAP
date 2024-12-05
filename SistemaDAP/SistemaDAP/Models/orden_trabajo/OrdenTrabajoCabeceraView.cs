using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SistemaDAP.Models;

namespace SistemaDAP.Models
{
    public class OrdenTrabajoCabeceraView
    {
        public PresupuestoOrdenTrabajoCabecera Presupuesto { set; get; }
        public ArticuloOrdenTrabajoCabecera Titulo { set; get; }
        public List<ArticuloOrdenTrabajoCabecera> Articulo { set; get; }
        public orden_trabajo_cabecera Orden_Trabajo { set; get; }
    }
}