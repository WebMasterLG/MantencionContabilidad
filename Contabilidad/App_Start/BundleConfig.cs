using System.Web;
using System.Web.Optimization;

namespace Contabilidad
{
    public class BundleConfig
    {
        // Para obtener más información sobre las uniones, visite https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Utilice la versión de desarrollo de Modernizr para desarrollar y obtener información. De este modo, estará
            // para la producción, use la herramienta de compilación disponible en https://modernizr.com para seleccionar solo las pruebas que necesite.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/popper.2017.min.js",
                       "~/Scripts/bootstrap.min.js",
                      "~/Scripts/js_etick-cot.js",
                      "~/Scripts/jquery-datapicker.js",
                      "~/Scripts/jquery.dataTables.js",
                     "~/Scripts/kit.fontawesome.a076d05399.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.min.css",
                      "~/Content/jquery-datapicker.css",
                      "~/Content/jquery.dataTables.min.css",
                      "~/Content/responsive.dataTables.min.css",
                      "~/Content/style_etick-cot.css"));
            bundles.Add(new StyleBundle("~/bundles/modalcss").Include(
                      "~/Content/bootstrap.v4.1.3.min.css",
                      "~/Content/style_etick-cot.css",
                      "~/Content/responsive_etick-cot.css"));
            bundles.Add(new ScriptBundle("~/bundles/modaljs").Include(
                        "~/Scripts/bootstrap.js",
                        "~/Scripts/js_etick-cot.js",
                        "~/Scripts/kit.fontawesome.a076d05399.js"
                        ));
        }
    }
}
