(function () {
    "use strict";

    function apiConfig() {
        return {
            orgaoUnidade: function() {
                return { url: "http://localhost:61815/api/orgaosunidades/?part=" };
            }
        }
    }

    angular
        .module("boipeba")
        .factory("apiConfig", apiConfig);
})();