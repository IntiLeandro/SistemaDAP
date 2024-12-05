using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web.Mvc;
using iTextSharp.text;
using iTextSharp.text.pdf;
using SistemaDAP.Models;
using SistemaDAP.App_Start;

namespace SistemaDAP.Controllers
{
    [SistemaDapAuth]
    public class presupuesto_cabeceraController : Controller
    {
        private DBDAPEntities db = new DBDAPEntities();
        List<presupuesto_cabecera> listPresupuesto= null;
        // GET: presupuesto_cabecera
        public ActionResult Index()
        {
            if (listPresupuesto == null)
            {
                listPresupuesto = (from ip in db.presupuesto_cabecera
                                   select ip).ToList<presupuesto_cabecera>();
                //var Presupuesto = (from ip in db.presupuesto_cabecera
                //                   join cli in db.cliente on ip.id_cliente equals cli.id_cliente
                //                   select new
                //                   {
                //                       id_presupuesto_cabecera = ip.id_presupuesto_cabecera,
                //                       fecha_presupuesto = ip.fecha_presupuesto,
                //                       id_cliente = cli.id_cliente,
                //                       nombre_razon_social = cli.nombre_razon_social
                //                   }).ToList();
            }
            return View(listPresupuesto);
        }

        public ActionResult DetallePresupuesto(int idPresupuestoCabecera)
        {
            List<presupuesto_detalle> listaDetallesPresupuesto = (from it in db.presupuesto_detalle
                                                              where it.id_presupuesto_cabecera == idPresupuestoCabecera
                                                              select it).ToList<presupuesto_detalle>();

            return View(listaDetallesPresupuesto);
        }

        public ActionResult OrdenTrabajo(int idPresupuestoCabecera)
        {
            List<orden_trabajo_cabecera> listaOrdenTrabajo = (from it in db.orden_trabajo_cabecera
                                                                  where it.id_presupuesto_cabecera == idPresupuestoCabecera
                                                                  select it).ToList<orden_trabajo_cabecera>();

            return View(listaOrdenTrabajo);
        }

        public ActionResult DetalleOrdenTrabajo(int idOrdenTrabajo)
        {
            List<orden_trabajo_detalle> listaDetallesOrdenTrabajo = (from it in db.orden_trabajo_detalle
                                                                  where it.id_orden_trabajo == idOrdenTrabajo
                                                                  select it).ToList<orden_trabajo_detalle>();

            return View(listaDetallesOrdenTrabajo);
        }


        public ActionResult ConsultarPresupuesto(int? pagina, int idCliente = 0, string fechaPresupuesto = "")
        {
            DateTime fecha;
            //int tamañoPagina = 2;
            int numeroPagina = pagina ?? 1;
            //var presupuesto = new Object();

            if (!(idCliente == 0) && (fechaPresupuesto == ""))
            {
                listPresupuesto = db.presupuesto_cabecera
                .Where(c => c.id_cliente == idCliente)
                .OrderBy(c => c.fecha_presupuesto).ToList();
                //.ToPagedList(numeroPagina, tamañoPagina);
            }
            else if ((idCliente == 0) && !(fechaPresupuesto == ""))
            {
                if (IsDate(fechaPresupuesto))
                {
                    fecha = Convert.ToDateTime(fechaPresupuesto);
                    listPresupuesto = db.presupuesto_cabecera
                        .Where(c => c.fecha_presupuesto == fecha)
                            .OrderBy(c => c.fecha_presupuesto).ToList();
                }
                else
                {
                    TempData["notice"] = "Fecha invalida";
                    listPresupuesto = db.presupuesto_cabecera.OrderBy(p => p.fecha_presupuesto).ToList();//.ToPagedList(numeroPagina, tamañoPagina);
                }
                    
            }
            else if (!(idCliente == 0) && !(fechaPresupuesto == ""))
            {
                if (IsDate(fechaPresupuesto))
                {
                    fecha = Convert.ToDateTime(fechaPresupuesto);
                    listPresupuesto = db.presupuesto_cabecera
                            .Where(c => c.id_cliente == idCliente && c.fecha_presupuesto == fecha)
                            .OrderBy(c => c.fecha_presupuesto).ToList();
                }
                else
                {
                    TempData["notice"] = "Fecha invalida";
                    listPresupuesto = db.presupuesto_cabecera.OrderBy(p => p.fecha_presupuesto).ToList();//.ToPagedList(numeroPagina, tamañoPagina);
                }
            }
            else
            {
                listPresupuesto = db.presupuesto_cabecera.OrderBy(p => p.fecha_presupuesto).ToList();//.ToPagedList(numeroPagina, tamañoPagina);
            }
            

            //if (!(idCliente == 0) && !(fechaPresupuesto == ""))
            //{
            //    DateTime fecha = Convert.ToDateTime(fechaPresupuesto);

            //    listPresupuesto = db.presupuesto_cabecera
            //        .Where(c => c.id_cliente == idCliente && c.fecha_presupuesto == fecha)
            //        .OrderBy(c => c.fecha_presupuesto).ToList();
            //        //.ToPagedList(numeroPagina, tamañoPagina);
            //}
            //else
            //{
                //listPresupuesto = db.presupuesto_cabecera.OrderBy(p => p.fecha_presupuesto).ToList();//.ToPagedList(numeroPagina, tamañoPagina);
            //}

            return View("Index", listPresupuesto);
        }

