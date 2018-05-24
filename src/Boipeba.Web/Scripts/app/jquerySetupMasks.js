
(function () {
    

    $(document).ready(function () {
        $('.input-group.date').datepicker({
            format: 'dd/mm/yyyy',
            language: 'pt-BR',
            autoclose: true,
            startView: 3
        });
    });


    $('.cpf').mask('000.000.000-00');

    $('.cnpj').mask('00.000.000/0000-00');

    $('.data-br').mask('00/00/0000');

    $('.simp').mask('000.0.0NNNNN/0000', {
        'translation': {
            N: { pattern: /[0-9]/, optional: true },
        }
    });

    $('.telefone').mask('(00) 0000-0000', {
        'translation': {
            N: { pattern: /[0-9]/, optional: true },
        }
    });

    var maskBehavior = function (val) {
        return val.replace(/\D/g, '').length === 11 ? '(00) 00000-0000' : '(00) 0000-00000';
    },
    options = {
        onKeyPress: function (val, e, field, options) {
            field.mask(maskBehavior.apply({}, arguments), options);
        }
    };

    $('.celular').mask(maskBehavior, options);

    $('.placa').mask('NNN-0000', {
        'translation': {
            N: { pattern: /[a-zA-Z]/ },
        }
    });
})();
