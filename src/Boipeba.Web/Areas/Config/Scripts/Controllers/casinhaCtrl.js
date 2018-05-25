(function () {
    "use strict";

    casinhaCtrl.$inject = ["$scope", "$http", "$filter", "toastr", "configSrv"];
    
    function casinhaCtrl($scope, $http, $filter, toastr, configSrv) {

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
            $scope.viewdata.casinha = angular.copy(item);
            console.log(item);
            $scope.viewdata.casinha.Data = $filter("jsdate")(item.Data);
            $("#casinhaModal").modal("show");
        }
        
        $scope.nova = function () {
            $scope.viewdata.casinha = {};
            $("#casinhaModal").modal("show");
        }
        
        $scope.setup = function () {

            $scope.view.loadingList = true;

            $http({ method: "POST", url: "/Config/Casinha/GetViewData" })
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

            $scope.viewdata.casinha = {};
            
            $http({
                method: "POST",
                url: "/Config/Casinha/Salvar",
                data: model
            }).then(function successCallback(response) {
                $scope.view.loading = false;
                toastr.success("Operação realizada com sucesso.", "OK");
                $scope.setup();
                $("#casinhaModal").modal("hide");
            }, function errorCallback(response) {
                $scope.view.loading = false;
                toastr.error("Serviço indisponível no momento.", "Atenção");
            });
        }

        $scope.excluir = function (item) {
            $http({
                method: "POST",
                url: "/Config/Casinha/Excluir",
                data: item
            }).then(function successCallback(response) {
                $scope.view.loading = false;
                toastr.success("Operação realizada com sucesso.", "OK");
                $scope.setup();
                },
                function errorCallback(response) {
                    $scope.view.loading = false;
                    toastr.error("Serviço indisponível no momento.", "Atenção");
                });
        }
    }
    
    angular
        .module("boipeba")
        .controller("casinhaCtrl", casinhaCtrl);
})();
