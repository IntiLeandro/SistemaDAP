using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SistemaDAP.Models;
using SistemaDAP.App_Start;

namespace SistemaDAP.Controllers
{
    [SistemaDapAuth]
    public class PermisoRolController : Controller
    {
        private DBDAPEntities db = new DBDAPEntities();

        // GET: PermisoRol
        public ActionResult Index()
        {
            return View(db.permiso_rol.ToList());
        }

        // GET: PermisoRol/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            permiso_rol permiso_rol = db.permiso_rol.Find(id);
            if (permiso_rol == null)
            {
                return HttpNotFound();
            }
            return View(permiso_rol);
        }

        // GET: PermisoRol/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PermisoRol/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_permiso_rol,controlador,accion,roles")] permiso_rol permiso_rol)
        {
            if (ModelState.IsValid)
            {
                db.permiso_rol.Add(permiso_rol);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(permiso_rol);
        }

        // GET: PermisoRol/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            permiso_rol permiso_rol = db.permiso_rol.Find(id);
            if (permiso_rol == null)
            {
                return HttpNotFound();
            }
            return View(permiso_rol);
        }

        // POST: PermisoRol/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_permiso_rol,controlador,accion,roles")] permiso_rol permiso_rol)
        {
            if (ModelState.IsValid)
            {
                db.Entry(permiso_rol).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(permiso_rol);
        }

        // GET: PermisoRol/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            permiso_rol permiso_rol = db.permiso_rol.Find(id);
            if (permiso_rol == null)
            {
                return HttpNotFound();
            }
            return View(permiso_rol);
        }

        // POST: PermisoRol/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            permiso_rol permiso_rol = db.permiso_rol.Find(id);
            db.permiso_rol.Remove(permiso_rol);
            db.SaveChanges();
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
    }
}
