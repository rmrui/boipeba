(function () {
    "use strict";

    function statusTour() {
        return {
            getSteps: function () {
                return [
                    {
                        element: ".tour-1",
                        placement: "top",
                        backdrop: true,
                        title: "Opções",
                        content: "Botões de ações para funcionalidade. Nova: Incluir nova situação."
                    },
                    {
                        element: ".tour-2",
                        placement: "bottom",
                        backdrop: true,
                        title: "Listagem de Registros",
                        content: "Lista de situações cadastradas."
                    }
                ];
            }
        }
    }

    angular
        .module("boipeba")
        .factory("statusTour", statusTour);
})();