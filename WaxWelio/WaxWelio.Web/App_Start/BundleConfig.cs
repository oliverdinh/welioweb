using System.Web.Optimization;

namespace WaxWelio.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            #region jquery, bootstrap, validate
            bundles.Add(new ScriptBundle("~/js").Include(
                "~/static/js/jquery-3.1.1.min.js",
                "~/static/js/bootstrap.min.js",
                "~/static/js/jquery.maskedinput.js",
                 "~/static/plugin/jquery-ui/jquery-ui.js"));

            bundles.Add(new StyleBundle("~/css").Include(
                "~/static/css/bootstrap.min.css",
                "~/static/css/bootstrap-theme.min.css",
                "~/static/plugin/jquery-ui/jquery-ui.css",
                "~/static/css/ndlong.css"));

            bundles.Add(new ScriptBundle("~/validate").Include(
                      "~/static/js/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/ConfigurePage").Include(
                "~/Scripts/swfobject.js",
                "~/Scripts/speedtest.js",
                "~/Scripts/TrustedApiAuth.js",
                "~/Scripts/ConfigurePage.js"));
            #endregion

            #region DataTable
            //JS
            bundles.Add(new ScriptBundle("~/js/datatable").Include(
                "~/static/plugin/DataTables/media/js/jquery.dataTables.min.js",
                "~/static/plugin/DataTables/media/js/dataTables.bootstrap.min.js",
                "~/static/plugin/DataTables/extensions/Select/js/dataTables.select.min.js",
                "~/static/plugin/DataTables/media/js/dataTables.fixedColumns.min.js",
                "~/static/js/fnSetFilteringDelay.js",
                "~/static/js/global.js"));

            //CSS
            bundles.Add(new StyleBundle("~/css/datatable").Include(
                "~/static/plugin/DataTables/media/css/jquery.dataTables.min.css"));
            #endregion

            #region Move late
            /*
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));
                      */
            #endregion
        }
    }
}
