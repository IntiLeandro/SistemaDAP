using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using iTextSharp.text;
using iTextSharp.text.pdf;
using SistemaDAP.Models;

namespace SistemaDAP.Controllers
{
    public class orden_trabajo_cabecera1Controller : Controller
    {
        private DBDAPEntities db = new DBDAPEntities();
        List<orden_trabajo_cabecera> ListadoOrdenTrabajo = null;
        // GET: orden_trabajo_cabecera1
        public ActionResult Index()
        {
            //var orden_trabajo_cabecera = db.orden_trabajo_cabecera.Include(o => o.presupuesto_cabecera);
            //return View(orden_trabajo_cabecera.ToList());
            if (ListadoOrdenTrabajo == null)
            {
                ListadoOrdenTrabajo = (from ip in db.orden_trabajo_cabecera
                                   select ip).ToList<orden_trabajo_cabecera>();
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
            return View(ListadoOrdenTrabajo);
        }

        public ActionResult ConsultarOrdenTrabajo(int? pagina, int idPresupuesto = 0, string fechaOrdenTrabajo = "")
        {
            DateTime fecha;
            //int tamañoPagina = 2;
            int numeroPagina = pagina ?? 1;
            //var presupuesto = new Object();

            if (!(idPresupuesto == 0) && (fechaOrdenTrabajo == ""))
            {
                ListadoOrdenTrabajo = db.orden_trabajo_cabecera
                .Where(c => c.id_presupuesto_cabecera == idPresupuesto)
                .OrderBy(c => c.fecha_orden_trabajo).ToList();
                //.ToPagedList(numeroPagina, tamañoPagina);
            }
            else if ((idPresupuesto == 0) && !(fechaOrdenTrabajo == ""))
            {
                if (IsDate(fechaOrdenTrabajo))
                {
                    fecha = Convert.ToDateTime(fechaOrdenTrabajo);
                    ListadoOrdenTrabajo = db.orden_trabajo_cabecera
                        .Where(c => c.fecha_orden_trabajo == fecha)
                            .OrderBy(c => c.fecha_orden_trabajo).ToList();
                }
                else
                {
                    TempData["notice"] = "Fecha invalida";
                    ListadoOrdenTrabajo = db.orden_trabajo_cabecera.OrderBy(p => p.fecha_orden_trabajo).ToList();//.ToPagedList(numeroPagina, tamañoPagina);
                }

            }
            else if (!(idPresupuesto == 0) && !(fechaOrdenTrabajo == ""))
            {
                if (IsDate(fechaOrdenTrabajo))
                {
                    fecha = Convert.ToDateTime(fechaOrdenTrabajo);
                    ListadoOrdenTrabajo = db.orden_trabajo_cabecera
                            .Where(c => c.id_presupuesto_cabecera == idPresupuesto && c.fecha_orden_trabajo == fecha)
                            .OrderBy(c => c.fecha_orden_trabajo).ToList();
                }
                else
                {
                    TempData["notice"] = "Fecha invalida";
                    ListadoOrdenTrabajo = db.orden_trabajo_cabecera.OrderBy(p => p.fecha_orden_trabajo).ToList();//.ToPagedList(numeroPagina, tamañoPagina);
                }
            }
            else
            {
                ListadoOrdenTrabajo = db.orden_trabajo_cabecera.OrderBy(p => p.fecha_orden_trabajo).ToList();//.ToPagedList(numeroPagina, tamañoPagina);
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

            return View("Index", ListadoOrdenTrabajo);
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

        public ActionResult DetalleOrdenTrabajo(int idOrdenTrabajo)
        {
            List<orden_trabajo_detalle> listaDetallesOrdenTrabajo = (from it in db.orden_trabajo_detalle
                                                                     where it.id_orden_trabajo == idOrdenTrabajo
                                                                     select it).ToList<orden_trabajo_detalle>();

            return View(listaDetallesOrdenTrabajo);
        }


        [HttpPost]
        public FileResult CreatePdf(List<orden_trabajo_cabecera> listaOrdenTrabajo)
        {
            ListadoOrdenTrabajo = listaOrdenTrabajo;
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

            tableLayout.AddCell(new PdfPCell(new Phrase("Listado de Orden de Trabajo", new Font(Font.FontFamily.HELVETICA, 8, 1, new iTextSharp.text.BaseColor(0, 0, 0))))
            {
                Colspan = 12,
                Border = 0,
                PaddingBottom = 5,
                HorizontalAlignment = Element.ALIGN_CENTER
            });


            ////Add header  
            AddCellToHeader(tableLayout, "Nro. Orden de Trabajo");
            AddCellToHeader(tableLayout, "Fecha Orden de Trabajo");
            AddCellToHeader(tableLayout, "Descripcion");

            ////Add body  

            foreach (var emp in ListadoOrdenTrabajo)
            {
                AddCellToBody(tableLayout, emp.id_orden_trabajo.ToString());
                AddCellToBody(tableLayout, emp.fecha_orden_trabajo.ToShortDateString());
                AddCellToBody(tableLayout, emp.descripcion_orden_trabajo.ToString());
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



        // GET: orden_trabajo_cabecera1/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            orden_trabajo_cabecera orden_trabajo_cabecera = db.orden_trabajo_cabecera.Find(id);
            if (orden_trabajo_cabecera == null)
            {
                return HttpNotFound();
            }
            return View(orden_trabajo_cabecera);
        }

        // GET: orden_trabajo_cabecera1/Create
        public ActionResult Create()
        {
            ViewBag.id_presupuesto_cabecera = new SelectList(db.presupuesto_cabecera, "id_presupuesto_cabecera", "id_presupuesto_cabecera");
            return View();
        }

        // POST: orden_trabajo_cabecera1/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_orden_trabajo,fecha_orden_trabajo,id_presupuesto_cabecera,descripcion_orden_trabajo")] orden_trabajo_cabecera orden_trabajo_cabecera)
        {
            if (ModelState.IsValid)
            {
                db.orden_trabajo_cabecera.Add(orden_trabajo_cabecera);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_presupuesto_cabecera = new SelectList(db.presupuesto_cabecera, "id_presupuesto_cabecera", "id_presupuesto_cabecera", orden_trabajo_cabecera.id_presupuesto_cabecera);
            return View(orden_trabajo_cabecera);
        }

        // GET: orden_trabajo_cabecera1/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            orden_trabajo_cabecera orden_trabajo_cabecera = db.orden_trabajo_cabecera.Find(id);
            if (orden_trabajo_cabecera == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_presupuesto_cabecera = new SelectList(db.presupuesto_cabecera, "id_presupuesto_cabecera", "id_presupuesto_cabecera", orden_trabajo_cabecera.id_presupuesto_cabecera);
            return View(orden_trabajo_cabecera);
        }

        // POST: orden_trabajo_cabecera1/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_orden_trabajo,fecha_orden_trabajo,id_presupuesto_cabecera,descripcion_orden_trabajo")] orden_trabajo_cabecera orden_trabajo_cabecera)
        {
            if (ModelState.IsValid)
            {
                db.Entry(orden_trabajo_cabecera).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_presupuesto_cabecera = new SelectList(db.presupuesto_cabecera, "id_presupuesto_cabecera", "id_presupuesto_cabecera", orden_trabajo_cabecera.id_presupuesto_cabecera);
            return View(orden_trabajo_cabecera);
        }

        // GET: orden_trabajo_cabecera1/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            orden_trabajo_cabecera orden_trabajo_cabecera = db.orden_trabajo_cabecera.Find(id);
            if (orden_trabajo_cabecera == null)
            {
                return HttpNotFound();
            }
            return View(orden_trabajo_cabecera);
        }

        // POST: orden_trabajo_cabecera1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            orden_trabajo_cabecera orden_trabajo_cabecera = db.orden_trabajo_cabecera.Find(id);
            db.orden_trabajo_cabecera.Remove(orden_trabajo_cabecera);
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
