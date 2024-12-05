using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace SistemaDAP.Models
{
    public class clientesController : Controller
    {
        private DBDAPEntities db = new DBDAPEntities();

        // GET: clientes
        public ActionResult Index()
        {
            var cliente = db.cliente.Include(c => c.ciudad).Include(c => c.Departamento).Include(c => c.pais);
            return View(cliente.ToList());
        }

        // GET: clientes/Details/5
        public ActionResult Details(int? id)
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

        // GET: clientes/Create
        public ActionResult Create()
        {
            ViewBag.id_ciudad = new SelectList(db.ciudad, "id_ciudad", "descripcion");
            ViewBag.id_departamento = new SelectList(db.Departamento, "id_departamento", "descripcion");
            ViewBag.id_pais = new SelectList(db.pais, "id_pais", "descripcion");
            return View();
        }

        // POST: clientes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_cliente,ci_ruc,nombre_razon_social,direccion,telefono,fecha_nacimiento,email,sexo,id_ciudad,id_departamento,id_pais")] cliente cliente)
        {
            if (ModelState.IsValid)
            {
                db.cliente.Add(cliente);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_ciudad = new SelectList(db.ciudad, "id_ciudad", "descripcion", cliente.id_ciudad);
            ViewBag.id_departamento = new SelectList(db.Departamento, "id_departamento", "descripcion", cliente.id_departamento);
            ViewBag.id_pais = new SelectList(db.pais, "id_pais", "descripcion", cliente.id_pais);
            return View(cliente);
        }

        // GET: clientes/Edit/5
        public ActionResult Edit(int? id)
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
            ViewBag.id_ciudad = new SelectList(db.ciudad, "id_ciudad", "descripcion", cliente.id_ciudad);
            ViewBag.id_departamento = new SelectList(db.Departamento, "id_departamento", "descripcion", cliente.id_departamento);
            ViewBag.id_pais = new SelectList(db.pais, "id_pais", "descripcion", cliente.id_pais);
            return View(cliente);
        }

        // POST: clientes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_cliente,ci_ruc,nombre_razon_social,direccion,telefono,fecha_nacimiento,email,sexo,id_ciudad,id_departamento,id_pais")] cliente cliente)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cliente).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_ciudad = new SelectList(db.ciudad, "id_ciudad", "descripcion", cliente.id_ciudad);
            ViewBag.id_departamento = new SelectList(db.Departamento, "id_departamento", "descripcion", cliente.id_departamento);
            ViewBag.id_pais = new SelectList(db.pais, "id_pais", "descripcion", cliente.id_pais);
            return View(cliente);
        }

        // GET: clientes/Delete/5
        public ActionResult Delete(int? id)
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

        // POST: clientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
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
