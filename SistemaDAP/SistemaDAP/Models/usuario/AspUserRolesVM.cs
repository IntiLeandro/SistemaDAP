using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaDAP.Models.usuario
{
    public class AspUserRolesVM
    {
        public AspNetUsers usuario { get; set; }
        public ICollection<AspNetRoles> rolesAsignados { get; set; }
        public ICollection<AspNetRoles> rolesNoAsignados { get; set; }
    }
}