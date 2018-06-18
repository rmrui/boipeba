(function () {
    "use strict";

    cadastrarProcessoGenericoCtrl.$inject = ["$scope", "$http", "toastr"];

    function cadastrarProcessoGenericoCtrl($scope, $http, toastr) {
        $scope.viewdata = {
            model: {
                Data: new Date().toLocaleDateString(),
                Urgente: false,
                Reservado: false
            }
        };

        $scope.tratarOuInteressado = function () {
            if ($scope.viewdata.model.Sociedade == true) {
                $scope.$broadcast("angucomplete-alt:clearInput", "ouInteressado");
                $scope.viewdata.OrgaoUnidadeInteressado = {};
            }
        }

        $scope.validaForm = function (form) {
            if (!$scope.viewdata.model.Sociedade && jQuery.isEmptyObject($scope.viewdata.OrgaoUnidadeInteressado)) {
                toastr.warning("Informe um valor para o interessado (Órgão/Unidade ou 'A sociedade'.)");
                return false;
            }

            if (!form.validate()) {
                return false;
            }

            return true;
        }

        $scope.enviar = function () {
            $http({
                method: "POST",
                url: "/Processos/Generico/Salvar",
                data: item
            }).then(function successCallback(response) {
                toastr.success("Operação realizada com sucesso.", "OK");
            });
        }
    }

    angular
        .module("boipeba")
        .controller("cadastrarProcessoGenericoCtrl", cadastrarProcessoGenericoCtrl);
})();
