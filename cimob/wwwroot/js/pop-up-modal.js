$(document).ready(function () {
    $("#btnCancelar").click(function () {
        $("#programModal").modal('hide');
    });

    function openModal(tituloConteudo) {
        $("#programModal").modal('show');
        var titulo = document.getElementById('titulo');
        titulo.textContent = tituloConteudo;
    }

    /*Adicionei o click a cada button para mostrar o título respetivo*/
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
    });

});