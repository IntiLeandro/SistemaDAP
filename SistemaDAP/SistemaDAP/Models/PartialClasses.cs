using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SistemaDAP.Models
{
    [MetadataType(typeof(ClienteMetaData))]
    public partial class cliente
    { }

    [MetadataType(typeof(ArticuloMetadata))]
    public partial class articulo
    { }

    [MetadataType(typeof(AspNetUsersMetadata))]
    public partial class AspNetUsers
    { }

    [MetadataType(typeof(PermisoRolMetadata))]
    public partial class permiso_rol
    { }

    [MetadataType(typeof(presupuesto_cabeceraMetadata))]
    public partial class presupuesto_cabecera
    { }
    [MetadataType(typeof(presupuesto_detalleMetadata))]
    public partial class presupuesto_detalle
    { }
    [MetadataType(typeof(orden_trabajo_cabeceraMetadata))]
    public partial class orden_trabajo_cabecera
    { }
}