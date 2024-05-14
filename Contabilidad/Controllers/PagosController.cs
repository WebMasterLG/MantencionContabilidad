using Contabilidad.Clases;
using Contabilidad.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Contabilidad.Controllers
{
    public class PagosController : Controller
    {
        // GET: Pagos
        string test = ConfigurationManager.AppSettings.Get("test");
        string urlCotizaciones = ConfigurationManager.AppSettings.Get("urlCotizaciones");
        string urlMantenedores = ConfigurationManager.AppSettings.Get("urlMantenedores");
        string urlEnEjecucion = ConfigurationManager.AppSettings.Get("urlEnEjecucion");
        string urlSolicitudes = ConfigurationManager.AppSettings.Get("urlSolicitudes");

        public ActionResult Index()
        {
            if (test != "True" && (Request.Cookies["cookiePerfil"] == null || Request.Cookies["cookiePerfil"]["usuario"] == null)) return Content("Debe autenticarse en Corretaje.");
            return PartialView();
        }

        public ActionResult _ListaComboBox()
        {
            return PartialView();
        }
        public ActionResult _GrillaPagos(string inicio, string termino, byte idEstado, int idProveedor = 0, int tipo_proveedor = 0)
        {
            if (test != "True" && (Request.Cookies["cookiePerfil"] == null || Request.Cookies["cookiePerfil"]["usuario"] == null)) return Content("Debe autenticarse en Corretaje.");

            int idUsuario = 10088;
            int perfil = 1;
            int idArea = 3;

            if (test != "True")
            {
                idUsuario = int.Parse(Request.Cookies["cookiePerfil"]["usuario"].ToString());
                perfil = int.Parse(Request.Cookies["cookiePerfil"]["perfil"].ToString());
                idArea = int.Parse(Request.Cookies["cookiePerfil"]["idArea"].ToString());
            }

            List<Pago> oListaPagos = new List<Pago>();
            try
            {
                oListaPagos = PagosModel.getPagosProveedores(idUsuario, inicio, termino, idEstado, idProveedor, tipo_proveedor);

            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = "Error:" + ex.Message;
            }

            ViewBag.perfil = perfil;
            ViewBag.idUsuario = idUsuario;
            ViewBag.idArea = idArea;
            ViewBag.urlCotizaciones = urlCotizaciones;
            ViewBag.urlMantenedores = urlMantenedores;
            ViewBag.urlEnEjecucion = urlEnEjecucion;
            ViewBag.urlSolicitudes = urlSolicitudes;
            return PartialView("_GrillaPagos", oListaPagos);
        }


        public string CambiarEstadoPago(string IdsActualizar, int idNuevoEstado, string fechaNomina)
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
                mensaje = PagosModel.CambiarEstado(idUsuario, IdsActualizar, idNuevoEstado, fechaNomina);

            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = "Error:" + ex.Message;
            }
            return mensaje;
        }


        public ActionResult _BancoCuenta()
        {
            if (test != "True" && (Request.Cookies["cookiePerfil"] == null || Request.Cookies["cookiePerfil"]["usuario"] == null)) return Content("Debe autenticarse en Corretaje.");

            int idUsuario = 10088;
            if (test != "True")
            {
                idUsuario = int.Parse(Request.Cookies["cookiePerfil"]["usuario"].ToString());
            }

            ViewBag.urlCotizaciones = urlCotizaciones;
            ViewBag.urlMantenedores = urlMantenedores;
            return PartialView("_BancoCuenta");
        }


        public ActionResult _ObservacionesPago(int idTipo, int idAsignacion, string numFactura, string nomProveedor)
        {
            int idUsuario = 10088;
            if (test != "True")
                idUsuario = int.Parse(Request.Cookies["cookiePerfil"]["usuario"].ToString());
            PagoObservaciones oPagoObs = new PagoObservaciones();
            try
            {
                oPagoObs = PagosModel.getPagosObservaciones(idUsuario, idTipo, idAsignacion);

            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = "Error:" + ex.Message;
            }
            ViewBag.urlSolicitudes = urlSolicitudes;
            ViewBag.idAsignacion = idAsignacion;
            ViewBag.numFactura = numFactura;
            ViewBag.nomProveedor = nomProveedor;
            return PartialView("_ObservacionesPago", oPagoObs);
        }


        public string _GuardarObservacion(int idTipo, int idAsignacion, string obs)
        {
            int idUsuario = 10088;
            if (test != "True") idUsuario = int.Parse(Request.Cookies["cookiePerfil"]["usuario"].ToString());

            string mensaje = "";
            if (obs.Trim() == "")
            {
                mensaje = "Debe ingresar una observación";
            }
            else
            {
                try
                {
                    mensaje = PagosModel.addObservacion(idUsuario, idTipo, idAsignacion, obs);
                }
                catch (Exception ex)
                {
                    mensaje = "Error:" + ex.Message;
                }
            }
            return mensaje;
        }

        public ActionResult _ListaProveedores()
        {
            int idUsuario = 10088;
            if (test != "True") idUsuario = int.Parse(Request.Cookies["cookiePerfil"]["usuario"].ToString());

            List<Proveedor> oListaProveedores = new List<Proveedor>();
            try
            {
                oListaProveedores = PagosModel.getListaProveedores(idUsuario);

            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = "Error:" + ex.Message;
            }
            return PartialView("_ListaProveedores", oListaProveedores);
        }
    }
}