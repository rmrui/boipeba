(function () {
    "use strict";

    historicoProcessoCtrl.$inject = ["$scope", "$http", "toastr"];

    function historicoProcessoCtrl($scope, $http, toastr) {

        $scope.view = {
        }

        $scope.viewdata = { model: {} }

        $scope.setup = function (idProcesso) {

            $scope.view.loadingList = true;

            $http({ method: "POST", url: "/Processos/Historico/GetViewData", data: { idProcesso } })
                .then(function success(response) {
                    $scope.view.loadingList = false;
                    $scope.viewdata.model.Items = response.data.Movimentos;
                    $scope.viewdata.model.Processo = response.data.Processo;
                },
                    function error(response) {
                        $scope.view.loadingList = false;
                    });
        }

        $scope.movimentar = function () {
            $("#id").val($scope.viewdata.model.Processo.Id);
            $("#movimentarForm").submit();
        }

        $scope.exibirComplemento = function(item) {
            $scope.viewdata.Complemento = item;
            $("#detalheMovimentoModal").modal("show");
        }
    }

    angular
        .module("boipeba")
        .controller("historicoProcessoCtrl", historicoProcessoCtrl);
})();
