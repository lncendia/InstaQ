let rangeInput = $(".range-input");
rangeInput.each((i, obj)=>{
    let jobj = $(obj)
    let input = jobj.children('input')
    let value = jobj.children('.value').children('div')

    let start = parseFloat(input.attr('min'));
    let end = parseFloat(input.attr('max'));
    let step = parseFloat(input.attr('step'));
    for(let i=start;i<=end;i+=step){
        value.html(value.html() + '<div>'+i+'</div>');
    }
    input.on("input",function(){
        let top = (parseFloat(input.val())/step -1) * -40;
        value.css('margin-top', top)
    });
})