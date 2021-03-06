﻿(function () {
    "use strict";

    simpleCrudCtrl.$inject = ["$scope", "$http", "$filter", "toastr", "configSrv"];
    
    function simpleCrudCtrl($scope, $http, $filter, toastr, configSrv) {

        $scope.view = {
            dtOptions: configSrv.getDtOptions("Nenhum registro encontrado")
        };

        $scope.viewdata = {};

        $scope.validaForm = function (form) {
            if (form.validate()) {
                return true;
            }

            return false;
        }

        $scope.editar = function (item) {
            $scope.viewdata.xxxx = angular.copy(item);
            $("#xxxx").modal("show");
        }
        
        $scope.nova = function () {
            $("#xxxxx").modal("show");
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
                toastr.success("Operação realizada com sucesso.", "OK");
                $scope.setup();
                $("#xxxx").modal("hide");
            }, function errorCallback(response) {
                $scope.view.loading = false;
                toastr.error("Serviço indisponível no momento.", "Atenção");
                $("#xxxx").modal("hide");
            });
        }

        $scope.excluir = function (item) {
            $http({
                method: "POST",
                url: "/URL/Excluir",
                data: item
            }).then(function successCallback(response) {
                $scope.view.loading = false;
                    toastr.success("Operação realizada com sucesso.", "OK");
                },
                function errorCallback(response) {
                    $scope.view.loading = false;
                    toastr.error("Serviço indisponível no momento.", "Atenção");
                });
        }
    }
    
    angular
        .module("modulo")
        .controller("controllerCtrl", simpleCrudCtrl);
})();
