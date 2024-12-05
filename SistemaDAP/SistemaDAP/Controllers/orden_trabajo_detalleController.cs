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
    public class orden_trabajo_detalleController : Controller
    {
        private DBDAPEntities db = new DBDAPEntities();

        // GET: orden_trabajo_detalle
        public ActionResult Index()
        {
            var orden_trabajo_detalle = db.orden_trabajo_detalle.Include(o => o.articulo).Include(o => o.orden_trabajo_cabecera);
            return View(orden_trabajo_detalle.ToList());
        }

        // GET: orden_trabajo_detalle/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            orden_trabajo_detalle orden_trabajo_detalle = db.orden_trabajo_detalle.Find(id);
            if (orden_trabajo_detalle == null)
            {
                return HttpNotFound();
            }
            return View(orden_trabajo_detalle);
        }

        // GET: orden_trabajo_detalle/Create
        public ActionResult Create()
        {
            ViewBag.id_articulo = new SelectList(db.articulo, "id_articulo", "descripcion");
            ViewBag.id_orden_trabajo = new SelectList(db.orden_trabajo_cabecera, "id_orden_trabajo", "id_orden_trabajo");
            return View();
        }

        // POST: orden_trabajo_detalle/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_orden_trabajo_detalle,id_orden_trabajo,id_articulo,cantidad,descripcion_trabajo")] orden_trabajo_detalle orden_trabajo_detalle)
        {
            if (ModelState.IsValid)
            {
                db.orden_trabajo_detalle.Add(orden_trabajo_detalle);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_articulo = new SelectList(db.articulo, "id_articulo", "descripcion", orden_trabajo_detalle.id_articulo);
            ViewBag.id_orden_trabajo = new SelectList(db.orden_trabajo_cabecera, "id_orden_trabajo", "id_orden_trabajo", orden_trabajo_detalle.id_orden_trabajo);
            return View(orden_trabajo_detalle);
        }

        // GET: orden_trabajo_detalle/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            orden_trabajo_detalle orden_trabajo_detalle = db.orden_trabajo_detalle.Find(id);
            if (orden_trabajo_detalle == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_articulo = new SelectList(db.articulo, "id_articulo", "descripcion", orden_trabajo_detalle.id_articulo);
            ViewBag.id_orden_trabajo = new SelectList(db.orden_trabajo_cabecera, "id_orden_trabajo", "id_orden_trabajo", orden_trabajo_detalle.id_orden_trabajo);
            return View(orden_trabajo_detalle);
        }

        // POST: orden_trabajo_detalle/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_orden_trabajo_detalle,id_orden_trabajo,id_articulo,cantidad,descripcion_trabajo")] orden_trabajo_detalle orden_trabajo_detalle)
        {
            if (ModelState.IsValid)
            {
                db.Entry(orden_trabajo_detalle).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_articulo = new SelectList(db.articulo, "id_articulo", "descripcion", orden_trabajo_detalle.id_articulo);
            ViewBag.id_orden_trabajo = new SelectList(db.orden_trabajo_cabecera, "id_orden_trabajo", "id_orden_trabajo", orden_trabajo_detalle.id_orden_trabajo);
            return View(orden_trabajo_detalle);
        }

        // GET: orden_trabajo_detalle/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            orden_trabajo_detalle orden_trabajo_detalle = db.orden_trabajo_detalle.Find(id);
            if (orden_trabajo_detalle == null)
            {
                return HttpNotFound();
            }
            return View(orden_trabajo_detalle);
        }

        // POST: orden_trabajo_detalle/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            orden_trabajo_detalle orden_trabajo_detalle = db.orden_trabajo_detalle.Find(id);
            db.orden_trabajo_detalle.Remove(orden_trabajo_detalle);
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
