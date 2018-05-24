(function () {
    "use strict";

    configSrv.$inject = ["DTOptionsBuilder"];

    function configSrv(DTOptionsBuilder) {
        return {
            getTour: function(stepsSrv) {
                var tour = new Tour({
                    storage: false,
                    template: "<div class='popover tour'><div class='arrow'></div><h3 class='popover-title'></h3><div class='popover-content'></div><div class='popover-navigation'><button class='btn btn-default' data-role='prev'>«</button><button class='btn btn-default' data-role='next'>»</button><button class='btn btn-default' data-role='end'>Fechar</button><div></div>"
                });

                tour.addSteps(stepsSrv.getSteps());

                return tour;
            },
            /* buttons = { Exibir, Excel, Pdf, Imprimir, Arquivo } */
            getDtOptions: function (msg, buttons) {

                var options = DTOptionsBuilder.newOptions().withPaginationType("simple_numbers")
                    .withOption("ordering", false)
                    .withOption("responsive", true)
                    .withLanguage({
                        decimal: ",",
                        processing: "Processando...",
                        search: "Filtrar tabela:",
                        lengthMenu: "Mostrar _MENU_ registros",
                        info: "Exibindo de _START_ à _END_ de _TOTAL_ registros",
                        infoEmpty: "Exibindo de 0 à 0 de 0 registros",
                        infoFiltered: "(filtrado de _MAX_ registros totais)",
                        infoPostFix: "",
                        loadingRecords: "Processando...",
                        zeroRecords: msg,
                        emptyTable: msg,
                        paginate: {
                            first: "Primeiro",
                            previous: "Anterior",
                            next: "Próximo",
                            last: "Último"
                        },
                        aria: {
                            sortAscending: ": Ordem ascendente",
                            sortDescending: ": Ordem decrescente"
                        }
                    })
                    .withBootstrap();

                if (!buttons)
                    return options;
                    

                var properties = [];

                if (buttons.exibir)
                    properties.push({ extend: "colvis", columns: ":gt(0)", text: "Exibir Colunas" });

                if (buttons.excel)
                    properties.push({ extend: "excel", title: buttons.arquivo, message: "Coordenadoria de Segurança Institucional e Inteligência" });

                if (buttons.pdf)
                    properties.push({ extend: "pdf", title: buttons.arquivo, message: "Coordenadoria de Segurança Institucional e Inteligência" });

                if (buttons.imprimir) {
                    properties.push({
                        extend: "print",
                        text: "Imprimir",
                        customize: function (win) {
                            $(win.document.body).addClass("white-bg");
                            $(win.document.body).css("font-size", "10px");

                            $(win.document.body).find("table")
                                .addClass("compact")
                                .css("font-size", "inherit");
                        }
                    });
                }

                return options.withButtons(properties);
            }
        }
    }

    angular
        .module("boipeba")
        .factory("configSrv", configSrv);
})();