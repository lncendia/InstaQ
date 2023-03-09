let sender = new AsyncSender('#message');
$(document).ready(() => {
    sender.Add('#changeName', "Имя успешно изменено")
    sender.Add('#changeEmail', "Запрос отправлен вам на почту")
    sender.Add('#changePassword', "Пароль успешно изменен")
    sender.Add('#changeTarget', "Цель успешно изменена")
    sender.Add('#createLink', "Связь успешно создана", LinkCreated)
    sender.Add('#createPayment', "Счёт выставлен", PaymentCreated)
})

let links = $('#links')
let payments = $('#payments')

function LinkCreated(json) {
    let data = JSON.parse(json);
    links.after('<tr id="' + data.id + '"><td>' + data.name1 + '</td>' +
        '<td>' + data.name2 + '</td>' +
        '    <td>' +
        '        <span class="text-warning">В ожидании</span>' +
        '    </td>' +
        '<td>' +
        '    <a class="action" href="#" link-id="' + data.id + '" onclick="DeleteLink(this)">' +
        '        <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-trash" viewBox="0 0 16 16">' +
        '            <path d="M5.5 5.5A.5.5 0 0 1 6 6v6a.5.5 0 0 1-1 0V6a.5.5 0 0 1 .5-.5zm2.5 0a.5.5 0 0 1 .5.5v6a.5.5 0 0 1-1 0V6a.5.5 0 0 1 .5-.5zm3 .5a.5.5 0 0 0-1 0v6a.5.5 0 0 0 1 0V6z"/>' +
        '            <path fill-rule="evenodd" d="M14.5 3a1 1 0 0 1-1 1H13v9a2 2 0 0 1-2 2H5a2 2 0 0 1-2-2V4h-.5a1 1 0 0 1-1-1V2a1 1 0 0 1 1-1H6a1 1 0 0 1 1-1h2a1 1 0 0 1 1 1h3.5a1 1 0 0 1 1 1v1zM4.118 4 4 4.059V13a1 1 0 0 0 1 1h6a1 1 0 0 0 1-1V4.059L11.882 4H4.118zM2.5 3V2h11v1h-11z"/>' +
        '        </svg>' +
        '    </a>' +
        '</td></tr>')
}

function PaymentCreated(json) {
    let data = JSON.parse(json);
    payments.after('<tr><td>' + data.creationDate + '</td>' +
        '<td>' + data.amount + '</td>' +
        '    <td>' +
        '        <a class="badge bg-danger" href="#" payment-id="' + data.id + '" onclick="Check(this)">Проверить оплату</a>' +
        '    </td>' +
        '    <td>' +
        '        <a class="action" href="' + data.payUrl + '">Перейти</a>' +
        '    </td></tr>')
}

async function Check(el) {
    let id = el.getAttribute("payment-id");
    let form = new FormData();
    form.append('id', id);
    let res = await fetch('/Payment/Check', {
        method: 'POST',
        body: form
    })
    if (res.ok) {
        let elReplace = document.createElement('span')
        elReplace.className = 'badge bg-success'
        elReplace.innerHTML = 'Оплачено'
        el.replaceWith(elReplace)
    } else ShowMessage(await res.text())
}


async function DeleteLink(el) {
    let id = el.getAttribute("link-id");
    let form = new FormData();
    form.append('id', id);
    let res = await fetch('/Link/Delete', {
        method: 'POST',
        body: form
    })
    if (res.ok) {
        $('#' + id).remove()
    } else ShowMessage(await res.text())
}


async function AcceptLink(el) {
    let id = el.getAttribute("link-id");
    let form = new FormData();
    form.append('id', id);
    let res = await fetch('/Link/Accept', {
        method: 'POST',
        body: form
    })
    if (res.ok) {
        $(el).remove()
        let stats = $('#stats' + id)
        let elReplace = document.createElement('span')
        elReplace.className = 'badge bg-success'
        elReplace.innerHTML = 'Активна'
        stats.replaceWith(elReplace)
    } else ShowMessage(await res.text())
}


let message = $('#message');

function ShowMessage(error) {
    message.empty()
    message.html('<div class="alert alert-danger alert-dismissible fade show" role="alert">' + error + '<button type="button" class="btn-close" data-bs-dismiss="alert"></button></div>')
}