$(document).ready(function () {
    $("#btnCancelar").click(function () {
        $("#programModal").modal('hide');
    });

    /*Enviar o tipo de mobilidade e o período de estudos ou estágio*/
    $("#btnSubmit").on("click", (ev) => {
        ev.preventDefault();
        window.location = "/Application?tipo_mobilidade=tipo_mobilidade&estagio= " +
            $("input[type='radio']").val() +"'";
    });

    $(".main-div a").on("click", (ev) => {
        var elem = ev.currentTarget;
        //se for erasmus temos que mostrar o modal para escolher se é período de estudos ou estágio
        if (elem.getAttribute("id") == "erasmus-modal") {
          $("#programModal").modal('show');
        } else {
            window.location = "/Application?tipo_mobilidade=" + elem.getAttribute("data-id")
        }

    })

});