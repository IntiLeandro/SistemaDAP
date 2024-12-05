using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SistemaDAP.App_Start;
using SistemaDAP.Models;

namespace SistemaDAP.Controllers
{
    [SistemaDapAuth]
    public class proveedoresController : Controller
    {
        private DBDAPEntities db = new DBDAPEntities();

        // GET: proveedores
        public ActionResult Index()
        {
            var proveedor = db.proveedor.Include(p => p.ciudad).Include(p => p.Departamento).Include(p => p.pais);
            return View(proveedor.ToList());
        }

        // GET: proveedores/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            proveedor proveedor = db.proveedor.Find(id);
            if (proveedor == null)
            {
                return HttpNotFound();
            }
            return View(proveedor);
        }

        // GET: proveedores/Create
        public ActionResult Create()
        {
            ViewBag.id_ciudad = new SelectList(db.ciudad, "id_ciudad", "descripcion");
            ViewBag.id_departamento = new SelectList(db.Departamento, "id_departamento", "descripcion");
            ViewBag.id_pais = new SelectList(db.pais, "id_pais", "descripcion");
            return View();
        }

        // POST: proveedores/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_proveedor,ruc,razon_social,direccion,telefono,email,id_ciudad,id_departamento,id_pais")] proveedor proveedor)
        {
            if (ModelState.IsValid)
            {
                db.proveedor.Add(proveedor);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_ciudad = new SelectList(db.ciudad, "id_ciudad", "descripcion", proveedor.id_ciudad);
            ViewBag.id_departamento = new SelectList(db.Departamento, "id_departamento", "descripcion", proveedor.id_departamento);
            ViewBag.id_pais = new SelectList(db.pais, "id_pais", "descripcion", proveedor.id_pais);
            return View(proveedor);
        }

        // GET: proveedores/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            proveedor proveedor = db.proveedor.Find(id);
            if (proveedor == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_ciudad = new SelectList(db.ciudad, "id_ciudad", "descripcion", proveedor.id_ciudad);
            ViewBag.id_departamento = new SelectList(db.Departamento, "id_departamento", "descripcion", proveedor.id_departamento);
            ViewBag.id_pais = new SelectList(db.pais, "id_pais", "descripcion", proveedor.id_pais);
            return View(proveedor);
        }

        // POST: proveedores/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_proveedor,ruc,razon_social,direccion,telefono,email,id_ciudad,id_departamento,id_pais")] proveedor proveedor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(proveedor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_ciudad = new SelectList(db.ciudad, "id_ciudad", "descripcion", proveedor.id_ciudad);
            ViewBag.id_departamento = new SelectList(db.Departamento, "id_departamento", "descripcion", proveedor.id_departamento);
            ViewBag.id_pais = new SelectList(db.pais, "id_pais", "descripcion", proveedor.id_pais);
            return View(proveedor);
        }

        // GET: proveedores/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            proveedor proveedor = db.proveedor.Find(id);
            if (proveedor == null)
            {
                return HttpNotFound();
            }
            return View(proveedor);
        }

        // POST: proveedores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            proveedor proveedor = db.proveedor.Find(id);
            db.proveedor.Remove(proveedor);
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
