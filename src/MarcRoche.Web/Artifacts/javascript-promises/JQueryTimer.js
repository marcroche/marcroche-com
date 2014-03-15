var app_ns = app_ns || {};

app_ns.JQueryTimer = (function ($) {
    var self = this;
    var currentTime = 0;

    var startCountDown = function (initialTime) {
        var deferred = $.Deferred();
        currentTime = initialTime;

        var counter = setInterval(function () {
            deferred.notify({
                time: currentTime
            });

            currentTime = currentTime - 1;

            if (currentTime < 0) {
                clearInterval(counter);
                deferred.resolve();
            }
        }, 1000);

        return deferred.promise();
    };

    var api = function () {
        this.startCountDown = startCountDown;
    };

    return api;
})(jQuery);