        public bool IsDate(object inValue)
        {
            bool bValid;
            try
            {
                DateTime myDT = DateTime.Parse(inValue.ToString());
                bValid = true;
            }
            catch (Exception e)
            {
                bValid = false;
            }

            return bValid;
        }

        [HttpPost]
        public FileResult CreatePdf(List<presupuesto_cabecera> listadoPresupuesto)
        {
            listPresupuesto = listadoPresupuesto;
            MemoryStream workStream = new MemoryStream();
            StringBuilder status = new StringBuilder("");
            DateTime dTime = DateTime.Now;
            //file name to be created   
            string strPDFFileName = string.Format("SamplePdf" + dTime.ToString("yyyyMMdd") + "-" + ".pdf");
            Document doc = new Document();
            doc.SetMargins(0f, 0f, 0f, 0f);
            //Create PDF Table with 5 columns  
            PdfPTable tableLayout = new PdfPTable(3);
            doc.SetMargins(0f, 0f, 0f, 0f);
            //Create PDF Table  

            //file will created in this path  
            string strAttachment = Server.MapPath("~/Downloadss/" + strPDFFileName);


            PdfWriter.GetInstance(doc, workStream).CloseStream = false;
            doc.Open();

            //Add Content to PDF   
            doc.Add(Add_Content_To_PDF(tableLayout));

            // Closing the document  
            doc.Close();

            byte[] byteInfo = workStream.ToArray();
            workStream.Write(byteInfo, 0, byteInfo.Length);
            workStream.Position = 0;


            return File(workStream, "application/pdf", strPDFFileName);

        }

        protected PdfPTable Add_Content_To_PDF(PdfPTable tableLayout)
        {

            float[] headers = { 50, 50, 50 }; //Header Widths  
            tableLayout.SetWidths(headers); //Set the pdf headers  
            tableLayout.WidthPercentage = 100; //Set the PDF File witdh percentage  
            tableLayout.HeaderRows = 1;
            //Add Title to the PDF file at the top  
            
            tableLayout.AddCell(new PdfPCell(new Phrase("Listado de Presupuestos", new Font(Font.FontFamily.HELVETICA, 8, 1, new iTextSharp.text.BaseColor(0, 0, 0))))
            {
                Colspan = 12,
                Border = 0,
                PaddingBottom = 5,
                HorizontalAlignment = Element.ALIGN_CENTER
            });


            ////Add header  
            AddCellToHeader(tableLayout, "Nro. Presupuesto");
            AddCellToHeader(tableLayout, "Fecha Presupuesto");
            AddCellToHeader(tableLayout, "Código Cliente");

            ////Add body  

            foreach (var emp in listPresupuesto)
            {
                AddCellToBody(tableLayout, emp.id_presupuesto_cabecera.ToString());
                AddCellToBody(tableLayout, emp.fecha_presupuesto.ToShortDateString());
                AddCellToBody(tableLayout, emp.id_cliente.ToString());
            }

            return tableLayout;
        }
        // Method to add single cell to the Header  
        private static void AddCellToHeader(PdfPTable tableLayout, string cellText)
        {

            tableLayout.AddCell(new PdfPCell(new Phrase(cellText, new Font(Font.FontFamily.HELVETICA, 8, 1, iTextSharp.text.BaseColor.YELLOW)))
            {
                HorizontalAlignment = Element.ALIGN_LEFT,
                Padding = 5,
                BackgroundColor = new iTextSharp.text.BaseColor(128, 0, 0)
            });
        }

