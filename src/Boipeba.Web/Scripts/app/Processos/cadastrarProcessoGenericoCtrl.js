(function () {
    "use strict";

    cadastrarProcessoGenericoCtrl.$inject = ["$scope", "$http", "toastr"];

    function cadastrarProcessoGenericoCtrl($scope, $http, toastr) {
        $scope.viewdata = {
            model: {
                Cadastro: new Date().toLocaleDateString(),
                Urgente: false,
                Reservado: false
            }
        };

        $scope.tratarOuInteressado = function() {
            if ($scope.viewdata.model.Sociedade === true) {
                $scope.$broadcast("angucomplete-alt:clearInput", "ouInteressado");
                $scope.viewdata.OrgaoUnidadeInteressado = {};
            }
        };

        $scope.validaForm = function(form) {
            if (!$scope.viewdata.model.Sociedade && jQuery.isEmptyObject($scope.viewdata.OrgaoUnidadeInteressado)) {
                toastr.warning("Informe um valor para o interessado (Órgão/Unidade ou 'A sociedade'.)");
                return false;
            }

            if (!$scope.viewdata.Assunto || jQuery.isEmptyObject($scope.viewdata.Assunto)) {
                toastr.warning("Informe um valor para o assunto.");
                return false;
            }

            if (!$scope.viewdata.Destinatario ||
                jQuery.isEmptyObject($scope.viewdata.Destinatario)) {
                toastr.warning("Informe um destinatário.");
                return false;
            }

            return form.validate();
        };

        $scope.salvar = function() {
            if (!$scope.viewdata.model.Sociedade) {
                $scope.viewdata.model.OrgaoUnidadeInteressado = $scope.viewdata.OrgaoUnidadeInteressado.originalObject;
            }

            $scope.viewdata.model.Assunto = $scope.viewdata.Assunto.originalObject;
            $scope.viewdata.model.Destinatario = $scope.viewdata.Destinatario.originalObject;

            $http({
                method: "POST",
                url: "/Processos/Cadastro/Salvar",
                data: $scope.viewdata.model
            }).then(function successCallback(response) {
                    toastr.success("Operação realizada com sucesso.", "OK");
                },
                function errorCallback(response) {
                    toastr.error("Serviço indisponível no momento.", "Atenção");
                });
        };
    }

    angular
        .module("boipeba")
        .controller("cadastrarProcessoGenericoCtrl", cadastrarProcessoGenericoCtrl);
})();
