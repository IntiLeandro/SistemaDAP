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
    public class tipo_servicioController : Controller
    {
        private DBDAPEntities db = new DBDAPEntities();

        // GET: tipo_servicio
        public ActionResult Index()
        {
            return View(db.tipo_servicio.ToList());
        }

        // GET: tipo_servicio/Details/5
        public ActionResult Details(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tipo_servicio tipo_servicio = db.tipo_servicio.Find(id);
            if (tipo_servicio == null)
            {
                return HttpNotFound();
            }
            return View(tipo_servicio);
        }

        // GET: tipo_servicio/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: tipo_servicio/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_tipo_servicio,descripcion_tipo_servicio")] tipo_servicio tipo_servicio)
        {
            if (ModelState.IsValid)
            {
                db.tipo_servicio.Add(tipo_servicio);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tipo_servicio);
        }

        // GET: tipo_servicio/Edit/5
        public ActionResult Edit(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tipo_servicio tipo_servicio = db.tipo_servicio.Find(id);
            if (tipo_servicio == null)
            {
                return HttpNotFound();
            }
            return View(tipo_servicio);
        }

        // POST: tipo_servicio/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_tipo_servicio,descripcion_tipo_servicio")] tipo_servicio tipo_servicio)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tipo_servicio).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tipo_servicio);
        }

        // GET: tipo_servicio/Delete/5
        public ActionResult Delete(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tipo_servicio tipo_servicio = db.tipo_servicio.Find(id);
            if (tipo_servicio == null)
            {
                return HttpNotFound();
            }
            return View(tipo_servicio);
        }

        // POST: tipo_servicio/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal id)
        {
            tipo_servicio tipo_servicio = db.tipo_servicio.Find(id);
            db.tipo_servicio.Remove(tipo_servicio);
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
