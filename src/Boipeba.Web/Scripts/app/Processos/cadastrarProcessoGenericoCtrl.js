(function () {
    "use strict";

    cadastrarProcessoGenericoCtrl.$inject = ["$scope"];

    function cadastrarProcessoGenericoCtrl($scope) {
        $scope.viewdata = {
            model: {
                Data: new Date().toLocaleDateString()
            }
        };
    }

    angular
        .module("boipeba")
        .controller("cadastrarProcessoGenericoCtrl", cadastrarProcessoGenericoCtrl);
})();
