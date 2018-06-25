(function () {
    "use strict";
    
    function leftpad() {
        return function (value, size) {
            var sign = Math.sign(this) === -1 ? '-' : '';
            return sign + new Array(size).concat([Math.abs(value)]).join('0').slice(-size);
        }
    }

    jsdate.$inject = ["datetime"];

    /* extrai apenas os números da formatação /date(num)/ */
    function jsdate(datetime) {
        return function (item) {

            if (item == null) return "";

            var num = new Date(parseInt(item.substr(6)));

            var parser = datetime("dd/MM/yyyy");
            parser.setDate(num);
            return parser.getText();
        }
    };

    /* extrai apenas os números da formatação /date(num)/ */
    function jsdatetime(datetime) {
        return function (item) {

            if (item == null) return "";

            var num = new Date(parseInt(item.substr(6)));

            var parser = datetime("dd/MM/yyyy HH:mm:ss");
            parser.setDate(num);
            return parser.getText();
        }
    };

    /* transforma bool em texto Sim ou Não */
    function simnao() {
        return function (valor) {
            if (valor) return "Sim";

            return "Não";
        }
    }

    /* formata cpf */
    function cpfmask() {
        return function (num) {

            if (num === null || num === undefined || num === 0) return "";

            var item = (1e4 + "" + num).slice(-11);
            return item.toString().replace(/(\d\d\d)(\d\d\d)(\d\d\d)(\d\d)/, "$1.$2.$3-$4");
        }
    }
     
    /* formata cpf 00.000.000/0001-91 */
    function cnpjmask() {
        return function (num) {

            if (num === null || num === undefined || num === 0) return "";

            var item = new Array(14).concat([Math.abs(num)]).join('0').slice(-14);
            return item.toString().replace(/(\d\d)(\d\d\d)(\d\d\d)(\d\d\d\d)(\d\d)/, "$1.$2.$3/$4-$5");
        }
    }

    /* remove todas as máscaras */
    function removemask() {
        return function(maskednum) {
            return maskednum.toString().replace(/[^\d]+/g, '');
        }
    }

    angular
        .module("boipeba")
        .filter("leftpad", leftpad)
        .filter("simnao", simnao)
        .filter("cpfmask", cpfmask)
        .filter("cnpjmask", cnpjmask)
        .filter("removemask", removemask)
        .filter("jsdate", jsdate)
        .filter("jsdatetime", jsdatetime);
})();

