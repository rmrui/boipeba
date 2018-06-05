(function () {
    "use strict";

    solicitarDiariaCtrl.$inject = ["$scope"];

    function solicitarDiariaCtrl($scope) {
        $scope.viewdata = {
            model: {
                SituacaoRisco: false
            }
        };
    }


    angular
        .module("boipeba")
        .controller("solicitarDiariaCtrl", solicitarDiariaCtrl);
})();
