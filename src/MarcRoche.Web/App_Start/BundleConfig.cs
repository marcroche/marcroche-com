using System.Web;
using System.Web.Optimization;

namespace MarcRoche.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            BundleTable.EnableOptimizations = true;

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            //bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
            //            "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jquery-ui-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/vendor").Include(
                //"~/Scripts/sammy-{version}.js"
                        "~/Scripts/underscore.js",
                        //"~/Scripts/angular.js",
                        "~/Scripts/d3.v3.js",
                        //"~/Scripts/knockout-{version}.js",
                        "~/Scripts/moment.js",
                        "~/Scripts/md5.js"
                        //"~/Scripts/knockout.mapping-latest.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/angular").Include(
                "~/Scripts/angular.js",
                "~/Scripts/angular-route.js",
                "~/Scripts/angular-animate.js",
                "~/Scripts/angular-mocks.js",
                "~/Scripts/angular-sanitize.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/admin").Include(
                "~/Scripts/moment.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/effects").Include(
                "~/Js/menu.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            //bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
            //          "~/Scripts/bootstrap.js",
            //          "~/Scripts/respond.js"));

            //bundles.Add(new StyleBundle("~/Content/css").Include(
            //          "~/Content/bootstrap.css",
            //          "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/css/styles")
                .Include(
                    "~/Content/css/normalize.css",
                    "~/Content/css/site.css",
                    "~/Content/css/blog.css",
                    "~/Content/css/tables.css",
                    "~/Content/css/layout.css")
                .IncludeDirectory("~/Content/css/areas", "*.css"));
        }
    }
}
