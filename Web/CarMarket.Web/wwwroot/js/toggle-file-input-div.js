$('.form-check-input').on('change', function () {
    $(this).parent().parent().children().last().toggle();
});