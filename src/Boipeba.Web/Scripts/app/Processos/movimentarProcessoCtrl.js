(function () {
    "use strict";

    movimentarProcessoCtrl.$inject = ["$scope", "$http", "toastr"];

    function movimentarProcessoCtrl($scope, $http, toastr) {

        $scope.view = {
            invalidMovimento: false,
            invalidDestino: false
        }

        $scope.setup = function (processo) {
            $scope.viewdata = { model: { Processo: {Id: processo.Id} } }
        }

        $scope.validaForm = function (form) {

            if (!$scope.viewdata.Movimento) {
                $scope.view.invalidMovimento = true;
                return false;
            }

            $scope.view.invalidMovimento = false;


            if ($scope.viewdata.Destinatario) {

                var destino = $scope.viewdata.Destinatario.originalObject;

                if ($scope.verificarMesmaPessoa(destino) || $scope.verificarMesmoOU(destino)) {

                    toastr.warning("Informe um destino diferente do atual.");

                    return false;
                }

            }

            if (form.validate()) {
                return true;
            }

            return false;
        }

        $scope.verificarMesmaPessoa = function (destino) {
            return destino.Tipo === "Pessoa" && $scope.viewdata.model.Processo.PessoaDestino && destino.Id === $scope.viewdata.Processo.PessoaDestino.Matricula;
        }

        $scope.verificarMesmoOU = function (destino) {
            return destino.Tipo === "OrgaoUnidade" && $scope.viewdata.model.Processo.OrgaoUnidadeDestino && destino.Id === $scope.viewdata.Processo.OrgaoUnidadeDestino.IdOrgaoUnidade;
        }


        $scope.salvar = function () {
            $scope.viewdata.model.Movimento = {
                CdMovimento: $scope.viewdata.Movimento.originalObject.CdMovimento
            };

            if ($scope.viewdata.Destinatario) {
                $scope.viewdata.model.Destinatario = $scope.viewdata.Destinatario.originalObject;
            }

            $http({
                method: "POST",
                url: "/Processos/Movimentar/Salvar",
                data: $scope.viewdata.model
            }).then(function successCallback(response) {
                toastr.success("Operação realizada com sucesso.", "OK");
            }, function errorCallback(response) {
                if (response.status === 500) {
                    $scope.viewdata.errorMsg = response.data.message;
                }
            });
        }
    }

    angular
        .module("boipeba")
        .controller("movimentarProcessoCtrl", movimentarProcessoCtrl);
})();
