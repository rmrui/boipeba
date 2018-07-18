(function () {
    "use strict";

    movimentarProcessoCtrl.$inject = ["$scope", "$http", "toastr"];

    function movimentarProcessoCtrl($scope, $http, toastr) {

        CKEDITOR.replace(document.getElementById("editor"));

        $scope.view = {
            invalidMovimento: false,
            invalidDestino: false,
            showform: true,
            showsuccess: false
        }

        $scope.setup = function (processo) {
            $scope.viewdata = { model: { Movimento: {} } }
            $scope.viewdata.model.Processo = processo;
        }

        $scope.editarComplemento = function() {
            $("#editarComplementoModal").modal("show");
        }
        

        $scope.validaForm = function (form) {

            if (!$scope.viewdata.Movimento) {
                $scope.view.invalidMovimento = true;
                return false;
            }

            $scope.view.invalidMovimento = false;


            if ($scope.viewdata.Destino) {

                var destino = $scope.viewdata.Destino.originalObject;

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
            return destino.Tipo === "Pessoa" && $scope.viewdata.model.Processo.PessoaDestino && destino.Id === $scope.viewdata.model.Processo.PessoaDestino.Matricula;
        }

        $scope.verificarMesmoOU = function (destino) {
            return destino.Tipo === "OrgaoUnidade" && $scope.viewdata.model.Processo.OrgaoUnidadeDestino && destino.Id === $scope.viewdata.model.Processo.OrgaoUnidadeDestino.IdOrgaoUnidade;
        }


        $scope.salvar = function () {
            var processoLimpo = {Id: $scope.viewdata.model.Processo.Id}

            $scope.viewdata.model.Movimento = {
                CdMovimento: $scope.viewdata.Movimento.originalObject.CdMovimento,
                Processo: processoLimpo
            };

            $scope.viewdata.model.Complemento = CKEDITOR.instances.editor.getData();

            if ($scope.viewdata.Destino) {
                $scope.viewdata.model.Destino = $scope.viewdata.Destino.originalObject;
            }

            $http({
                method: "POST",
                url: "/Processos/Movimentar/Salvar",
                data: $scope.viewdata.model
            }).then(function successCallback(response) {
                $scope.view.showform = false;
                $scope.view.showsuccess = true;
            }, function errorCallback(response) {
                if (response.status === 500) {
                    $scope.viewdata.errorMsg = "Serviço insidponível no momento.";
                }
            });
        }

        $scope.historico = function () {
            $("#idHistorico").val($scope.viewdata.model.Processo.Id);
            $("#historicoForm").submit();
        }
    }

    angular
        .module("boipeba")
        .controller("movimentarProcessoCtrl", movimentarProcessoCtrl);
})();
