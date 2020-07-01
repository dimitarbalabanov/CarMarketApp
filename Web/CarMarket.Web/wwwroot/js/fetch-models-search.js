function fetchModels(id) {
    $.get(`/api/models/${id}`, function (data) {
        const options = data.map(x => `<option value=${x.id}>${x.name}</option>`);
        $('#modelSelect').empty().append(`<option>Any model</option>`).append(options);
    });
}