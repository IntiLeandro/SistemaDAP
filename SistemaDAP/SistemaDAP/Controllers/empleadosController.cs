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
    public class empleadosController : Controller
    {
        private DBDAPEntities db = new DBDAPEntities();

        // GET: empleados
        public ActionResult Index()
        {
            var empleado = db.empleado.Include(e => e.AspNetUsers).Include(e => e.cargo).Include(e => e.ciudad).Include(e => e.Departamento).Include(e => e.pais);
            return View(empleado.ToList());
        }

        // GET: empleados/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            empleado empleado = db.empleado.Find(id);
            if (empleado == null)
            {
                return HttpNotFound();
            }
            return View(empleado);
        }

        // GET: empleados/Create
        public ActionResult Create()
        {
            ViewBag.id_usuario = new SelectList(db.AspNetUsers, "Id", "Email");
            ViewBag.id_cargo = new SelectList(db.cargo, "id_cargo", "descripcion");
            ViewBag.id_ciudad = new SelectList(db.ciudad, "id_ciudad", "descripcion");
            ViewBag.id_departamento = new SelectList(db.Departamento, "id_departamento", "descripcion");
            ViewBag.id_pais = new SelectList(db.pais, "id_pais", "descripcion");
            return View();
        }

        // POST: empleados/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_empleado,numero_documento,nombre,apellido,direccion,telefono,sexo,fecha_nacimiento,email,estado,id_pais,id_usuario,id_cargo,id_departamento,id_ciudad")] empleado empleado)
        {
            if (ModelState.IsValid)
            {
                db.empleado.Add(empleado);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_usuario = new SelectList(db.AspNetUsers, "Id", "Email", empleado.id_usuario);
            ViewBag.id_cargo = new SelectList(db.cargo, "id_cargo", "descripcion", empleado.id_cargo);
            ViewBag.id_ciudad = new SelectList(db.ciudad, "id_ciudad", "descripcion", empleado.id_ciudad);
            ViewBag.id_departamento = new SelectList(db.Departamento, "id_departamento", "descripcion", empleado.id_departamento);
            ViewBag.id_pais = new SelectList(db.pais, "id_pais", "descripcion", empleado.id_pais);
            return View(empleado);
        }

        // GET: empleados/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            empleado empleado = db.empleado.Find(id);
            if (empleado == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_usuario = new SelectList(db.AspNetUsers, "Id", "Email", empleado.id_usuario);
            ViewBag.id_cargo = new SelectList(db.cargo, "id_cargo", "descripcion", empleado.id_cargo);
            ViewBag.id_ciudad = new SelectList(db.ciudad, "id_ciudad", "descripcion", empleado.id_ciudad);
            ViewBag.id_departamento = new SelectList(db.Departamento, "id_departamento", "descripcion", empleado.id_departamento);
            ViewBag.id_pais = new SelectList(db.pais, "id_pais", "descripcion", empleado.id_pais);
            return View(empleado);
        }

        // POST: empleados/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_empleado,numero_documento,nombre,apellido,direccion,telefono,sexo,fecha_nacimiento,email,estado,id_pais,id_usuario,id_cargo,id_departamento,id_ciudad")] empleado empleado)
        {
            if (ModelState.IsValid)
            {
                db.Entry(empleado).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_usuario = new SelectList(db.AspNetUsers, "Id", "Email", empleado.id_usuario);
            ViewBag.id_cargo = new SelectList(db.cargo, "id_cargo", "descripcion", empleado.id_cargo);
            ViewBag.id_ciudad = new SelectList(db.ciudad, "id_ciudad", "descripcion", empleado.id_ciudad);
            ViewBag.id_departamento = new SelectList(db.Departamento, "id_departamento", "descripcion", empleado.id_departamento);
            ViewBag.id_pais = new SelectList(db.pais, "id_pais", "descripcion", empleado.id_pais);
            return View(empleado);
        }

        // GET: empleados/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            empleado empleado = db.empleado.Find(id);
            if (empleado == null)
            {
                return HttpNotFound();
            }
            return View(empleado);
        }

        // POST: empleados/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            empleado empleado = db.empleado.Find(id);
            db.empleado.Remove(empleado);
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
