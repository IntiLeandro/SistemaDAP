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
    public class presupuesto_detalleController : Controller
    {
        private DBDAPEntities db = new DBDAPEntities();

        // GET: presupuesto_detalle
        public ActionResult Index()
        {
            var presupuesto_detalle = db.presupuesto_detalle.Include(p => p.articulo).Include(p => p.presupuesto_cabecera);
            return View(presupuesto_detalle.ToList());
        }

        // GET: presupuesto_detalle/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            presupuesto_detalle presupuesto_detalle = db.presupuesto_detalle.Find(id);
            if (presupuesto_detalle == null)
            {
                return HttpNotFound();
            }
            return View(presupuesto_detalle);
        }

        // GET: presupuesto_detalle/Create
        public ActionResult Create()
        {
            ViewBag.id_articulo = new SelectList(db.articulo, "id_articulo", "descripcion");
            ViewBag.id_presupuesto_cabecera = new SelectList(db.presupuesto_cabecera, "id_presupuesto_cabecera", "id_presupuesto_cabecera");
            return View();
        }

        // POST: presupuesto_detalle/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_presupuesto_detalle,id_presupuesto_cabecera,id_articulo,cantidad,precio_unitario")] presupuesto_detalle presupuesto_detalle)
        {
            if (ModelState.IsValid)
            {
                db.presupuesto_detalle.Add(presupuesto_detalle);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_articulo = new SelectList(db.articulo, "id_articulo", "descripcion", presupuesto_detalle.id_articulo);
            ViewBag.id_presupuesto_cabecera = new SelectList(db.presupuesto_cabecera, "id_presupuesto_cabecera", "id_presupuesto_cabecera", presupuesto_detalle.id_presupuesto_cabecera);
            return View(presupuesto_detalle);
        }

        // GET: presupuesto_detalle/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            presupuesto_detalle presupuesto_detalle = db.presupuesto_detalle.Find(id);
            if (presupuesto_detalle == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_articulo = new SelectList(db.articulo, "id_articulo", "descripcion", presupuesto_detalle.id_articulo);
            ViewBag.id_presupuesto_cabecera = new SelectList(db.presupuesto_cabecera, "id_presupuesto_cabecera", "id_presupuesto_cabecera", presupuesto_detalle.id_presupuesto_cabecera);
            return View(presupuesto_detalle);
        }

        // POST: presupuesto_detalle/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_presupuesto_detalle,id_presupuesto_cabecera,id_articulo,cantidad,precio_unitario")] presupuesto_detalle presupuesto_detalle)
        {
            if (ModelState.IsValid)
            {
                db.Entry(presupuesto_detalle).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_articulo = new SelectList(db.articulo, "id_articulo", "descripcion", presupuesto_detalle.id_articulo);
            ViewBag.id_presupuesto_cabecera = new SelectList(db.presupuesto_cabecera, "id_presupuesto_cabecera", "id_presupuesto_cabecera", presupuesto_detalle.id_presupuesto_cabecera);
            return View(presupuesto_detalle);
        }

        // GET: presupuesto_detalle/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            presupuesto_detalle presupuesto_detalle = db.presupuesto_detalle.Find(id);
            if (presupuesto_detalle == null)
            {
                return HttpNotFound();
            }
            return View(presupuesto_detalle);
        }

        // POST: presupuesto_detalle/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            presupuesto_detalle presupuesto_detalle = db.presupuesto_detalle.Find(id);
            db.presupuesto_detalle.Remove(presupuesto_detalle);
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
