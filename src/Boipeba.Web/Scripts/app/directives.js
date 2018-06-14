(function () {
    "use strict";

    function tooltip() {
        return {
            restrict: "A",
            link: function (scope, element, attrs) {
                $(element).hover(function () {
                    $(element).tooltip("show");
                }, function () {
                    $(element).tooltip("hide");
                });
            }
        };
    }

    function simnaoswitch() {
        return {
            restrict: "A",
            link: function (scope, element, attrs) {

                $(element).bootstrapSwitch({
                    onText: "Sim", offText: "Não", onColor: "primary", offColor: "default", state: true
                });
            }
        };
    }

    function loading() {
        return {
            restrict: "E",
            template: function (element, attrs) {

                if (attrs.icon)
                    return "<img src='/images/loadingsm.gif' ng-show='" + attrs.target + "'/>";

                return "<div class='alert alert-info' ng-show='" + attrs.target + "'><img src='/images/loadingsm.gif' /> Carregando...</div>";
            }
        };
    }

    function ajuda() {
        return {
            restrict: "E",
            template: function (element, attrs) {

                return "<a href=\"#\" ng-show=\"view.tour\" ng-click=\"view.tour.init();view.tour.restart();\" class=\"button-tour\" style=\"color: white;\"><span class=\"fa fa-question-circle\"></span><span>&nbsp;Ajuda Interativa</span></a>";
            }
        };
    }

    orgaoUnidade.$inject = ["apiConfig"];
    function orgaoUnidade(apiConfig) {
        return {
            restrict: "E",
            template: function (element, attrs) {

                return "<div angucomplete-alt id='ou' placeholder='Digite o Órgão/Unidade' pause='400' selected-object='" + attrs.selectedObject + "' remote-url='" + apiConfig.orgaoUnidade().url + "' remote-url-data-field='' title-field='DsOrgaoUnidade' minlength='3' match-class='highlight' input-class='form-control form-control-small'/>";
            }
        };
    }

    angular
        .module("boipeba")
        .directive("loading", loading)
        .directive("ajuda", ajuda)
        .directive("simnaoswitch", simnaoswitch)
        .directive("tooltip", tooltip)
        .directive("orgaoUnidade", orgaoUnidade);
})();
