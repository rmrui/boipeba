(function () {
    "use strict";

    dashboardCtrl.$inject = ["$scope", "$http", "$window", "toastr"];

    function dashboardCtrl($scope, $http, $window, toastr) {

        $scope.view = {}
        $scope.viewdata = {}

        $scope.setup = function () {

            $scope.view.loadingList = true;

            $http({ method: "POST", url: "/Dashboard/GetViewData" })
                .then(function success(response) {
                        $scope.view.loadingList = false;
                        $scope.viewdata.processos = response.data.Processos;
                    },
                    function error(response) {
                        $scope.view.loadingList = false;
                    });
        }

        $scope.movimentar = function (id) {
            $("#id").val(id);
            $("#movimentarForm").submit();
        }

        $scope.historico = function (id) {
            $("#idHistorico").val(id);
            $("#historicoForm").submit();
        }

    }

    angular
        .module("boipeba")
        .controller("dashboardCtrl", dashboardCtrl);

})();
