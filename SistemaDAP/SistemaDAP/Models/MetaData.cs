using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SistemaDAP.Models
{
    public class ClienteMetaData
    {
        [Display(Name = "Cliente")]
        [Required(ErrorMessage = "Es campo es requerido")]
        public string id_cliente { get; set; }

        [Display(Name = "CI / RUC")]
        [Required(ErrorMessage = "Es campo es requerido")]
        [StringLength(23, ErrorMessage = "La cantidad máxima de caracteres es de 23.")]
        public string ci_ruc { get; set; }

        [Display(Name = "Nombre / Razón Social")]
        [Required(ErrorMessage = "Es campo es requerido")]
        [StringLength(100, ErrorMessage = "La cantidad máxima de caracteres es de 100.")]
        public string nombre_razon_social { get; set; }

        [Display(Name = "Dirección")]
        [StringLength(160, ErrorMessage = "La cantidad máxima de caracteres es de 160.")]
        public string direccion { get; set; }

        [Display(Name = "Teléfono")]
        [StringLength(50, ErrorMessage = "La cantidad máxima de caracteres es de 50.")]
        public string telefono { get; set; }

        [Display(Name = "Fecha de Nacimiento")]
        [DataType(DataType.Date)]
        public DateTime fecha_nacimiento { set; get; }

        [Display(Name = "Sexo")]
        [StringLength(1, ErrorMessage = "La cantidad máxima de caracteres es de 1.")]
        public string sexo { get; set; }

        [Display(Name = "Email")]
        [StringLength(100, ErrorMessage = "La cantidad máxima de caracteres es de 100.")]
        public string email { get; set; }
    }

    public class ArticuloMetadata
    {
        [Display(Name = "Artículo")]
        public string id_articulo { get; set; }

        [Display(Name = "Descripción de Artículo")]
        public string descripcion { get; set; }

        [Display(Name = "Precio Venta")]
        //[StringLength(20, ErrorMessage = "La cantidad máxima de caracteres es de 20.")]
        public int precio_venta { get; set; }

        [Display(Name = "Cantidad")]
        public int cantidad { get; set; }

    }

    public class AspNetUsersMetadata
    {
        [Display(Name = "Usuario")]
        public string UserName { get; set; }
    }

    public class PermisoRolMetadata 
    {
        [Display(Name = "Controlador")]
        public string controlador { get; set; }

        [Display(Name = "Acción")]
        public string accion { get; set; }

        [Display(Name = "Roles")]
        public string roles { get; set; }
    }

    public class presupuesto_cabeceraMetadata
    {
        [Display(Name = "Nro. Presupuesto")]
        public string id_presupuesto_cabecera { get; set; }

        [Display(Name = "Fecha Presupuesto")]
        public DateTime fecha_presupuesto { get; set; }


        [Display(Name = "Código Cliente")]
        public string id_cliente { get; set; }
    }
    public class presupuesto_detalleMetadata
    {
        [Display(Name = "Nro. Presupuesto")]
        public string id_presupuesto_cabecera { get; set; }

        [Display(Name = "Cantidad de Artículo")]
        public int cantidad { get; set; }

        [Display(Name = "Precio Unitario")]
        public string precio_unitario { get; set; }
    }
    public class orden_trabajo_cabeceraMetadata
    {
        [Display(Name = "Nro. Orden Trabajo")]
        public string id_orden_trabajo { get; set; }

        [Display(Name = "Fecha Orden Trabajo")]
        public DateTime fecha_orden_trabajo { get; set; }

        [Display(Name = "Descripción Orden Trabajo")]
        public string descripcion_orden_trabajo { get; set; }
    }
}