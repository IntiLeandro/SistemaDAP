using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SistemaDAP.Models
{
    public class ClientePresupuestoCabecera : cliente
    {
        [Display(Name ="Fecha Presupuesto")]
        [DataType(DataType.Date)]
        public DateTime FechaPresupuesto { set; get; }

        [Display(Name = "Nombre/Razon Social")]
        public string NombreRazonSocial { set; get; }

    }
}