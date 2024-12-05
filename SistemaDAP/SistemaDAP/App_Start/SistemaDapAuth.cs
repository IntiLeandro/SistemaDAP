using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SistemaDAP.Models;

namespace SistemaDAP.App_Start
{
    public class SistemaDapAuth : AuthorizeAttribute
    {
        private DBDAPEntities db = new DBDAPEntities();

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            bool authorized = base.AuthorizeCore(httpContext);

            db = new DBDAPEntities();

            if (authorized)
            {
                #region Obtener nombre de controlador y accion
                var routeData = httpContext.Request.RequestContext.RouteData;
                var controller = routeData.GetRequiredString("controller");
                var action = routeData.GetRequiredString("action");
                #endregion

                #region Obtener roles de usuario logueado
                string userName = httpContext.User.Identity.Name;

                //obtener usuario
                AspNetUsers usuario = (from l in db.AspNetUsers where l.UserName == userName select l).FirstOrDefault();
                if (usuario == null)
                    return false;

                //obtener lista de roles
                ICollection<AspNetRoles> roles = usuario.AspNetRoles;

                if (roles.Count <= 0)
                    return false;

                //formatear roles como rol1, rol2, roln
                List<string> listaRoles = (from l in roles select l.Name).ToList();
                string rolesUsuario = string.Join(", ", listaRoles);

                //si es administrador tiene acceso total :)
                if (rolesUsuario.Contains("Administrador"))
                    return true;

                #endregion

                #region Obtener permisos de roles

                //obtener roles permitidos para controlador/accion
                string permisos = (from l in db.permiso_rol where l.controlador == controller && l.accion == action select l.roles).FirstOrDefault();

                if (string.IsNullOrEmpty(permisos))
                    return false;



                #endregion

                #region Comparar listas

                bool canAccess = false;

                string[] permisosConfig = permisos.Split(',');
                var result = listaRoles.Intersect(permisosConfig);

                canAccess = (result.ToList().Count > 0);

                foreach (var item in listaRoles)
                {
                    if (permisos.Contains(item))
                        canAccess = true;
                }

                return canAccess;

                #endregion

            }
            else
            {
                return authorized;
            }
        }
    }
}