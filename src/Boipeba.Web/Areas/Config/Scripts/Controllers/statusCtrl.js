(function () {
    "use strict";

    statusCtrl.$inject = ["$scope", "$http", "$filter", "toastr", "configSrv"];
    
    function statusCtrl($scope, $http, $filter, toastr, configSrv) {

        $scope.view = {
            dtOptions: configSrv.getDtOptions("Nenhum registro encontrado")
        };

        $scope.viewdata = {
            status: {Fechamento: false}
        };

        $scope.validaForm = function (form) {
            if (form.validate()) {
                return true;
            }

            return false;
        }

        $scope.editar = function(item) {
            $scope.viewdata.status = angular.copy(item);
            $("#statusModal").modal("show");
        }
    

        $scope.nova = function() {
            $("#statusModal").modal("show");
        }
        
        $scope.setup = function () {

            $scope.view.loadingList = true;

            $http({ method: "POST", url: "/Config/Status/GetViewData" })
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

            $scope.viewdata.status = {};

            $http({
                method: "POST",
                url: "/Config/Status/Salvar",
                data: model
            }).then(function successCallback(response) {
                $scope.view.loading = false;

                toastr.success("Operação realizada com sucesso.", "OK");
                $scope.setup();
                $("#statusModal").modal("hide");

            }, function errorCallback(response) {
                $scope.view.loading = false;
                toastr.error("Serviço indisponível no momento.", "Atenção");
            });
        }
    }
    
    angular
        .module("boipeba")
        .controller("statusCtrl", statusCtrl);
})();
