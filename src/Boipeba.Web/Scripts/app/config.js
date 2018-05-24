(function () {
    "use strict";

    config.$inject = ["toastrConfig"];

    function config(toastrConfig) {

        toastrConfig.positionClass = "toast-top-right";
        toastrConfig.closeButton = true;
        toastrConfig.progressBar = true;

        bootbox.setLocale("pt");
    }

    angular
        .module("boipeba")
        .config(config);
})();