requirejs.config(
    {
        paths: {
            'jQuery': '../vendor/jquery',
            'underscore': '../vendor/underscore',
			'd3' : '../vendor/d3',
            "Q": "../vendor/q",
        },
        shim: {
            'jQuery': {
                exports: '$'
            },
            'underscore': {
                exports: '_'
            }
        }
    }
);

require(['bootstrapper'], function (bootstrapper) {
	bootstrapper.start();
});