
(function () {

    $(document).ajaxError(function (xhr, props) {
        if (props.status === 401) {
            alert('Sessão Expirada. Você será redirecionado para tela de entrada do sistema. Por favor, acesse novamente com seu usuário e senha.');
            location.reload();
        }

        //alert('Erro: ' + props.status);
    });

    $.fn.rte = function () {
        var that = this;
        this.find('th').each(function (index) {
            index++;
            that.find('tr td:nth-child(' + index + ')').attr('data-title', that.find('th:nth-child(' + index + ')').text());
        });
    }

    $.validator.setDefaults({
        highlight: function (element) {
            $(element).closest('.form-group').addClass('has-error');
        },
        unhighlight: function (element) {
            $(element).closest('.form-group').removeClass('has-error');
        },
        errorElement: 'span',
        errorClass: 'help-block',
        errorPlacement: function (error, element) {
            if (element.parent('.input-group').length) {
                error.insertAfter(element.parent());
            } else {
                error.insertAfter(element);
            }
        }
    });

    $.validator.addMethod("validateCPF", function (value) {
        if (value.length == 0) return true;
        value = value.replace('.', '');
        value = value.replace('.', '');
        cpf = value.replace('-', '');
        while (cpf.length < 11) cpf = "0" + cpf;
        var expReg = /^0+$|^1+$|^2+$|^3+$|^4+$|^5+$|^6+$|^7+$|^8+$|^9+$/;
        var a = [];
        var b = new Number;
        var c = 11;
        for (i = 0; i < 11; i++) {
            a[i] = cpf.charAt(i);
            if (i < 9) b += (a[i] * --c);
        }
        if ((x = b % 11) < 2) { a[9] = 0 } else { a[9] = 11 - x }
        b = 0;
        c = 11;
        for (y = 0; y < 10; y++) b += (a[y] * c--);
        if ((x = b % 11) < 2) { a[10] = 0; } else { a[10] = 11 - x; }
        if ((cpf.charAt(9) != a[9]) || (cpf.charAt(10) != a[10]) || cpf.match(expReg)) return false;
        return true;
    }, "O CPF informado é inválido.");

    $.validator.addMethod("validateCNPJ", function (cnpj) {
       
        cnpj = cnpj.replace(/[^\d]+/g, '');

        if (cnpj == '') return true;

        if (cnpj.length != 14)
            return false;

        // Elimina CNPJs invalidos conhecidos
        if (cnpj == "00000000000000" ||
            cnpj == "11111111111111" ||
            cnpj == "22222222222222" ||
            cnpj == "33333333333333" ||
            cnpj == "44444444444444" ||
            cnpj == "55555555555555" ||
            cnpj == "66666666666666" ||
            cnpj == "77777777777777" ||
            cnpj == "88888888888888" ||
            cnpj == "99999999999999")
            return false;

        // Valida DVs
        tamanho = cnpj.length - 2
        numeros = cnpj.substring(0, tamanho);
        digitos = cnpj.substring(tamanho);
        soma = 0;
        pos = tamanho - 7;
        for (i = tamanho; i >= 1; i--) {
            soma += numeros.charAt(tamanho - i) * pos--;
            if (pos < 2)
                pos = 9;
        }
        resultado = soma % 11 < 2 ? 0 : 11 - soma % 11;
        if (resultado != digitos.charAt(0))
            return false;

        tamanho = tamanho + 1;
        numeros = cnpj.substring(0, tamanho);
        soma = 0;
        pos = tamanho - 7;
        for (i = tamanho; i >= 1; i--) {
            soma += numeros.charAt(tamanho - i) * pos--;
            if (pos < 2)
                pos = 9;
        }
        resultado = soma % 11 < 2 ? 0 : 11 - soma % 11;
        if (resultado != digitos.charAt(1))
            return false;

        return true;

    }, "O CNPJ informado é inválido.");

    $.validator.addMethod("validateDate", function(value, element) {
        if (value.length == 0) return true;
        var check = false;
        var re = /^\d{1,2}\/\d{1,2}\/\d{4}$/;
        if (re.test(value)) {
            var adata = value.split('/');
            var gg = parseInt(adata[0], 10);
            var mm = parseInt(adata[1], 10);
            var aaaa = parseInt(adata[2], 10);

            if (aaaa < 1888 || aaaa > 2100)
                return false;

            var xdata = new Date(aaaa, mm - 1, gg);
            if ((xdata.getFullYear() == aaaa) && (xdata.getMonth() == mm - 1) && (xdata.getDate() == gg))
                check = true;
            else
                check = false;
        } else
            check = false;
        return this.optional(element) || check;
    }, "A data informada é inválida.");  // Mensagem padrão

    jQuery.validator.addMethod("greaterThan",
        // new Date(year, month [, day [, hours[, minutes[, seconds[, ms]]]]]) // Note: months are 0-based
        function (value, element, params) {
            var iniciostr = $(params).val();
            var parts = iniciostr.split('/');
            var inicio = new Date(parts[2], parts[1] - 1, parts[0]); 
            var parts2 = value.split('/');
            var fim = new Date(parts2[2], parts2[1] - 1, parts2[0]);

            if (value == "") return true;

            if (parts.length == 1) {
                return parseInt(value) >= parseInt(iniciostr);
            }

            return fim > inicio;
            
        }, 'Deve ser maior que a data inicial.');

        jQuery.validator.addMethod("PrazoMinimo",
        // new Date(year, month [, day [, hours[, minutes[, seconds[, ms]]]]]) // Note: months are 0-based
        function (value, element, params) {
            var iniciostr = $(params).val();
            var parts = iniciostr.split('/');
            var inicio = new Date(parts[2], parts[1] - 1, parts[0]); 
            var parts2 = value.split('/');
            var fim = new Date(parts2[2], parts2[1] - 1, parts2[0]);

            if (value == "") return true;

            if (parts.length == 1) {
                return parseInt(value) >= parseInt(iniciostr);
            }

            return fim >= inicio;
            
        }, 'O prazo mínimo são 2 dias úteis. A complexidade da solicitação pode estender este prazo.');

    jQuery.validator.addClassRules("cpf", {
        validateCPF: true
    });

    jQuery.validator.addClassRules("cnpj", {
        validateCNPJ: true
    });

    jQuery.validator.addClassRules("data-br", {
        validateDate: true
    });

    jQuery.validator.addMethod("valueNotEquals", function (value, element, arg) {
        return arg != value;
    }, "Selecione valor.");

    function scrollTo(id) {
        $('html,body').animate({ scrollTop: $("#" + id).offset().top }, 'slow');
    }
    
    $.extend(jQuery.validator.messages, {
        required: "Campo obrigatório.",
        //remote: "Please fix this field.",
        email: "O e-mail informado é inválido.",
        //date: "Please enter a valid date.",
        //dateISO: "Please enter a valid date (ISO).",
        //number: "O número informado é inválido.",
        digits: "Campo numérico.",
        //creditcard: "Please enter a valid credit card number.",
        //equalTo: "Please enter the same value again.",
        //accept: "Please enter a value with a valid extension.",
        maxlength: jQuery.validator.format("Please enter no more than {0} characters."),
        minlength: jQuery.validator.format("Informe pelo menos {0} caracteres."),
        //rangelength: jQuery.validator.format("Please enter a value between {0} and {1} characters long."),
        //range: jQuery.validator.format("Please enter a value between {0} and {1}."),
        //max: jQuery.validator.format("Please enter a value less than or equal to {0}."),
        //min: jQuery.validator.format("Please enter a value greater than or equal to {0}.")
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
