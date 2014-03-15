
$(function() {
    $(this.textBox)
        .change(autoSize)
        .keydown(autoSize)
        .keyup(autoSize);
    autoSize();
});

function autoSize() {
    // Copy textarea contents; browser will calculate correct height of copy,
    // which will make overall container taller, which will make textarea taller.
    var text = $("#textArea").val().replace(/\n/g, '<br/>');
    $("#textCopy").html(text);
}