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
    public class gruposController : Controller
    {
        private DBDAPEntities db = new DBDAPEntities();

        // GET: grupos
        public ActionResult Index()
        {
            var grupo = db.grupo.Include(g => g.familia);
            return View(grupo.ToList());
        }

        // GET: grupos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            grupo grupo = db.grupo.Find(id);
            if (grupo == null)
            {
                return HttpNotFound();
            }
            return View(grupo);
        }

        // GET: grupos/Create
        public ActionResult Create()
        {
            ViewBag.id_familia = new SelectList(db.familia, "id_familia", "descripcion");
            return View();
        }

        // POST: grupos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_grupo,descripcion,id_familia")] grupo grupo)
        {
            if (ModelState.IsValid)
            {
                db.grupo.Add(grupo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_familia = new SelectList(db.familia, "id_familia", "descripcion", grupo.id_familia);
            return View(grupo);
        }

        // GET: grupos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            grupo grupo = db.grupo.Find(id);
            if (grupo == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_familia = new SelectList(db.familia, "id_familia", "descripcion", grupo.id_familia);
            return View(grupo);
        }

        // POST: grupos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_grupo,descripcion,id_familia")] grupo grupo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(grupo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_familia = new SelectList(db.familia, "id_familia", "descripcion", grupo.id_familia);
            return View(grupo);
        }

        // GET: grupos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            grupo grupo = db.grupo.Find(id);
            if (grupo == null)
            {
                return HttpNotFound();
            }
            return View(grupo);
        }

        // POST: grupos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            grupo grupo = db.grupo.Find(id);
            db.grupo.Remove(grupo);
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
