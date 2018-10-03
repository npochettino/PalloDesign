using System.Web.Optimization;

namespace UI.Web
{
    public class BundleConfig
    {
        // Para obtener más información sobre Bundles, visite http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {

            bundles.Add(new StyleBundle("~/Content/bootstrap").Include(
                "~/Content/bootstrap.cerulean.css"));

            bundles.Add(new ScriptBundle("~/Scripts/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/Scripts/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Utilice la versión de desarrollo de Modernizr para desarrollar y obtener información. De este modo, estará
            // preparado para la producción y podrá utilizar la herramienta de compilación disponible en http://modernizr.com para seleccionar solo las pruebas que necesite.
            bundles.Add(new ScriptBundle("~/Scripts/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/Scripts/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js",
                      "~/Scripts/jquery.validate.min.js",
                      "~/Scripts/jquery.validate.unobtrusive.min.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/reset").Include(
                "~/Content/reset.css"));

            //Added
            bundles.Add(new StyleBundle("~/Content/jqueryui/themes/base/css").Include(
               "~/Content/themes/base/core.css",
               "~/Content/themes/base/resizable.css",
               "~/Content/themes/base/selectable.css",
               "~/Content/themes/base/accordion.css",
               "~/Content/themes/base/autocomplete.css",
               "~/Content/themes/base/button.css",
               "~/Content/themes/base/dialog.css",
               "~/Content/themes/base/slider.css",
               "~/Content/themes/base/tabs.css",
               "~/Content/themes/base/datepicker.css",
               "~/Content/themes/base/progressbar.css",
               "~/Content/themes/base/theme.css",
               "~/Content/themes/base/draggable.css",
               "~/Content/themes/base/menu.css",
               "~/Content/themes/base/selectmenu.css",
               "~/Content/themes/base/sortable.css",
               "~/Content/themes/base/spinner.css",
               "~/Content/themes/base/tooltip.css"));

            bundles.Add(new ScriptBundle("~/Scripts/momentjs").Include(
                    "~/Scripts/moment.js"));

            bundles.Add(new StyleBundle("~/Content/Custom/customcss")
              .IncludeDirectory("~/Content/Custom", "*.css", true));

            bundles.Add(new StyleBundle("~/Content/dt").Include(
                "~/Content/DataTables/css/dataTables.bootstrap.css"));

            bundles.Add(new ScriptBundle("~/Scripts/dt").Include(
                       "~/Scripts/DataTables/jquery.dataTables.js",
                       "~/Scripts/DataTables/dataTables.bootstrap.js"));

            bundles.Add(new ScriptBundle("~/Scripts/jqueryui").Include(
                        "~/Scripts/jquery-ui-{version}.js"));

            bundles.Add(new ScriptBundle("~/Scripts/jqueryform").Include(
                        "~/Scripts/jquery.form.js"));

            bundles.Add(new ScriptBundle("~/Scripts/jquerydatetimepicker").Include("~/Scripts/jquery.datetimepicker.js"));

            bundles.Add(new StyleBundle("~/Content/datetimepicker").Include("~/Content/jquery.datetimepicker.css"));

            bundles.Add(new ScriptBundle("~/Scripts/swal").Include("~/Scripts/sweet-alert.min.js"));

            bundles.Add(new StyleBundle("~/Content/swal").Include("~/Content/sweet-alert.css"));

            bundles.Add(new StyleBundle("~/Content/fullcalendarcss").Include("~/Content/fullcalendar.css",
                "~/Content/fullcalendar.print.css"
                ));

            bundles.Add(new ScriptBundle("~/Scripts/fullcalendarjs").Include(
                   "~/Scripts/fullcalendar.js",
                   "~/Scripts/fullcalendar.min.js",
                   "~/Scripts/gcal.js",
                   "~/Scripts/es.js"
                   ));

            bundles.Add(new ScriptBundle("~/Scripts/foolproof").Include(
                       "~/Scripts/MvcFoolproofJQueryValidation.min.js",
                       "~/Scripts/MvcFoolproofValidation.min.js",
                       "~/Scripts/mvcfoolproof.unobtrusive.min.js"));

        }
    }
}
