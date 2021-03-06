(function () {
    "use strict";

    simpleCrudCtrl.$inject = ["$scope", "$http", "$filter", "toastr", "configSrv"];

    function simpleCrudCtrl($scope, $http, $filter, toastr, configSrv) {

        $scope.view = {
            baseUrl: "",
            loading: false,
            dtOptions: configSrv.getDtOptions("Nenhum registro encontrado")
        };

        $scope.viewdata = {
            model: {},
            list: []
        };

        $scope.setup = function(baseurl) {

            $scope.view.baseUrl = baseurl;

            $scope.getViewData();
        }

        $scope.getViewData = function() {

            $scope.viewdata.loading = true;

            $http({ method: "POST", url: $scope.view.baseUrl + "/GetViewData" })
                .then(function success(response) {
                        $scope.view.loading = false;
                        $scope.viewdata.list = response.data;
                    },
                    function error(response) {
                        $scope.view.loading = false;
                    });
        }
        
        $scope.editar = function (item) {
            $scope.viewdata.model = angular.copy(item);
            $("#formModal").modal("show");
        }
        
        $scope.novo = function () {
            $scope.viewdata.model = {};
            $("#formModal").modal("show");
        }
        
        $scope.salvar = function(form) {

            if (!form.validate()) {
                return false;
            }

            $scope.view.loading = true;

            $http({
                method: "POST",
                url: $scope.view.baseUrl + "/Salvar",
                data: $scope.viewdata.model
            }).then(function successCallback(response) {

                    toastr.success("Operação realizada com sucesso.", "Concluído.");

                    $scope.viewdata.model = {};
                    $scope.view.loading = false;
                    $("#formModal").modal("hide");
                    $scope.getViewData();
                    
                },
                function errorCallback(response) {
                    toastr.error(response.data.message, "Atenção.");
                    $scope.view.loading = false;
                    $("#formModal").modal("hide");
                });
        }

        $scope.excluir = function(item) {

            bootbox.confirm({
                size: "small",
                title: "Atenção",
                message: "Confirmar exclusão?",
                callback: function(result) {

                    if (!result) return;

                    $scope.view.loading = true;

                    $http({
                        method: "POST",
                        url: $scope.view.baseUrl + "/Excluir",
                        data: item
                    }).then(function successCallback(response) {

                            toastr.success("Operação realizada com sucesso.", "Concluído.");

                            $scope.viewdata.model = {};
                            $scope.view.loading = false;
                            $("#formModal").modal("hide");
                            $scope.getViewData();
                        },
                        function errorCallback(response) {

                            toastr.error(response.data.message, "Atenção.");

                            $scope.view.loading = false;
                            $("#formModal").modal("hide");
                        });
                }
            });
        }
    }

    angular
        .module("boipeba")
        .controller("simpleCrudCtrl", simpleCrudCtrl);
})();
