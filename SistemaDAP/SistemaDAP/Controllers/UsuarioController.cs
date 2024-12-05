using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SistemaDAP.Models;
using SistemaDAP.Models.usuario;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.Owin;
using SistemaDAP.App_Start;

namespace SistemaDAP.Controllers
{
    [SistemaDapAuth]
    public class UsuarioController : Controller
    {
        private DBDAPEntities db = new DBDAPEntities();
        private ApplicationUserManager _userManager;

        // GET: Usuario
        public ActionResult Index()
        {
            return View(db.AspNetUsers.ToList());
        }

        // GET: Usuario/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AspNetUsers aspNetUsers = db.AspNetUsers.Find(id);
            if (aspNetUsers == null)
            {
                return HttpNotFound();
            }
            return View(aspNetUsers);
        }

        // GET: Usuario/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Usuario/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Email,EmailConfirmed,PasswordHash,SecurityStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount,UserName")] AspNetUsers aspNetUsers)
        {
            if (ModelState.IsValid)
            {
                db.AspNetUsers.Add(aspNetUsers);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(aspNetUsers);
        }

        // GET: Usuario/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AspNetUsers aspNetUsers = db.AspNetUsers.Find(id);
            if (aspNetUsers == null)
            {
                return HttpNotFound();
            }
            return View(aspNetUsers);
        }

        // POST: Usuario/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Email,EmailConfirmed,PasswordHash,SecurityStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount,UserName")] AspNetUsers aspNetUsers)
        {
            if (ModelState.IsValid)
            {
                AspNetUsers user = db.AspNetUsers.Find(aspNetUsers.Id);
                user.UserName = aspNetUsers.UserName;
                user.Email = aspNetUsers.Email;

                //db.Entry(aspNetUsers).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(aspNetUsers);
        }

        // GET: Usuario/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AspNetUsers aspNetUsers = db.AspNetUsers.Find(id);
            if (aspNetUsers == null)
            {
                return HttpNotFound();
            }
            return View(aspNetUsers);
        }

        // POST: Usuario/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            AspNetUsers aspNetUsers = db.AspNetUsers.Find(id);
            db.AspNetUsers.Remove(aspNetUsers);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult RolesDeUsuario(string idUsuario)
        {
            if (idUsuario == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            AspNetUsers aspNetUsers = db.AspNetUsers.Find(idUsuario);
            if (aspNetUsers == null)
            {
                return HttpNotFound();
            }

            AspUserRolesVM userRoles = new AspUserRolesVM();
            userRoles.usuario = aspNetUsers;
            userRoles.rolesAsignados = aspNetUsers.AspNetRoles;

            userRoles.rolesNoAsignados = (((from l in db.AspNetRoles select l).ToList()).Except(userRoles.rolesAsignados)).ToList();

            return View(userRoles);
        }

        public ActionResult HabilitarRol(string idUsuario, string idRol)
        {
            if (idUsuario == null || idRol == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AspNetUsers aspNetUsers = db.AspNetUsers.Find(idUsuario);
            if (aspNetUsers == null)
            {
                return HttpNotFound();
            }

            //Asignar rol al usuario
            AspNetUsers user = db.AspNetUsers.Find(idUsuario);
            AspNetRoles rol = db.AspNetRoles.Find(idRol);

            user.AspNetRoles.Add(rol);
            db.SaveChanges();

            return RedirectToAction("RolesDeUsuario", new { idUsuario = idUsuario });
        }

        public ActionResult DeshabilitarRol(string idUsuario, string idRol)
        {
            if (idUsuario == null || idRol == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AspNetUsers aspNetUsers = db.AspNetUsers.Find(idUsuario);
            if (aspNetUsers == null)
            {
                return HttpNotFound();
            }

            //Asignar rol al usuario
            AspNetUsers user = db.AspNetUsers.Find(idUsuario);
            AspNetRoles rol = db.AspNetRoles.Find(idRol);

            user.AspNetRoles.Remove(rol);
            db.SaveChanges();

            return RedirectToAction("RolesDeUsuario", new { idUsuario = idUsuario });
        }
        public ActionResult ResetPass(string idUsuario)
        {
            if (string.IsNullOrEmpty(idUsuario))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AspNetUsers user = db.AspNetUsers.Find(idUsuario);
            if (user == null)
            {
                return HttpNotFound();
            }

            ResetPassVM rp = new ResetPassVM();
            rp.Email = user.Email;
            rp.Usuario = user.UserName;

            return View(rp);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPass(ResetPassVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await UserManager.FindByNameAsync(model.Usuario);
            if (user == null)
            {

            }
            //string resetToken = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
            //var result = await UserManager.ResetPasswordAsync(user.Id, resetToken, model.NewPassword);

            UserManager.RemovePassword(user.Id);
            var result = UserManager.AddPassword(user.Id, model.NewPassword);

            if (result.Succeeded)
            {

            }
            else
            {

            }

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
    }
}
