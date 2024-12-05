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
    public class departamentoController : Controller
    {
        private DBDAPEntities db = new DBDAPEntities();

        // GET: departamento
        public ActionResult Index()
        {
            var departamento = db.departamento.Include(d => d.pais);
            return View(departamento.ToList());
        }

        // GET: departamento/Details/5
        public ActionResult Details(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            departamento departamento = db.departamento.Find(id);
            if (departamento == null)
            {
                return HttpNotFound();
            }
            return View(departamento);
        }

        // GET: departamento/Create
        public ActionResult Create()
        {
            ViewBag.id_pais = new SelectList(db.pais, "id_pais", "descripcion_pais");
            return View();
        }

        // POST: departamento/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_departamento,id_pais,descripcion_departamento")] departamento departamento)
        {
            if (ModelState.IsValid)
            {
                db.departamento.Add(departamento);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_pais = new SelectList(db.pais, "id_pais", "descripcion_pais", departamento.id_pais);
            return View(departamento);
        }

        // GET: departamento/Edit/5
        public ActionResult Edit(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            departamento departamento = db.departamento.Find(id);
            if (departamento == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_pais = new SelectList(db.pais, "id_pais", "descripcion_pais", departamento.id_pais);
            return View(departamento);
        }

        // POST: departamento/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_departamento,id_pais,descripcion_departamento")] departamento departamento)
        {
            if (ModelState.IsValid)
            {
                db.Entry(departamento).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_pais = new SelectList(db.pais, "id_pais", "descripcion_pais", departamento.id_pais);
            return View(departamento);
        }

        // GET: departamento/Delete/5
        public ActionResult Delete(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            departamento departamento = db.departamento.Find(id);
            if (departamento == null)
            {
                return HttpNotFound();
            }
            return View(departamento);
        }

        // POST: departamento/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal id)
        {
            departamento departamento = db.departamento.Find(id);
            db.departamento.Remove(departamento);
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
