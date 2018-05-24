(function () {
    "use strict";

    /**
     * Obs.: A ordem dos módulos e plugins/dependências importa para correta injeção de dependência no angularjs
     */
    
    angular.module("boipeba", [
        "angular-ladda",
        "datatables",
        "datatables.bootstrap",
        "datatables.buttons",
        "datetime",
        "checklist-model",
        "frapontillo.bootstrap-switch",
        "lr.upload",
        "multiStepForm",
        "ngAnimate",
        "ngResource",
        "ngSanitize",
        "ngValidate",
        "toastr"
    ]);

})();