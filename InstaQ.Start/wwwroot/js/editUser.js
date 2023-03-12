let sender = new AsyncSender('#message');
$(document).ready(() => {
    sender.Add('#editForm', "Данные успешно изменены")
    sender.Add('#changePassword', "Пароль успешно изменён")
    sender.Add('#changeBalance', "Баланс успешно изменён")
})
