using System.Web.Optimization;

namespace Boipeba.Web.Bundles
{
    public class ConfigBundles
    {
        private const string Path = "~/Areas/Config/Scripts/Controllers";

        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/statusCtrl")
                .Include(
                $"{Path}/statusCtrl.js",
                $"~/Areas/Config/Scripts/Tours/statusTour.js"
                ));
        }
    }
}
