let sender = new AsyncSender('#message');
$(document).ready(() => {
    sender.Add('#edit', "Участник изменён")
    $('#select').select2();
})