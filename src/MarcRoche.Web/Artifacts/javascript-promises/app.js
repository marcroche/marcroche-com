var app_ns = app_ns || {};

var jQueryTimer = new app_ns.JQueryTimer(),
    qTimer = new app_ns.QTimer(),
    timerDiv = $('#timer'),
    timer2Div = $('#timer2'),
    messageDiv = $('#message');

jQueryTimer.startCountDown(5).progress(function (data) {
    timerDiv.text(data.time);
}).then(function () {
    timerDiv.text('Blast off!');
}).then(function () {
    qTimer.startCountDown(5).progress(function (data) {
        timer2Div.text(data.time);
    }).then(function () {
        timer2Div.text('Blast off!');
    }).then(function () {
        messageDiv.text('Both promises have been resolved.');
    });
});