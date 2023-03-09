let sender = new AsyncSender('#message');
$(document).ready(() => {
    $('#participantForm').validate();
    sender.Add('#likeForm', "Отчёт успешно создан")
    sender.Add('#commentForm', "Отчёт успешно создан")
    sender.Add('#participantForm', "Отчёт успешно создан")
})
