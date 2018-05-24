(function () {
    "use strict";

    /**
     * Obs.: A ordem dos módulos e plugins/dependências importa para correta injeção de dependência no angularjs
     */

    angular.module("boipeba",
        [
            "boipeba.global",
            "boipeba.dashboard"
        ]);

    angular.module("boipeba.global", [
                "ngResource",
                "multiStepForm",
                "checklist-model",
                "angular-ladda",
                "lr.upload",
                "ngAnimate",
                "toastr",
                "ngValidate",
                "datetime",
                "datatables",
                "datatables.buttons",
                "datatables.bootstrap",
                "frapontillo.bootstrap-switch"
    ]);

    //angular.module("scsi.solicitacao",
    //    [
    //        "ngResource",
    //        "multiStepForm",
    //        "checklist-model",
    //        "angular-ladda",
    //        "lr.upload",
    //        "ngAnimate",
    //        "toastr",
    //        "ngValidate",
    //        "datetime",
    //        "datatables",
    //        "datatables.buttons",
    //        "datatables.bootstrap",
    //        "frapontillo.bootstrap-switch",
    //        "scsi.global"
    //    ]);

    //angular.module("scsi.pesquisa",
    //    [
    //        "datetime",
    //        "datatables",
    //        "datatables.buttons",
    //        "datatables.bootstrap",
    //        "angular-ladda",
    //        "checklist-model",
    //        "ngAnimate",
    //        "toastr",
    //        "ngValidate",
    //        "frapontillo.bootstrap-switch",
    //        "scsi.global"
    //    ]);

    //angular.module("scsi.acesso",
    //    [
    //        "datetime",
    //        "datatables",
    //        "datatables.buttons",
    //        "datatables.bootstrap",
    //        "ngResource",
    //        "checklist-model",
    //        "angular-ladda",
    //        "ngAnimate",
    //        "toastr",
    //        "ngValidate",
    //        "datetime",
    //        "scsi.global"
    //    ]);

    //angular.module("scsi.admin",
    //    [
    //        "datetime",
    //        "datatables",
    //        "datatables.buttons",
    //        "datatables.bootstrap",
    //        "angular-ladda",
    //        "checklist-model",
    //        "ngAnimate",
    //        "toastr",
    //        "ngValidate",
    //        "scsi.global"
    //    ]);
    
    //angular.module("scsi.geoproc",
    //    [
    //        "datetime",
    //        "datatables",
    //        "datatables.buttons",
    //        "datatables.bootstrap",
    //        "angular-ladda",
    //        "checklist-model",
    //        "ngAnimate",
    //        "toastr",
    //        "ngValidate",
    //        "scsi.global"
    //    ]);
    
    //angular.module("scsi.gestao",
    //    [
    //        "datetime",
    //        "datatables",
    //        "datatables.buttons",
    //        "datatables.bootstrap",
    //        "angular-ladda",
    //        "checklist-model",
    //        "ngAnimate",
    //        "toastr",
    //        "ngValidate",
    //        "scsi.global"
    //    ]);


    //angular.module("scsi.nti",
    //    [
    //        "datetime",
    //        "datatables",
    //        "datatables.buttons",
    //        "datatables.bootstrap",
    //        "angular-ladda",
    //        "checklist-model",
    //        "ngAnimate",
    //        "toastr",
    //        "ngValidate",
    //        "scsi.global"
    //    ]);

    //angular.module("scsi.csi",
    //    [
    //        "datatables",
    //        "datatables.buttons",
    //        "datatables.bootstrap",
    //        "datetime",
    //        "checklist-model",
    //        "angular-ladda",
    //        "ngAnimate",
    //        "toastr",
    //        "lr.upload",
    //        "ngValidate",
    //        "scsi.global"
    //    ]);

    //angular.module("scsi.dashboard",
    //    [
    //        "angular-ladda",
    //        "ngAnimate",
    //        "toastr",
    //        "ngValidate",
    //        "ngSanitize",
    //        "scsi.global"
    //    ]);

})();