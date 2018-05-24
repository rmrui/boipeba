(function () {
    "use strict";

    dashboardCtrl.$inject = ["$scope", "$http", "$window", "toastr"];

    function dashboardCtrl($scope, $http, $window, toastr) {
        $scope.msg = "Rui";
    }

    angular
        .module("boipeba")
        .controller("dashboardCtrl", dashboardCtrl);

})();
