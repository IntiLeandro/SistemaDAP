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
    public class tipo_herramientaController : Controller
    {
        private DBDAPEntities db = new DBDAPEntities();

        // GET: tipo_herramienta
        public ActionResult Index()
        {
            return View(db.tipo_herramienta.ToList());
        }

        // GET: tipo_herramienta/Details/5
        public ActionResult Details(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tipo_herramienta tipo_herramienta = db.tipo_herramienta.Find(id);
            if (tipo_herramienta == null)
            {
                return HttpNotFound();
            }
            return View(tipo_herramienta);
        }

        // GET: tipo_herramienta/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: tipo_herramienta/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_tipo_herramienta,descripcion_tipo_herramienta")] tipo_herramienta tipo_herramienta)
        {
            if (ModelState.IsValid)
            {
                db.tipo_herramienta.Add(tipo_herramienta);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tipo_herramienta);
        }

        // GET: tipo_herramienta/Edit/5
        public ActionResult Edit(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tipo_herramienta tipo_herramienta = db.tipo_herramienta.Find(id);
            if (tipo_herramienta == null)
            {
                return HttpNotFound();
            }
            return View(tipo_herramienta);
        }

        // POST: tipo_herramienta/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_tipo_herramienta,descripcion_tipo_herramienta")] tipo_herramienta tipo_herramienta)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tipo_herramienta).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tipo_herramienta);
        }

        // GET: tipo_herramienta/Delete/5
        public ActionResult Delete(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tipo_herramienta tipo_herramienta = db.tipo_herramienta.Find(id);
            if (tipo_herramienta == null)
            {
                return HttpNotFound();
            }
            return View(tipo_herramienta);
        }

        // POST: tipo_herramienta/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal id)
        {
            tipo_herramienta tipo_herramienta = db.tipo_herramienta.Find(id);
            db.tipo_herramienta.Remove(tipo_herramienta);
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
