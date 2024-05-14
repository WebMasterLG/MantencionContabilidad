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
    public class HomeController : Controller
    {
        string test = ConfigurationManager.AppSettings.Get("test");
        public ActionResult Index()
        {
            if (test != "True" && (Request.Cookies["cookiePerfil"] == null || Request.Cookies["cookiePerfil"]["usuario"] == null)) return Content("Debe autenticarse en Corretaje.");
            return View();
        }

    }
}