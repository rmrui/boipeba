using System.Web;
using System.Web.Optimization;
using Boipeba.Web.Bundles;

namespace Boipeba.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            #region jQuery

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                "~/Scripts/jquery/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquery-plugins").Include(
                "~/Scripts/jquery/plugins/spin/spin.js",
                "~/Scripts/jquery/plugins/ladda/ladda.js",
                "~/Scripts/jquery/plugins/typeahead/typeahead.bundle.js",
                "~/Scripts/jquery/plugins/typeahead/typeahead.jquery.js",
                "~/Scripts/jquery/plugins/handlebars/handlebars.js",
                "~/Scripts/jquery/plugins/bloodhound/bloodhound.js",
                "~/Scripts/jquery/plugins/bootbox/bootbox.min.js",
                "~/Scripts/jquery/plugins/bootstrap-tour/bootstrap-tour.min.js",
                "~/Scripts/jquery/plugins/bootstrap-tour/bootstrap-tour-standalone.min.js"
            ));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                "~/Scripts/jquery/jquery.validate*",
                "~/Scripts/jquery/additional-methods.min.js",
                "~/Scripts/jquery/messages_pt_BR.js",
                "~/Scripts/jquery/jquery.mask.min.js",
                "~/Scripts/app/jquery.config.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquerySetupMasks").Include(
                "~/Scripts/app/jquerySetupMasks.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryConfig").Include(
                "~/Scripts/app/jquery.config.js"));

            #endregion

            #region Modernizr

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                "~/Scripts/modernizr-*"));

            #endregion

            #region Bootstrap Plugins

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                "~/Scripts/bootstrap/bootstrap.js",
                "~/Scripts/bootstrap/bootstrap-datepicker.js",
                "~/Scripts/bootstrap/locales/bootstrap-datepicker.pt-BR.min.js",
                "~/Scripts/bootstrap/bootstrap-switch.js",
                "~/Scripts/bootstrap/respond.js"
            ));

            bundles.Add(new StyleBundle("~/bundles/star-rating").Include(
                "~/Scripts/jquery/plugins/star-rating/star-rating.min.css"
            ));

            bundles.Add(new ScriptBundle("~/plugins/star-rating").Include(
                "~/Scripts/jquery/plugins/star-rating/star-rating.min.js"
            ));

            #endregion

            #region AngularJS

            bundles.Add(new ScriptBundle("~/bundles/angular").Include(
                "~/Scripts/angular/angular.min.js",
                "~/Scripts/angular/angular-resource.min.js",
                "~/Scripts/angular/angular-sanitize.min.js",
                "~/Scripts/angular/angular-locale_pt-br.js",
                "~/Scripts/angular/angular-upload.min.js",
                "~/Scripts/angular/angular-animate.min.js",
                "~/Scripts/angular/plugins/angucomplete-alt.min.js",
                "~/Scripts/angular/plugins/angular-validate.min.js",
                "~/Scripts/angular/plugins/angular-ladda.min.js",
                "~/Scripts/angular/plugins/angular-multi-step-form.min.js",
                "~/Scripts/angular/plugins/checklist-model.js",
                "~/Scripts/angular/plugins/angular-toastr/angular-toastr.tpls.min.js",
                "~/Scripts/angular/plugins/datetime.js",
                "~/Scripts/angular/plugins/dataTables/datatables.min.js",
                "~/Scripts/angular/plugins/dataTables/angular-datatables.min.js",
                "~/Scripts/angular/plugins/dataTables/buttons/angular-datatables.buttons.min.js",
                "~/Scripts/angular/plugins/dataTables/angular-datatables.bootstrap.min.js",
                "~/Scripts/angular/plugins/angular-bootstrap-switch.min.js",
                "~/Scripts/app/app.js",
                "~/Scripts/app/apiConfig.js",
                "~/Scripts/app/config.js",
                "~/Scripts/app/configSrv.js",
                "~/Scripts/app/directives.js",
                "~/Scripts/app/filters.js"));

            #endregion
            
            RegisterContentCSS(bundles);

            RegisterApp(bundles);

            BundleTable.EnableOptimizations = true;
        }

        private static void RegisterApp(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/loginManager").Include("~/Scripts/app/Login/loginManager.js"));

            bundles.Add(new ScriptBundle("~/bundles/dashboardCtrl").Include("~/Scripts/app/Dashboard/dashboardCtrl.js"));

            bundles.Add(new ScriptBundle("~/bundles/labCtrl").Include("~/Scripts/app/Lab/labCtrl.js"));

            bundles.Add(new ScriptBundle("~/bundles/solicitarDiariaCtrl").Include("~/Scripts/app/Tramitacao/solicitarDiariaCtrl.js"));

            bundles.Add(new ScriptBundle("~/bundles/cadastrarProcessoGenericoCtrl").Include("~/Scripts/app/Processos/cadastrarProcessoGenericoCtrl.js"));

            bundles.Add(new ScriptBundle("~/bundles/simpleCrudCtrl").Include("~/Scripts/app/crud/simpleCrudCtrl.js"));

            ConfigBundles.RegisterBundles(bundles);
        }

        private static void RegisterContentCSS(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Scripts/jquery/plugins/bootstrap-tour/bootstrap-tour.min.css",
                "~/Scripts/jquery/plugins/bootstrap-tour/bootstrap-tour-standalone.min.css",

                "~/Content/angucomplete-alt.css",
                "~/Content/bootstrap.css",
                "~/Content/bootstrap-theme.min.css",
                "~/Content/bootstrap-checkbox.css",
                "~/Content/bootstrap-datepicker3.css",
                "~/Content/bootstrap-switch.css",

                "~/Content/ladda-themeless.min.css",
                "~/Content/Table.css",
                "~/Content/font-awesome.min.css",

                "~/Scripts/angular/plugins/angular-toastr/angular-toastr.min.css",
                "~/Scripts/angular/plugins/dataTables/datatables.min.css",
                "~/Scripts/angular/plugins/dataTables/angular-datatables.min.css",
                "~/Scripts/angular/plugins/dataTables/datatables.bootstrap.min.css",

                "~/Content/site.css"));
        }
    }
}
