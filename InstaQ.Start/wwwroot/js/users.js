let scroller = new Scroller('/Users/Users', '.elements', '#filter')
scroller.Start();

$('#clearFilter').click(function () {
    scroller.ResetData()
    return false;
});