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
            if ($scope.viewdata.model.Sociedade) {
                $scope.$broadcast("angucomplete-alt:clearInput", "ouInteressado");
            }
        }

        $scope.validaForm = function (form) {
            jQuery.validator.addClassRules("interessado-group", {
                require_from_group: [1, ".interessado-group"]
            });

            if (!form.validate({
                groups: {
                interessadogroup: "ouInteressado sociedade"
            },
                messages: {
                ouInteressado: { require_from_group: "Informe um valor." },
                    sociedade: { require_from_group: "Mensagem bizarra" }
            }
            })) {
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
