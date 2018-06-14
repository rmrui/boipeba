(function () {
    "use strict";

    cadastrarProcessoGenericoCtrl.$inject = ["$scope", "$http"];

    function cadastrarProcessoGenericoCtrl($scope, $http) {
        $scope.viewdata = {
            model: {
                Data: new Date().toLocaleDateString()
            }
        };

        $scope.buscarOU = function(description) {
            var results = [];
            $http({
                    method: "GET",
                    url: "/Config/OrgaoUnidade/Find",
                    data: description
                })
                .then(function(response) {
                    results = response.data;
                });
            return results;
        }
    }

    angular
        .module("boipeba")
        .controller("cadastrarProcessoGenericoCtrl", cadastrarProcessoGenericoCtrl);
})();
