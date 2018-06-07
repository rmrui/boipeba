(function () {
    "use strict";

    solicitarDiariaCtrl.$inject = ["$scope"];

    function solicitarDiariaCtrl($scope) {
        $scope.viewdata = {
            model: {
                SituacaoRisco: false,
                AlimentacaoHospedagem: false
            }
        };

        $scope.abrirModalBeneficiario = function() {
            $("#modalExibirBeneficiario").modal("show");
        }
    }

    angular
        .module("boipeba")
        .controller("solicitarDiariaCtrl", solicitarDiariaCtrl);
})();
