using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaDAP.Models
{
    public class PresupuestoCabeceraView
    {
        public ClientePresupuestoCabecera Cliente { set; get; }
        public ArticuloPresupuestoCabecera Titulo { set; get; }
        public List<ArticuloPresupuestoCabecera> Articulo { set; get; }
        public List<presupuesto_cabecera> Presupuesto { set; get; }
    }
}