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
    public class familiasController : Controller
    {
        private DBDAPEntities db = new DBDAPEntities();

        // GET: familias
        public ActionResult Index()
        {
            var familia = db.familia.Include(f => f.marca);
            return View(familia.ToList());
        }

        // GET: familias/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            familia familia = db.familia.Find(id);
            if (familia == null)
            {
                return HttpNotFound();
            }
            return View(familia);
        }

        // GET: familias/Create
        public ActionResult Create()
        {
            ViewBag.id_marca = new SelectList(db.marca, "id_marca", "descripcion");
            return View();
        }

        // POST: familias/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_familia,descripcion,id_marca")] familia familia)
        {
            if (ModelState.IsValid)
            {
                db.familia.Add(familia);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_marca = new SelectList(db.marca, "id_marca", "descripcion", familia.id_marca);
            return View(familia);
        }

        // GET: familias/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            familia familia = db.familia.Find(id);
            if (familia == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_marca = new SelectList(db.marca, "id_marca", "descripcion", familia.id_marca);
            return View(familia);
        }

        // POST: familias/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_familia,descripcion,id_marca")] familia familia)
        {
            if (ModelState.IsValid)
            {
                db.Entry(familia).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_marca = new SelectList(db.marca, "id_marca", "descripcion", familia.id_marca);
            return View(familia);
        }

        // GET: familias/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            familia familia = db.familia.Find(id);
            if (familia == null)
            {
                return HttpNotFound();
            }
            return View(familia);
        }

        // POST: familias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            familia familia = db.familia.Find(id);
            db.familia.Remove(familia);
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
