
requirejs.config(
    {
        paths: {
            'text': '../Scripts/text',
            'jQuery': '../Scripts/jquery-1.10.2.min',
            'underscore': '../Scripts/underscore.min',
            'knockout': '../Scripts/knockout-3.0.0',
            'ko.mapping': '../Scripts/knockout.mapping-latest',
            'knockout.validation': '../Scripts/knockout.validation',
        },
        shim: {
            'jQuery': {
                exports: '$'
            },
            'underscore': {
                exports: '_'
            },
            'knockout': {
                exports: 'ko'
            },
            'ko.mapping': {
                deps: ['knockout'],
                exports: 'ko.mapping'
            },
            'knockout.validation': {
                deps: ["knockout"]
            }
        }
    }
);

require(['router', 'knockout'], function (router, ko) {
    window.ko = ko;
    //require(['knockout.validation'], function () {

        //knockout_validation.configure({
        //    decorateElement: true
        //});
        router.start();
        //ko.applyBindings({});
    //});
});