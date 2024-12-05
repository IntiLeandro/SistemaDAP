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
    public class caja_apertura_cierreController : Controller
    {
        private DBDAPEntities db = new DBDAPEntities();

        // GET: caja_apertura_cierre
        public ActionResult Index()
        {
            var caja_apertura_cierre = db.caja_apertura_cierre.Include(c => c.AspNetUsers).Include(c => c.caja);
            return View(caja_apertura_cierre.ToList());
        }

        // GET: caja_apertura_cierre/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            caja_apertura_cierre caja_apertura_cierre = db.caja_apertura_cierre.Find(id);
            if (caja_apertura_cierre == null)
            {
                return HttpNotFound();
            }
            return View(caja_apertura_cierre);
        }

        // GET: caja_apertura_cierre/Create
        public ActionResult Create()
        {
            ViewBag.id_usuario = new SelectList(db.AspNetUsers, "Id", "Email");
            ViewBag.id_caja = new SelectList(db.caja, "id_caja", "descripcion");
            return View();
        }

        // POST: caja_apertura_cierre/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_apertura_cierre,fecha_apertura,monto_inicial,fecha_cierre,monto_final,estado_apertura_cierre,id_usuario,id_caja")] caja_apertura_cierre caja_apertura_cierre)
        {
            if (ModelState.IsValid)
            {
                db.caja_apertura_cierre.Add(caja_apertura_cierre);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_usuario = new SelectList(db.AspNetUsers, "Id", "Email", caja_apertura_cierre.id_usuario);
            ViewBag.id_caja = new SelectList(db.caja, "id_caja", "descripcion", caja_apertura_cierre.id_caja);
            return View(caja_apertura_cierre);
        }

        // GET: caja_apertura_cierre/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            caja_apertura_cierre caja_apertura_cierre = db.caja_apertura_cierre.Find(id);
            if (caja_apertura_cierre == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_usuario = new SelectList(db.AspNetUsers, "Id", "Email", caja_apertura_cierre.id_usuario);
            ViewBag.id_caja = new SelectList(db.caja, "id_caja", "descripcion", caja_apertura_cierre.id_caja);
            return View(caja_apertura_cierre);
        }

        // POST: caja_apertura_cierre/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_apertura_cierre,fecha_apertura,monto_inicial,fecha_cierre,monto_final,estado_apertura_cierre,id_usuario,id_caja")] caja_apertura_cierre caja_apertura_cierre)
        {
            if (ModelState.IsValid)
            {
                db.Entry(caja_apertura_cierre).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_usuario = new SelectList(db.AspNetUsers, "Id", "Email", caja_apertura_cierre.id_usuario);
            ViewBag.id_caja = new SelectList(db.caja, "id_caja", "descripcion", caja_apertura_cierre.id_caja);
            return View(caja_apertura_cierre);
        }

        // GET: caja_apertura_cierre/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            caja_apertura_cierre caja_apertura_cierre = db.caja_apertura_cierre.Find(id);
            if (caja_apertura_cierre == null)
            {
                return HttpNotFound();
            }
            return View(caja_apertura_cierre);
        }

        // POST: caja_apertura_cierre/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            caja_apertura_cierre caja_apertura_cierre = db.caja_apertura_cierre.Find(id);
            db.caja_apertura_cierre.Remove(caja_apertura_cierre);
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
