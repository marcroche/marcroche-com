$(function () {
    var pull = $('#menubutton');
    menu = $('#nav2');
   // menuHeight = menu.height();

    $(pull).on('click', function (e) {
        e.preventDefault();
        menu.slideToggle();
    });
});

$(window).resize(function () {
    var menu = $('#nav2');
    var w = $(window).width();
    if (w > 650 && !menu.is(':hidden')) {
        menu.removeAttr('style');
    }
});

$("#nav2 div").click(function () {
    window.location = $(this).find("a").attr("href");
    return false;
});