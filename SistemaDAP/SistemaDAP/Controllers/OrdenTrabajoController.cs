using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SistemaDAP.Models;
using SistemaDAP.App_Start;
using System.Net;

namespace SistemaDAP.Controllers
{
    [SistemaDapAuth]
    public class OrdenTrabajoController : Controller
    {
        private DBDAPEntities db = new DBDAPEntities();
        // GET: OrdenTrabajo
        public ActionResult Index()
        {
            OrdenTrabajoCabeceraView ordenTrabajoCabeceraView = new OrdenTrabajoCabeceraView();
            ordenTrabajoCabeceraView.Presupuesto = new PresupuestoOrdenTrabajoCabecera();
            ordenTrabajoCabeceraView.Articulo = new List<ArticuloOrdenTrabajoCabecera>();
            ordenTrabajoCabeceraView.Orden_Trabajo = new orden_trabajo_cabecera();
            Session["OrdenTrabajoCabeceraView"] = ordenTrabajoCabeceraView;
            var list = db.presupuesto_cabecera.ToList();
            ViewBag.id_presupuesto_cabecera = new SelectList(list, "id_presupuesto_cabecera", "id_presupuesto_cabecera");
            return View(ordenTrabajoCabeceraView);//presupuestoCabeceraView
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(OrdenTrabajoCabeceraView ordenTrabajoView)
        {
            ordenTrabajoView = Session["OrdenTrabajoCabeceraView"] as OrdenTrabajoCabeceraView;
            int idPresupuestoCabecera = int.Parse(Request["id_presupuesto_cabecera"]);
            DateTime fechaOrdenTrabajo = Convert.ToDateTime(Request["Presupuesto.FechaOrdenTrabajo"]).Date;
            string descripcionOrdenTrabajo = (Request["descripcion_orden_trabajo"]);
            orden_trabajo_cabecera nuevaOrdenTrabajo = new orden_trabajo_cabecera
            {
                id_presupuesto_cabecera = idPresupuestoCabecera,
                fecha_orden_trabajo = fechaOrdenTrabajo,
                descripcion_orden_trabajo = descripcionOrdenTrabajo
            };
            db.orden_trabajo_cabecera.Add(nuevaOrdenTrabajo);
            db.SaveChanges();

            //revisar si es necesario cambiar en DB el tipo de dato decimal a int!!!!!
            int ultimoIdOrdenTrabajo = Convert.ToInt32(db.orden_trabajo_cabecera.ToList().Select(o => o.id_orden_trabajo).Max());

            foreach (ArticuloOrdenTrabajoCabecera item in ordenTrabajoView.Articulo)
            {
                var detalles = new orden_trabajo_detalle()
                {
                    id_orden_trabajo = ultimoIdOrdenTrabajo,
                    id_articulo = item.id_articulo,
                    cantidad = item.cantidad,
                    //descripcion_trabajo = item.descripcion
                };
                db.orden_trabajo_detalle.Add(detalles);
            }
            db.SaveChanges();
            ordenTrabajoView = Session["OrdenTrabajoCabeceraView"] as OrdenTrabajoCabeceraView;
            var list = db.presupuesto_cabecera.ToList();
            ViewBag.id_presupuesto_cabecera = new SelectList(list, "id_presupuesto_cabecera", "id_presupuesto_cabecera");
            TempData["notice"] = "Registrado exitosamente";
            //return View(presupuestoView);
            return RedirectToAction("Index", "OrdenTrabajo");
        }

        public ActionResult AddArticulo()
        {
            var listaArticulo = db.articulo.ToList();
            ViewBag.id_articulo = new SelectList(listaArticulo, "id_articulo", "descripcion");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddArticulo(ArticuloOrdenTrabajoCabecera articuloOrdenTrabajo)
        {
            var ordenTrabajoView = Session["OrdenTrabajoCabeceraView"] as OrdenTrabajoCabeceraView;
            var idArticulos = int.Parse(Request["id_articulo"]);
            var Articulos = db.articulo.Find(idArticulos);
            articuloOrdenTrabajo = new ArticuloOrdenTrabajoCabecera()
            {
                id_articulo = Articulos.id_articulo,
                descripcion = Articulos.descripcion,
                //precio_venta = Articulos.precio_venta,
                cantidad = int.Parse(Request["cantidad"])
            };
            ordenTrabajoView.Articulo.Add(articuloOrdenTrabajo);
            var list = db.presupuesto_cabecera.ToList();
            ViewBag.id_presupuesto_cabecera = new SelectList(list, "id_presupuesto_cabecera", "id_presupuesto_cabecera");
            var listaArticulo = db.articulo.ToList();
            ViewBag.id_articulo = new SelectList(listaArticulo, "id_articulo", "descripcion");
            return View("Index", ordenTrabajoView);
        }

        // GET: /Delete/5
        public ActionResult Delete()
        {
            var ordenTrabajoView = Session["OrdenTrabajoCabeceraView"] as OrdenTrabajoCabeceraView;
            int index = 0;
            var item = ordenTrabajoView.Articulo.ToArray()[index];
            ordenTrabajoView.Articulo.Remove(item);
            var list = db.presupuesto_cabecera.ToList();
            ViewBag.id_presupuesto_cabecera = new SelectList(list, "id_presupuesto_cabecera", "id_presupuesto_cabecera");
            return View("Index", ordenTrabajoView);
        }

    }
}