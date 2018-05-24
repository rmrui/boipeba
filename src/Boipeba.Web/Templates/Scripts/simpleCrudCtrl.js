(function () {
    "use strict";

    simpleCrudCtrl.$inject = ["$scope", "$http", "$window", "$filter", "toastr"];
    
    function simpleCrudCtrl($scope, $http, $window, $filter, toastr) {

        $scope.view = {};
        $scope.viewdata = {};

        $scope.validaForm = function (form) {
            if (form.validate()) {
                return true;
            }

            return false;
        }
        
        $scope.setup = function () {

            $scope.view.loadingList = true;

            $http({ method: "POST", url: "/URL/GetViewData" })
                .then(function success(response) {
                        $scope.view.loadingList = false;
                        $scope.viewdata.list = response.data;
                    },
                    function error(response) {
                        $scope.view.loadingList = false;
                    });
        }

        $scope.salvar = function (item) {

            $scope.view.loading = true;

            var model = angular.copy(item);
            
            $http({
                method: "POST",
                url: "/URL/Salvar",
                data: model
            }).then(function successCallback(response) {
                $scope.view.loading = false;
                toastr.success("Operação realizada com sucesso.", "Usuário Cadastrado");
            }, function errorCallback(response) {
                $scope.view.loading = false;
                toastr.error("Serviço indisponível no momento.", "Atenção");
            });
        }

        $scope.excluir = function (item) {
            $http({
                method: "POST",
                url: "/URL/Excluir",
                data: item
            }).then(function successCallback(response) {
                $scope.view.loading = false;
                    toastr.success("Operação realizada com sucesso.", "Usuário Inativado");
                },
                function errorCallback(response) {
                    $scope.view.loading = false;
                    toastr.error("Serviço indisponível no momento.", "Atenção");
                });
        }
    }
    
    angular
        .module("scsi.modulo")
        .controller("controllerCtrl", simpleCrudCtrl);
})();
