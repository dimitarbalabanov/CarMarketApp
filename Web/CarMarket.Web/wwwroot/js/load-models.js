$('#makeModel').on('change', function () {
    if ($('#makeModel').val() != "") {
        fetchModels(this.value);
    }
});

$(window).on('load', function () {
    if ($('#makeModel').val() != "") {
        fetchModels($('#makeModel').val());
    }
});