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
    public class tipo_articuloController : Controller
    {
        private DBDAPEntities db = new DBDAPEntities();

        // GET: tipo_articulo
        public ActionResult Index()
        {
            return View(db.tipo_articulo.ToList());
        }

        // GET: tipo_articulo/Details/5
        public ActionResult Details(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tipo_articulo tipo_articulo = db.tipo_articulo.Find(id);
            if (tipo_articulo == null)
            {
                return HttpNotFound();
            }
            return View(tipo_articulo);
        }

        // GET: tipo_articulo/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: tipo_articulo/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_tipo_articulo,descripcion_tipo_articulo")] tipo_articulo tipo_articulo)
        {
            if (ModelState.IsValid)
            {
                db.tipo_articulo.Add(tipo_articulo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tipo_articulo);
        }

        // GET: tipo_articulo/Edit/5
        public ActionResult Edit(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tipo_articulo tipo_articulo = db.tipo_articulo.Find(id);
            if (tipo_articulo == null)
            {
                return HttpNotFound();
            }
            return View(tipo_articulo);
        }

        // POST: tipo_articulo/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_tipo_articulo,descripcion_tipo_articulo")] tipo_articulo tipo_articulo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tipo_articulo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tipo_articulo);
        }

        // GET: tipo_articulo/Delete/5
        public ActionResult Delete(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tipo_articulo tipo_articulo = db.tipo_articulo.Find(id);
            if (tipo_articulo == null)
            {
                return HttpNotFound();
            }
            return View(tipo_articulo);
        }

        // POST: tipo_articulo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal id)
        {
            tipo_articulo tipo_articulo = db.tipo_articulo.Find(id);
            db.tipo_articulo.Remove(tipo_articulo);
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