        // Method to add single cell to the body  
        private static void AddCellToBody(PdfPTable tableLayout, string cellText)
        {
            tableLayout.AddCell(new PdfPCell(new Phrase(cellText, new Font(Font.FontFamily.HELVETICA, 8, 1, iTextSharp.text.BaseColor.BLACK)))
            {
                HorizontalAlignment = Element.ALIGN_LEFT,
                Padding = 5,
                BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255)
            });
        }


        // GET: presupuesto_cabecera/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            presupuesto_cabecera presupuesto_cabecera = db.presupuesto_cabecera.Find(id);
            if (presupuesto_cabecera == null)
            {
                return HttpNotFound();
            }
            return View(presupuesto_cabecera);
        }

        // GET: orden_trabajo_cabecera/Create
        public ActionResult Create(List<presupuesto_cabecera> listadoPresupuesto)
        {
            ViewBag.id_cliente = new SelectList(db.cliente, "id_cliente", "ci_ruc");
            ViewBag.id_presupuesto_cabecera = new SelectList(db.presupuesto_cabecera, "id_presupuesto_cabecera", "id_presupuesto_cabecera");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_orden_trabajo,fecha_orden_trabajo,id_cliente,id_presupuesto_cabecera")] orden_trabajo_cabecera orden_trabajo_cabecera)
        {
            if (ModelState.IsValid)
            {
                db.orden_trabajo_cabecera.Add(orden_trabajo_cabecera);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            //ViewBag.id_cliente = new SelectList(db.cliente, "id_cliente", "ci_ruc", orden_trabajo_cabecera.id_cliente);
            ViewBag.id_presupuesto_cabecera = new SelectList(db.presupuesto_cabecera, "id_presupuesto_cabecera", "id_presupuesto_cabecera", orden_trabajo_cabecera.id_presupuesto_cabecera);
            return View(orden_trabajo_cabecera);
        }

        // GET: orden_trabajo_detalle/Create
        public ActionResult CreateOrdenTrabajoDetalle()
        {
            ViewBag.id_articulo = new SelectList(db.articulo, "id_articulo", "descripcion");
            ViewBag.id_orden_trabajo = new SelectList(db.orden_trabajo_cabecera, "id_orden_trabajo", "id_orden_trabajo");
            return View();
        }

        // POST: orden_trabajo_detalle/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateOrdenTrabajoDetalle([Bind(Include = "id_orden_trabajo_detalle,id_orden_trabajo,id_articulo,cantidad,descripcion_trabajo")] orden_trabajo_detalle orden_trabajo_detalle)
        {
            if (ModelState.IsValid)
            {
                db.orden_trabajo_detalle.Add(orden_trabajo_detalle);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_articulo = new SelectList(db.articulo, "id_articulo", "descripcion", orden_trabajo_detalle.id_articulo);
            ViewBag.id_orden_trabajo = new SelectList(db.orden_trabajo_cabecera, "id_orden_trabajo", "id_orden_trabajo", orden_trabajo_detalle.id_orden_trabajo);
            return View(orden_trabajo_detalle);
        }


        // GET: presupuesto_cabecera/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            presupuesto_cabecera presupuesto_cabecera = db.presupuesto_cabecera.Find(id);
            if (presupuesto_cabecera == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_cliente = new SelectList(db.cliente, "id_cliente", "ci_ruc", presupuesto_cabecera.id_cliente);
            return View(presupuesto_cabecera);
        }

        // POST: presupuesto_cabecera/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_presupuesto_cabecera,fecha_presupuesto,id_cliente")] presupuesto_cabecera presupuesto_cabecera)
        {
            if (ModelState.IsValid)
            {
                db.Entry(presupuesto_cabecera).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_cliente = new SelectList(db.cliente, "id_cliente", "ci_ruc", presupuesto_cabecera.id_cliente);
            return View(presupuesto_cabecera);
        }

        // GET: presupuesto_cabecera/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            presupuesto_cabecera presupuesto_cabecera = db.presupuesto_cabecera.Find(id);
            if (presupuesto_cabecera == null)
            {
                return HttpNotFound();
            }
            return View(presupuesto_cabecera);
        }

        // POST: presupuesto_cabecera/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            presupuesto_cabecera presupuesto_cabecera = db.presupuesto_cabecera.Find(id);
            db.presupuesto_cabecera.Remove(presupuesto_cabecera);
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
