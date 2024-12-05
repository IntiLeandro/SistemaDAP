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
    public class articulosController : Controller
    {
        private DBDAPEntities db = new DBDAPEntities();

        // GET: articulos
        public ActionResult Index()
        {
            var articulo = db.articulo.Include(a => a.tipo_articulo).Include(a => a.grupo).Include(a => a.impuesto);
            return View(articulo.ToList());
        }

        // GET: articulos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            articulo articulo = db.articulo.Find(id);
            if (articulo == null)
            {
                return HttpNotFound();
            }
            return View(articulo);
        }

        // GET: articulos/Create
        public ActionResult Create()
        {
            ViewBag.id_tipo_articulo = new SelectList(db.tipo_articulo, "id_tipo_articulo", "descripcion");
            ViewBag.id_grupo = new SelectList(db.grupo, "id_grupo", "descripcion");
            ViewBag.id_iva = new SelectList(db.impuesto, "id_iva", "descripcion");
            return View();
        }

        // POST: articulos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_articulo,descripcion,precio_venta,cantidad,id_tipo_articulo,id_iva,precio_costo,id_grupo")] articulo articulo)
        {
            if (ModelState.IsValid)
            {
                db.articulo.Add(articulo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_tipo_articulo = new SelectList(db.tipo_articulo, "id_tipo_articulo", "descripcion", articulo.id_tipo_articulo);
            ViewBag.id_grupo = new SelectList(db.grupo, "id_grupo", "descripcion", articulo.id_grupo);
            ViewBag.id_iva = new SelectList(db.impuesto, "id_iva", "descripcion", articulo.id_iva);
            return View(articulo);
        }

        // GET: articulos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            articulo articulo = db.articulo.Find(id);
            if (articulo == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_tipo_articulo = new SelectList(db.tipo_articulo, "id_tipo_articulo", "descripcion", articulo.id_tipo_articulo);
            ViewBag.id_grupo = new SelectList(db.grupo, "id_grupo", "descripcion", articulo.id_grupo);
            ViewBag.id_iva = new SelectList(db.impuesto, "id_iva", "descripcion", articulo.id_iva);
            return View(articulo);
        }

        // POST: articulos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_articulo,descripcion,precio_venta,cantidad,id_tipo_articulo,id_iva,precio_costo,id_grupo")] articulo articulo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(articulo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_tipo_articulo = new SelectList(db.tipo_articulo, "id_tipo_articulo", "descripcion", articulo.id_tipo_articulo);
            ViewBag.id_grupo = new SelectList(db.grupo, "id_grupo", "descripcion", articulo.id_grupo);
            ViewBag.id_iva = new SelectList(db.impuesto, "id_iva", "descripcion", articulo.id_iva);
            return View(articulo);
        }

        // GET: articulos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            articulo articulo = db.articulo.Find(id);
            if (articulo == null)
            {
                return HttpNotFound();
            }
            return View(articulo);
        }

        // POST: articulos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            articulo articulo = db.articulo.Find(id);
            db.articulo.Remove(articulo);
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
