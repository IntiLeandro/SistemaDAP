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
    public class PresupuestoController : Controller
    {
        private DBDAPEntities db = new DBDAPEntities();
        // GET: Presupuesto
        public ActionResult Index()
        {
            PresupuestoCabeceraView presupuestoCabeceraView = new PresupuestoCabeceraView();
            presupuestoCabeceraView.Cliente = new ClientePresupuestoCabecera();
            presupuestoCabeceraView.Articulo = new List<ArticuloPresupuestoCabecera>();
            Session["PresupuestoCabeceraView"] = presupuestoCabeceraView;
            var list = db.cliente.ToList();
            ViewBag.id_cliente = new SelectList(list,"id_cliente","nombre_razon_social");
            return View(presupuestoCabeceraView);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(PresupuestoCabeceraView presupuestoView)
        {
            presupuestoView = Session["PresupuestoCabeceraView"] as PresupuestoCabeceraView;
            int codigoCliente = int.Parse(Request["id_cliente"]);
            DateTime fechaPresupuesto = Convert.ToDateTime(Request["Cliente.FechaPresupuesto"]).Date;
            presupuesto_cabecera nuevoPresupuesto = new presupuesto_cabecera
            {
                id_cliente = codigoCliente,
                fecha_presupuesto = fechaPresupuesto
            };
            db.presupuesto_cabecera.Add(nuevoPresupuesto);
            db.SaveChanges();

            //revisar si es necesario cambiar en DB el tipo de dato decimal a int!!!!!
            int ultimoIdPresupuesto = Convert.ToInt32(db.presupuesto_cabecera.ToList().Select(o=> o.id_presupuesto_cabecera).Max());

            foreach (ArticuloPresupuestoCabecera item in presupuestoView.Articulo)
            {
                var detalles = new presupuesto_detalle()
                {
                    id_presupuesto_cabecera = ultimoIdPresupuesto,
                    id_articulo = item.id_articulo,
                    cantidad = item.cantidad,
                    precio_unitario = item.precio_venta
                };
                db.presupuesto_detalle.Add(detalles);
            }
            db.SaveChanges();
            presupuestoView = Session["PresupuestoCabeceraView"] as PresupuestoCabeceraView;
            var list = db.cliente.ToList();
            ViewBag.id_cliente = new SelectList(list, "id_cliente", "nombre_razon_social");
            TempData["notice"] = "Registrado exitosamente";
            //return View(presupuestoView);
            return RedirectToAction("Index", "Presupuesto");
        }

        public ActionResult AddArticulo()
        {
            var listaArticulo = db.articulo.ToList();
            ViewBag.id_articulo = new SelectList(listaArticulo, "id_articulo", "descripcion");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddArticulo(ArticuloPresupuestoCabecera articuloPresupuesto)
        {
            var presupuestoView = Session["PresupuestoCabeceraView"] as PresupuestoCabeceraView;
            var idArticulos = int.Parse(Request["id_articulo"]);
            var Articulos = db.articulo.Find(idArticulos);
            articuloPresupuesto = new ArticuloPresupuestoCabecera()
            {
                id_articulo = Articulos.id_articulo,
                descripcion = Articulos.descripcion,
                precio_venta = Articulos.precio_venta,
                cantidad = int.Parse(Request["cantidad"])
            };
            presupuestoView.Articulo.Add(articuloPresupuesto);
            var list = db.cliente.ToList();
            ViewBag.id_cliente = new SelectList(list, "id_cliente", "nombre_razon_social");
            var listaArticulo = db.articulo.ToList();
            ViewBag.id_articulo = new SelectList(listaArticulo, "id_articulo", "descripcion");
            return View("Index",presupuestoView);
        }
        
        // GET: /Delete/5
        public ActionResult Delete()
        {
            var presupuestoView = Session["PresupuestoCabeceraView"] as PresupuestoCabeceraView;
            int index = 0;
            var item = presupuestoView.Articulo.ToArray()[index];
            presupuestoView.Articulo.Remove(item);
            var list = db.cliente.ToList();
            ViewBag.id_cliente = new SelectList(list, "id_cliente", "nombre_razon_social");
            return View("Index", presupuestoView);
        }
    }
}