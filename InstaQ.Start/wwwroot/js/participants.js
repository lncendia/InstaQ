let scroller = new Scroller('/Participants/Participants', '.elements', '#filter')
scroller.Start();

$('#clearFilter').click(function () {
    scroller.ResetData()
    return false;
});

