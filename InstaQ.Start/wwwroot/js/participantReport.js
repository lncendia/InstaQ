let scroller = new Scroller('/Elements/ParticipantReportElements', '.elements', '#filter')
scroller.Start();

$('#clearFilter').click(function () {
    scroller.ResetData()
    return false;
});