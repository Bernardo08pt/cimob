$(() => {
    $(".radio-group label").on("click", function (ev) {
        $(this).prev().prop("checked", true)
    });

    $("#btnCancelar").on("click", () => {
        $("#programModal").modal('hide');
    });

    $(document).on("keydown", function (e) {
        if (window.location.href.indexOf("ProgramasMobilidade") != -1 && e.keyCode == 27) {
            $("#programModal").modal('hide');
        }
    });

    /*Enviar o tipo de mobilidade e o período de estudos ou estágio*/
    $("#btnSubmit").on("click", (ev) => {
        ev.preventDefault();

        window.location = "/Application?tipo_mobilidade=" + $("input[type='radio']").val();
    });

    $(".programas-mobilidade a").on("click", (ev) => {
        var elem = ev.currentTarget;
        //se for erasmus temos que mostrar o modal para escolher se é período de estudos ou estágio
        if (elem.getAttribute("id") == "erasmus-modal") {
            $("#programModal").modal('show');
        } else {
            window.location = "/Application?tipo_mobilidade=" + elem.getAttribute("data-id")
        }
    });
});