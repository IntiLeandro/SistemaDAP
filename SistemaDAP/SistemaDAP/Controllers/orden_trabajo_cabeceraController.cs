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
    public class orden_trabajo_cabeceraController : Controller
    {
        private DBDAPEntities db = new DBDAPEntities();

        // GET: orden_trabajo_cabecera
        public ActionResult Index()
        {
            //var orden_trabajo_cabecera = db.orden_trabajo_cabecera.Include(o => o.cliente).Include(o => o.presupuesto_cabecera);
            return View();//orden_trabajo_cabecera.ToList()
        }

        // GET: orden_trabajo_cabecera/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            orden_trabajo_cabecera orden_trabajo_cabecera = db.orden_trabajo_cabecera.Find(id);
            if (orden_trabajo_cabecera == null)
            {
                return HttpNotFound();
            }
            return View(orden_trabajo_cabecera);
        }

        // GET: orden_trabajo_cabecera/Create
        public ActionResult Create()
        {
            ViewBag.id_cliente = new SelectList(db.cliente, "id_cliente", "ci_ruc");
            ViewBag.id_presupuesto_cabecera = new SelectList(db.presupuesto_cabecera, "id_presupuesto_cabecera", "id_presupuesto_cabecera");
            return View();
        }

        // POST: orden_trabajo_cabecera/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_orden_trabajo,fecha_orden_trabajo,id_cliente,id_presupuesto_cabecera")] orden_trabajo_cabecera orden_trabajo_cabecera)
        {
            if (ModelState.IsValid)
            {
                db.orden_trabajo_cabecera.Add(orden_trabajo_cabecera);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            //ViewBag.id_cliente = new SelectList(db.cliente, "id_cliente", "ci_ruc", orden_trabajo_cabecera.id_cliente);
            ViewBag.id_presupuesto_cabecera = new SelectList(db.presupuesto_cabecera, "id_presupuesto_cabecera", "id_presupuesto_cabecera", orden_trabajo_cabecera.id_presupuesto_cabecera);
            return View(orden_trabajo_cabecera);
        }

        // GET: orden_trabajo_cabecera/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            orden_trabajo_cabecera orden_trabajo_cabecera = db.orden_trabajo_cabecera.Find(id);
            if (orden_trabajo_cabecera == null)
            {
                return HttpNotFound();
            }
            //ViewBag.id_cliente = new SelectList(db.cliente, "id_cliente", "ci_ruc", orden_trabajo_cabecera.id_cliente);
            ViewBag.id_presupuesto_cabecera = new SelectList(db.presupuesto_cabecera, "id_presupuesto_cabecera", "id_presupuesto_cabecera", orden_trabajo_cabecera.id_presupuesto_cabecera);
            return View(orden_trabajo_cabecera);
        }

        // POST: orden_trabajo_cabecera/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_orden_trabajo,fecha_orden_trabajo,id_cliente,id_presupuesto_cabecera")] orden_trabajo_cabecera orden_trabajo_cabecera)
        {
            if (ModelState.IsValid)
            {
                db.Entry(orden_trabajo_cabecera).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            //ViewBag.id_cliente = new SelectList(db.cliente, "id_cliente", "ci_ruc", orden_trabajo_cabecera.id_cliente);
            ViewBag.id_presupuesto_cabecera = new SelectList(db.presupuesto_cabecera, "id_presupuesto_cabecera", "id_presupuesto_cabecera", orden_trabajo_cabecera.id_presupuesto_cabecera);
            return View(orden_trabajo_cabecera);
        }

        // GET: orden_trabajo_cabecera/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            orden_trabajo_cabecera orden_trabajo_cabecera = db.orden_trabajo_cabecera.Find(id);
            if (orden_trabajo_cabecera == null)
            {
                return HttpNotFound();
            }
            return View(orden_trabajo_cabecera);
        }

        // POST: orden_trabajo_cabecera/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            orden_trabajo_cabecera orden_trabajo_cabecera = db.orden_trabajo_cabecera.Find(id);
            db.orden_trabajo_cabecera.Remove(orden_trabajo_cabecera);
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
