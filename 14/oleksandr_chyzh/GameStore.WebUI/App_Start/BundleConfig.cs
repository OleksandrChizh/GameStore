using System.Web.Optimization;

namespace GameStore.WebUI
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/layout/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/CustomStyles/layout.css",
                      "~/Content/bootstrap-select.css"));

            bundles.Add(new StyleBundle("~/bundles/bootstrap-select").Include(
                      "~/Scripts/bootstrap-select.js"));
        }
    }
}