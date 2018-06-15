(function () {
    "use strict";

    function apiConfig() {
        return {
            orgaoUnidade: function() {
                return { url: "/api/orgaosunidades/?part=" };
            }
        }
    }

    angular
        .module("boipeba")
        .factory("apiConfig", apiConfig);
})();