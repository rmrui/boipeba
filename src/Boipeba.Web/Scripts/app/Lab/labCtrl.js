(function () {
    "use strict";

    labCtrl.$inject = ["$scope"];
    
    function labCtrl($scope) {

       // $scope.OrgaoUnidade = {}

        $scope.viewdata = {
                
        }
    }
    
    angular
        .module("boipeba")
        .controller("labCtrl", labCtrl);
})();
