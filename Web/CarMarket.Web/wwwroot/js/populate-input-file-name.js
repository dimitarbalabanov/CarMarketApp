$('.custom-file-input').on('change', function () {
    const fileName = event.target.files[0].name;
    $(this).next('.custom-file-label').addClass("selected").html(fileName);
});