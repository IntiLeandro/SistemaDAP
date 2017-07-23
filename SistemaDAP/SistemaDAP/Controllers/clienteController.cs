using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SistemaDAP.Models;

namespace SistemaDAP.Controllers
{
    public class clienteController : Controller
    {
        private DBDAPEntities db = new DBDAPEntities();

        // GET: cliente
        public ActionResult Index()
        {
            var cliente = db.cliente.Include(c => c.ciudad).Include(c => c.departamento).Include(c => c.pais);
            return View(cliente.ToList());
        }

        // GET: cliente/Details/5
        public ActionResult Details(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            cliente cliente = db.cliente.Find(id);
            if (cliente == null)
            {
                return HttpNotFound();
            }
            return View(cliente);
        }

        // GET: cliente/Create
        public ActionResult Create()
        {
            ViewBag.id_ciudad = new SelectList(db.ciudad, "id_ciudad", "descripcion_ciudad");
            ViewBag.id_departamento = new SelectList(db.departamento, "id_departamento", "descripcion_departamento");
            ViewBag.id_pais = new SelectList(db.pais, "id_pais", "descripcion_pais");
            return View();
        }

        // POST: cliente/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_cliente,id_departamento,id_ciudad,id_pais,nombre_razon_social_cliente,direccion_cliente,telefono_cliente,email_cliente,ci_ruc_cliente,fecha_nacimiento_cliente,sexo_cliente")] cliente cliente)
        {
            if (ModelState.IsValid)
            {
                db.cliente.Add(cliente);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_ciudad = new SelectList(db.ciudad, "id_ciudad", "descripcion_ciudad", cliente.id_ciudad);
            ViewBag.id_departamento = new SelectList(db.departamento, "id_departamento", "descripcion_departamento", cliente.id_departamento);
            ViewBag.id_pais = new SelectList(db.pais, "id_pais", "descripcion_pais", cliente.id_pais);
            return View(cliente);
        }

        // GET: cliente/Edit/5
        public ActionResult Edit(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            cliente cliente = db.cliente.Find(id);
            if (cliente == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_ciudad = new SelectList(db.ciudad, "id_ciudad", "descripcion_ciudad", cliente.id_ciudad);
            ViewBag.id_departamento = new SelectList(db.departamento, "id_departamento", "descripcion_departamento", cliente.id_departamento);
            ViewBag.id_pais = new SelectList(db.pais, "id_pais", "descripcion_pais", cliente.id_pais);
            return View(cliente);
        }

        // POST: cliente/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_cliente,id_departamento,id_ciudad,id_pais,nombre_razon_social_cliente,direccion_cliente,telefono_cliente,email_cliente,ci_ruc_cliente,fecha_nacimiento_cliente,sexo_cliente")] cliente cliente)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cliente).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_ciudad = new SelectList(db.ciudad, "id_ciudad", "descripcion_ciudad", cliente.id_ciudad);
            ViewBag.id_departamento = new SelectList(db.departamento, "id_departamento", "descripcion_departamento", cliente.id_departamento);
            ViewBag.id_pais = new SelectList(db.pais, "id_pais", "descripcion_pais", cliente.id_pais);
            return View(cliente);
        }

        // GET: cliente/Delete/5
        public ActionResult Delete(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            cliente cliente = db.cliente.Find(id);
            if (cliente == null)
            {
                return HttpNotFound();
            }
            return View(cliente);
        }

        // POST: cliente/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal id)
        {
            cliente cliente = db.cliente.Find(id);
            db.cliente.Remove(cliente);
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
