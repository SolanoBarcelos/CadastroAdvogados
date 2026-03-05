function AdvogadoViewModel_AoCarregarComponente() {
    AdvogadoViewModel_FormatarCampos();

    console.log("Componente Advogado carregado com sucesso.");
}

function AdvogadoViewModel_FormatarCampos() {
    if ($("#Cep").length > 0) {
        $("#Cep").mask("99999-999");
    }
}

function AdvogadoViewModel_Excluir(pIntId) {
    if (confirm("Deseja realmente excluir este advogado?")) {
        $.post("/Advogado/Excluir", { pIntId: pIntId }, function (data) {
            if (data.sucesso) {
                alert("Excluído com sucesso!");
                location.reload();
            } else {
                alert("Erro ao excluir: " + data.erro);
            }
        });
    }
}