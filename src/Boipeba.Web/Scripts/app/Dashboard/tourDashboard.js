(function () {
    var tour = new Tour({
        storage: false,
        template: "<div class='popover tour'><div class='arrow'></div><h3 class='popover-title'></h3><div class='popover-content'></div><div class='popover-navigation'><button class='btn btn-default' data-role='prev'>«</button><button class='btn btn-default' data-role='next'>»</button><button class='btn btn-default' data-role='end'>Fechar</button><div></div>"
    });

    tour.addSteps([
        {
            element: ".tour-ajuda",
            placement: "auto",
            backdrop: true,
            title: "Bem vindo ao SIGA",
            content: "<strong><p>Agradecemos a utilização.</p></strong>" +
                "<p>Esta ajuda interativa pode ser acionada neste botão indicado a qualquer momento. Utilize os botões de setas para navegar no tutorial ou 'fechar' para encerrar.</p>" +
                "<p>Caso possua alguma dúvida na utilização das funcionalidades, clique na Ajuda Interativa para obter dicas contextualizadas à pagina acessada.</p>"
        },
        {
            element: ".pesquisa-pessoafisica",
            placement: "top",
            backdrop: true,
            title: "Pesquisar Pessoa Física",
            content: "Aqui você consegue fazer buscas em nossas bases de pessoas físicas e gerar um extrato em PDF."
        }

    ]);

}());
