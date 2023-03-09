let scroller = new Scroller('/Reports/Find', '.elements', '#filter')
scroller.Start();

$('#clearFilter').click(function () {
    scroller.ResetData()
    return false;
});


async function Cancel(el) {
    let id = el.getAttribute("report-id");
    let type = el.getAttribute("report-type");
    let form = new FormData();
    form.append('id', id);
    form.append('reportType', type)
    let res = await fetch('/ReportsManager/CancelReport/', {
        method: 'POST',
        body: form
    })
    if (res.ok) {
        let elReplace = document.createElement('span')
        elReplace.innerHTML = 'Отчёт удалён'
        el.replaceWith(elReplace)
    }
}