(function () {
    "use strict";

    historicoProcessoCtrl.$inject = ["$scope", "$http", "toastr"];

    function historicoProcessoCtrl($scope, $http, toastr) {

        $scope.view = {
        }

        $scope.viewdata = {model: {} }

        $scope.setup = function (idProcesso) {
            
            $scope.view.loadingList = true;

            $http({ method: "POST", url: "/Processos/Historico/GetViewData", data: { idProcesso } })
                .then(function success(response) {
                        $scope.view.loadingList = false;
                        $scope.viewdata.model.Items = response.data;
                        
                    },
                    function error(response) {
                        $scope.view.loadingList = false;
                        console.log(response);
                        console.log(idProcesso);
                    });

            console.log("boa 06");
        }
    }

    angular
        .module("boipeba")
        .controller("historicoProcessoCtrl", historicoProcessoCtrl);
})();
