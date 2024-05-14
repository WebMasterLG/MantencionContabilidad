using Contabilidad.Clases;
using Contabilidad.Models;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Contabilidad.Controllers
{
    public class FacturacionController : Controller
    {
        // GET: Facturacion

        string test = ConfigurationManager.AppSettings.Get("test");
        string urlNuevaCotizacion = ConfigurationManager.AppSettings.Get("urlNuevaCotizacion");
        string urlSolicitudes = ConfigurationManager.AppSettings.Get("urlSolicitudes");
        public ActionResult Index()
        {
            if (test != "True" && (Request.Cookies["cookiePerfil"] == null || Request.Cookies["cookiePerfil"]["usuario"] == null)) return Content("Debe autenticarse en Corretaje.");
            return PartialView();
        }

        public ActionResult _GrillaFacturacion(string inicio, string termino, byte idEstado)
        {
            if (test != "True" && (Request.Cookies["cookiePerfil"] == null || Request.Cookies["cookiePerfil"]["usuario"] == null)) return Content("Debe autenticarse en Corretaje.");

            int idUsuario = 10088;
            int perfil = 1;
            if (test != "True")
            {
                idUsuario = int.Parse(Request.Cookies["cookiePerfil"]["usuario"].ToString());
                perfil = int.Parse(Request.Cookies["cookiePerfil"]["perfil"].ToString());
            }

            List<Factura> oListaFacturas = new List<Factura>();
            try
            {
                oListaFacturas = FacturacionModel.getFacturacion(idUsuario, inicio, termino, idEstado);

            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = "Error:" + ex.Message;
            }

            ViewBag.perfil = perfil;
            ViewBag.idUsuario = idUsuario;
            ViewBag.urlNuevaCotizacion = urlNuevaCotizacion;
            ViewBag.urlSolicitudes = urlSolicitudes;
            return PartialView("_GrillaFacturacion", oListaFacturas);
        }

        public string CambiarEstado(string IdsActualizar, int idNuevoEstado)
        {
            if (test != "True" && (Request.Cookies["cookiePerfil"] == null || Request.Cookies["cookiePerfil"]["usuario"] == null)) return "Debe autenticarse en Corretaje.";

            int idUsuario = 10088;
            string mensaje = "";
            if (test != "True")
            {
                idUsuario = int.Parse(Request.Cookies["cookiePerfil"]["usuario"].ToString());
            }

            try
            {
                mensaje = FacturacionModel.CambiarEstado(idUsuario, IdsActualizar, idNuevoEstado);
            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = "Error:" + ex.Message;
            }
            return mensaje;
        }


        public JsonResult FormatosExcel(string grilla)
        {
            int idUsuario = 10088;
            if (test != "True")
            {
                idUsuario = int.Parse(Request.Cookies["cookiePerfil"]["usuario"].ToString());
            }
            //int usuario = 1;
            //int usuario = int.Parse(Request.Cookies["cookiePerfil"]["usuario"].ToString());
            return Json(new { resp = FacturacionModel.Llama_GeneraExcelHeader(idUsuario,grilla) }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ExportarExcel(FormCollection f)
        {
            if (test != "True" && (Request.Cookies["cookiePerfil"] == null || Request.Cookies["cookiePerfil"]["usuario"] == null)) return Content("Debe autenticarse en Corretaje.");

            int idUsuario = 10088;
            if (test != "True")
            {
                idUsuario = int.Parse(Request.Cookies["cookiePerfil"]["usuario"].ToString());
            }

            int idFormato = int.Parse(f["idFormato"].ToString());
            string grilla = f["grilla"].ToString();
            string inicio = f["inicio"].ToString();
            string termino = f["termino"].ToString();
            string fechaContable = f["fechaContable"].ToString();
            int id_estado_facturacion = int.Parse(f["id_estado_facturacion"].ToString()); // en el caso de Pago proveedores, corresponde al estado de pago
            string idsSeleccion = f["ids_seleccion"].ToString();
            int idProveedor = int.Parse(f["id_proveedor"].ToString());


            GridView gv = new GridView();

            gv.DataSource = FacturacionModel.Llama_GeneraExcel(idUsuario, idFormato, grilla, inicio, termino, id_estado_facturacion, fechaContable, idsSeleccion, idProveedor);

            gv.DataBind();

            string nomArchSalida = "Consulta_" + inicio + "_al_" + termino + ".xls";
            switch (idFormato)
            {
                case 1:
                    nomArchSalida = "Interfaz_NUBOX_" + inicio + "_al_" + termino + ".xls";
                    break;
                case 2:
                    nomArchSalida = "Interfaz_Banco_Pagos_" + inicio + "_al_" + termino + ".xls";
                    break;
                case 3:
                    nomArchSalida = "Interfaz_Contable_Pagos_" + inicio + "_al_" + termino + ".xls";
                    break;
                case 4:
                    nomArchSalida = "Interfaz_NUBOX_" + inicio + "_al_" + termino + "_Con_Seleccion.xls";
                    break;
                case 90:
                    nomArchSalida = "Consulta_Facturacion_" + inicio + "_al_" + termino + ".xls";
                    break;
                case 91:
                    nomArchSalida = "Consulta_Pagos_" + inicio + "_al_" + termino + ".xls";
                    break;

            }


            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment; filename=" + nomArchSalida);
            Response.ContentType = "application/vnd.ms-excel";

            Response.Charset = "";
            StringWriter objStringWriter = new StringWriter();
            HtmlTextWriter objHtmlTextWriter = new HtmlTextWriter(objStringWriter);

            gv.RenderControl(objHtmlTextWriter);

            Response.Output.Write(objStringWriter.ToString());
            Response.Flush();
            Response.End();

            return null;
        }


        [HttpPost]
        public ActionResult _FormularioCargaArchivo()
        {
            if (test != "True" && (Request.Cookies["cookiePerfil"] == null || Request.Cookies["cookiePerfil"]["usuario"] == null)) return Content("Debe autenticarse en Corretaje.");
            return PartialView();
        }

        [HttpPost]
        public ActionResult _ResultadoCargarArchivos(HttpPostedFileBase archivo)
        {
            if (test != "True" && (Request.Cookies["cookiePerfil"] == null || Request.Cookies["cookiePerfil"]["usuario"] == null)) return Content("Debe autenticarse en Corretaje.");

            List<ListadoCargaFacturas> lst = new List<ListadoCargaFacturas>();

            string rutaSubirFactura = ConfigurationManager.AppSettings.Get("rutaSubirFactura");

            if (!System.IO.Directory.Exists(rutaSubirFactura)) { System.IO.Directory.CreateDirectory(rutaSubirFactura); }

            if (archivo != null && archivo.ContentLength > 0)
            {
                var path = "";
                var nombreArchivo = System.IO.Path.GetFileName(archivo.FileName);
                nombreArchivo = DateTime.Now.ToString("yyyyMMddHHmmss") + "_" + nombreArchivo.Replace(' ', '_');
                path = System.IO.Path.Combine(rutaSubirFactura, nombreArchivo);
                archivo.SaveAs(path);
                List<FacturaCotizacion> listaFacturas = new List<FacturaCotizacion>();
                listaFacturas = LeerPDF(path, rutaSubirFactura);
                foreach (var factura in listaFacturas)
                {
                    lst.Add(new ListadoCargaFacturas
                    {
                        idCotizacion = factura.idCotizacion,
                        id_factura = factura.folioFactura,
                        nombre_cliente = factura.nombreCliente,
                        g_archivo_factura = factura.nombreArchivo,
                        neto = factura.neto.ToString("N0"),
                        iva = factura.iva.ToString("N0"),
                        total = factura.total.ToString("N0")
                    });
                }

                if (System.IO.File.Exists(path))
                    System.IO.File.Delete(path);

                ViewBag.urlSolicitudes = urlSolicitudes;
                return PartialView(lst);
            }
            else
            {
                return PartialView(lst);
            }
        }

        public List<FacturaCotizacion> LeerPDF(object Filename, string directorio_salida)
        {
            int IdUsuario = 10088;
            if (test != "True")
            {
                IdUsuario = int.Parse(Request.Cookies["cookiePerfil"]["usuario"].ToString());
            }
            List<FacturaCotizacion> lst = new List<FacturaCotizacion>();

            PdfReader reader2 = new PdfReader((string)Filename);

            for (int page = 1; page <= reader2.NumberOfPages; page++)
            {
                ITextExtractionStrategy its = new iTextSharp.text.pdf.parser.SimpleTextExtractionStrategy();
                PdfReader reader = new PdfReader((string)Filename);
                String s = PdfTextExtractor.GetTextFromPage(reader, page, its);

                s = Encoding.UTF8.GetString(ASCIIEncoding.Convert(Encoding.Default, Encoding.UTF8, Encoding.Default.GetBytes(s)));
                System.Diagnostics.Debug.WriteLine(s);
                double esNumero = 0;
                int idCotizacion;

                if (Double.TryParse(TreaCotizacion(s), out esNumero))
                {
                    idCotizacion = int.Parse(TreaCotizacion(s));
                }
                else
                {
                    idCotizacion = 0;
                }

                
                int folioFactura = Int32.Parse(TreaFactura(s));

                string RUT = TraeRut(s);
                string Periodo = TraeFecha(s);
                int neto = int.Parse(MontoNeto(s));
                int iva = int.Parse(MontoIva(s));
                int total = int.Parse(MontoTotal(s));

                string[] Valores_RUT = RUT.Split('-');

                //string NombreArchivo = DateTime.Now.ToString("yyyyMMddHHmmss") + "_" + Folio + "_" + Periodo + "_" + RUT + ".pdf";
                string NombreArchivo = "Cotizacion_" + idCotizacion + "_Factura_" + folioFactura + ".pdf";
                string RutaSalida = System.IO.Path.Combine(directorio_salida, NombreArchivo);
                ExtractPages((string)Filename, RutaSalida, page, page);

                FacturaCotizacion oFactura = FacturacionModel.updateFacturaCotizacion(IdUsuario, idCotizacion, folioFactura, NombreArchivo);

                oFactura.idCotizacion = idCotizacion;
                oFactura.folioFactura = folioFactura;
                oFactura.nombreArchivo = NombreArchivo;
                oFactura.neto = neto;
                oFactura.iva = iva;
                oFactura.total = total;
                lst.Add(oFactura);

                reader.Close();
            }
            reader2.Close();

            return lst;
        }

        private void ExtractPages(string sourcePDFpath, string outputPDFpath, int startpage, int endpage)
        {

            PdfReader reader = null;
            Document sourceDocument = null;
            PdfCopy pdfCopyProvider = null;
            PdfImportedPage importedPage = null;

            reader = new PdfReader(sourcePDFpath);
            sourceDocument = new Document(reader.GetPageSizeWithRotation(startpage));
            pdfCopyProvider = new PdfCopy(sourceDocument, new System.IO.FileStream(outputPDFpath, System.IO.FileMode.Create));

            sourceDocument.Open();

            for (int i = startpage; i <= endpage; i++)
            {
                importedPage = pdfCopyProvider.GetImportedPage(reader, i);
                pdfCopyProvider.AddPage(importedPage);
            }
            sourceDocument.Close();
            reader.Close();
        }

        private string TreaCotizacion(string texto)
        {
            string resultado = null;
            //string PatternCotizacion = "Cotización N°([0-9]{1,})";
            string PatternCotizacion = "N°([0-9]{1,})";
            Regex BCot = new Regex(PatternCotizacion, RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.Compiled);
            MatchCollection Esta = BCot.Matches(texto);
            foreach (Match v in Esta)
            {
                resultado = v.Groups[1].ToString();
                break;
            }
            if (resultado == null)
            {
                resultado = "SinCotizacion";
            }
            return resultado;
        }

        private string TraeRut(string texto)
        {
            int x = 0;
            string resultado = null;
            string PatternRUT = "[0-9]{1,2}\\.[0-9]{3}\\.[0-9]{3}\\-[0-9|K]{1}";
            Regex BRut = new Regex(PatternRUT, RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.Compiled);
            MatchCollection Esta = BRut.Matches(texto);
            foreach (Match v in Esta)
            {
                if (x == 1)
                {
                    resultado = v.Groups[0].ToString().Replace(".", "");
                }
                x++;
            }
            return resultado;
        }

        private string TraeFecha(string texto)
        {
            string resultado = null;
            string PatternRUT = "(([0-9]{1,2}).de.([a-zA-Z]{1,}).de.([0-9]{1,}))";
            Regex BRut = new Regex(PatternRUT, RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.Compiled);
            MatchCollection Esta = BRut.Matches(texto);
            foreach (Match v in Esta)
            {
                resultado = v.Groups[4].ToString() + "-" + NombreMesANumero(v.Groups[3].ToString()) + "-" + v.Groups[2].ToString();
                break;
            }
            return resultado;
        }

        private string TreaFactura(string texto)
        {
            string resultado = null;
            string PatternRUT = "N[º°].([0-9]{1,})";
            Regex BRut = new Regex(PatternRUT, RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.Compiled);
            MatchCollection Esta = BRut.Matches(texto);
            foreach (Match v in Esta)
            {
                resultado = v.Groups[1].ToString();
                break;
            }
            return resultado;
        }

        private string NombreMesANumero(string mes)
        {
            string NombreMes = mes.ToLower();
            string NumeroMes = "";
            switch (NombreMes)
            {
                case "enero":
                    NumeroMes = "01";
                    break;
                case "febrero":
                    NumeroMes = "02";
                    break;
                case "marzo":
                    NumeroMes = "03";
                    break;
                case "abril":
                    NumeroMes = "04";
                    break;
                case "mayo":
                    NumeroMes = "05";
                    break;
                case "junio":
                    NumeroMes = "06";
                    break;
                case "julio":
                    NumeroMes = "07";
                    break;
                case "agosto":
                    NumeroMes = "08";
                    break;
                case "septiembre":
                    NumeroMes = "09";
                    break;
                case "octubre":
                    NumeroMes = "10";
                    break;
                case "noviembre":
                    NumeroMes = "11";
                    break;
                case "diciembre":
                    NumeroMes = "12";
                    break;
            }
            return NumeroMes;
        }

        private string MontoNeto(string texto)
        {
            string resultado = null;
            string PatternRUT = "Monto.Neto.([0-9]{1,}\\.{0,}[0-9]{1,}\\.{0,}[0-9]{0,}\\.{0,}[0-9]{1,}\\.{0,})";
            Regex BRut = new Regex(PatternRUT, RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.Compiled);
            MatchCollection Esta = BRut.Matches(texto);
            foreach (Match v in Esta)
            {
                resultado = v.Groups[1].ToString().Replace(".", "");
            }
            return resultado;
        }

        private string MontoIva(string texto)
        {
            string resultado = null;
            string PatternRUT = "IVA.([0-9]{1,}\\.{0,}[0-9]{1,}\\.{0,}[0-9]{0,}\\.{0,}[0-9]{1,}\\.{0,})";
            Regex BRut = new Regex(PatternRUT, RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.Compiled);
            MatchCollection Esta = BRut.Matches(texto);
            foreach (Match v in Esta)
            {
                resultado = v.Groups[1].ToString().Replace(".", "");
            }
            return resultado;
        }

        private string MontoTotal(string texto)
        {
            string resultado = null;
            string PatternRUT = "Total.([0-9]{1,}\\.{0,}[0-9]{1,}\\.{0,}[0-9]{0,}\\.{0,}[0-9]{1,}\\.{0,})";
            Regex BRut = new Regex(PatternRUT, RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.Compiled);
            MatchCollection Esta = BRut.Matches(texto);
            foreach (Match v in Esta)
            {
                resultado = v.Groups[1].ToString().Replace(".", "");
            }
            return resultado;
        }


        [HttpPost]
        public string SubirDocumento(int idCotizacion, int numFactura, HttpPostedFileBase archivo)
        {
            int idUsu = 10088;
            if (test != "True") idUsu = int.Parse(Request.Cookies["cookiePerfil"]["usuario"].ToString());

            string nombreArchivo = "";
            string rutaSubirFactura = ConfigurationManager.AppSettings.Get("rutaSubirFactura");

            if (!System.IO.Directory.Exists(rutaSubirFactura)) { System.IO.Directory.CreateDirectory(rutaSubirFactura); }

            if (archivo != null && archivo.ContentLength > 0)
            {
                var nombreUpload = System.IO.Path.GetFileName(archivo.FileName);
                var arrNomArch = nombreUpload.Split('.');
                var extension = arrNomArch[arrNomArch.Length - 1];

                string NombreArchivo = "Cotizacion_" + idCotizacion.ToString() + "_Factura_" + numFactura.ToString() + "." + extension;
                var path = System.IO.Path.Combine(rutaSubirFactura, NombreArchivo);
                
                FacturaCotizacion oFactura = FacturacionModel.updateFacturaCotizacion(idUsu, idCotizacion, numFactura, NombreArchivo);
                
                archivo.SaveAs(path);

            }
            return nombreArchivo;

        }

        //[HttpPost]
        //public bool EliminarDocumentoCompra(int idCompra)
        //{
        //    int idUsu = 10088;
        //    if (test != "True") idUsu = int.Parse(Request.Cookies["cookiePerfil"]["usuario"].ToString());

        //    return FacturacionModel.EliminarDocumentoCompra(idUsu, idCompra);

        //}


    }
}