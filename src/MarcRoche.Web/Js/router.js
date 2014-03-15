define(['jQuery'], function ($) {

    var start = function () {

        var path = window.location.pathname;

        if (path === '/') {
            loadController('commentsController');
        }
    };

    var loadController = function (controllerName) {
        require(['controllers/' + controllerName], function (controller) {
            controller.start();
        });
    };

    var navigate = function (url) {
        window.location.href = url;
    };

    return {
        start: start,
        navigate: navigate
    };
});