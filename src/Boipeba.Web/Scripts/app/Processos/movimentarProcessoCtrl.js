(function () {
    "use strict";

    movimentarProcessoCtrl.$inject = ["$scope","$http","toastr"];
    
    function movimentarProcessoCtrl($scope, $http, toastr) {
        $scope.viewdata = {}

        $scope.setup = function (model) {
            $scope.viewdata.Processo = model;
        }

        $scope.validaForm = function (form) {
            if (!$scope.viewdata.Movimento ||
                jQuery.isEmptyObject($scope.viewdata.Movimento)) {
                toastr.warning("Informe o movimento.");
                return false;
            }

            if (form.validate()) {
                return true;
            }

            return false;
        }

        $scope.salvar = function() {
            $http({
                method: "POST",
                url: "/Processos/Movimentar/Salvar",
                data: $scope.viewdata.model
            }).then(function successCallback(response) {
                toastr.success("Operação realizada com sucesso.", "OK");
            }, function errorCallback(response) {
                toastr.error("Serviço indisponível no momento.", "Atenção");
            });
        }
    }
    
    angular
        .module("boipeba")
        .controller("movimentarProcessoCtrl", movimentarProcessoCtrl);
})();
