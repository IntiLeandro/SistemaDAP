using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SistemaDAP.Models
{
    public class PresupuestoOrdenTrabajoCabecera : presupuesto_cabecera
    {
        [Display(Name = "Fecha Orden Trabajo")]
        [DataType(DataType.Date)]
        public DateTime FechaOrdenTrabajo { set; get; }

    }
}