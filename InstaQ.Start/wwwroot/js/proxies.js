let scroller = new Scroller('/Proxy/Proxies', '.elements', '#filter')
let sender = new AsyncSender('#message');

$(document).ready(() => {
    scroller.Start();
    sender.Add('#proxiesForm', "Прокcи успешно загружены")
})


async function Delete(el) {
    let id = el.getAttribute("proxy-id");
    let form = new FormData();
    form.append('id', id);
    let res = await fetch('/Proxy/Delete', {
        method: 'POST',
        body: form
    })
    if (res.ok) {
        $('#' + id).remove()
    } else ShowMessage(await res.text())
}


let message = $('#message');

function ShowMessage(error) {
    message.empty()
    message.html('<div class="alert alert-danger alert-dismissible fade show" role="alert">' + error + '<button type="button" class="btn-close" data-bs-dismiss="alert"></button></div>')
}

$('#clearFilter').click(function () {
    scroller.ResetData()
    return false;
});