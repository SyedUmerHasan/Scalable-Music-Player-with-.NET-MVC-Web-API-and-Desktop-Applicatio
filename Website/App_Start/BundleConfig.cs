using System.Web;
using System.Web.Optimization;

namespace Website
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/admincss").Include(
                      "~/Content/admin_theme/app-assets/css/vendors.css",
                      "~/Content/admin_theme/app-assets/css/app.css",
                      "~/Content/admin_theme/app-assets/css/core/menu/menu-types/vertical-menu-modern.css",
                      "~/Content/admin_theme/app-assets/css/core/colors/palette-gradient.css",
                      "~/Content/admin_theme/app-assets/vendors/css/charts/jquery-jvectormap-2.0.3.css",
                      "~/Content/admin_theme/app-assets/vendors/css/charts/morris.css",
                      "~/Content/admin_theme/app-assets/fonts/simple-line-icons/style.css",
                      "~/Content/admin_theme/app-assets/assets/css/style.css"));

            bundles.Add(new StyleBundle("~/Content/adminjs").Include(
                      "~/Content/admin/app-assets/vendors/js/vendors.min.js",
                      "~/Content/admin/app-assets/vendors/js/charts/chart.min.js",
                      "~/Content/admin/app-assets/vendors/js/charts/raphael-min.js",
                      "~/Content/admin/app-assets/vendors/js/charts/morris.min.js",
                      "~/Content/admin/app-assets/vendors/js/charts/jvector/jquery-jvectormap-2.0.3.min.js",
                      "~/Content/admin/app-assets/vendors/js/charts/jvector/jquery-jvectormap-world-mill.js",
                      "~/Content/admin/app-assets/data/jvector/visitor-data.js",
                      "~/Content/admin/app-assets/js/core/app-menu.js",
                      "~/Content/admin/app-assets/js/core/app.js",
                      "~/Content/admin/app-assets/js/scripts/customizer.js",
                      "~/Content/admin/app-assets/js/scripts/pages/dashboard-sales.js"));

        }
    }
}
