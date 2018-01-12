$(document).ready(function () {
    $("#btnCancelar").click(function () {
        $("#programModal").modal('hide');
    });

    /*USA-SE ISTO TUDO SE FOR PARA TODOS TEREM O MODAL
    function openModal(tituloConteudo) {
        $("#programModal").modal('show');
        var titulo = document.getElementById('titulo');
        titulo.textContent = tituloConteudo;
    }

    /*Adicionei o click a cada button para mostrar o título respetivo
    $("#erasmus-modal").click(function () {
        openModal('Erasmus+')
    });

    $("#luso-modal").click(function () {
        openModal('Bolsas Luso-Brasileiras Santander Universidades')
    });

    $("#ibero-modal").click(function () {
        openModal('Bolsas Ibero-Americanas Santander Universidades')
    });

    $("#macau-modal").click(function () {
        openModal('Cooperação com o Instituto Politécnico de Macau')
    });

    $("#vasco-gama-modal").click(function () {
        openModal('Mobilidade Vasco da Gama')
    });*/



    /*Enviar o tipo de mobilidade e o período de estudos ou estágio*/
    $("#btnSubmit").on("click", (ev) => {
        ev.preventDefault();
        /*TODO: no estágio está a dar undefined*/
        window.location = "/Application?tipo_mobilidade=tipo_mobilidade&estagio= " +
            $("input[type='radio']").val() +"'";
    });

    /*$("#erasmus-modal").click(function () {
        $("#programModal").modal('show');
    });*/
    $(".main-div a").on("click", (ev) => {
        var elem = ev.currentTarget;
        // vês se é erasmus 
        // se for abres o popup e fazes
        if (elem.getAttribute("id") == "erasmus-modal") {
          $("#programModal").modal('show');
        } else {

            // senão
            window.location = "/Application?tipo_mobilidade=" + elem.getAttribute("data-id")
        }

    })

});