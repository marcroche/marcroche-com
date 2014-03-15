#JavaScript Promises: Looking at jQuery and Q

<div>
	<time class="postinfo left-50 postdate">December 8, 2013</time>
</div>

Promises provide a method of programming asynchronously in javascript. Prior to promises the most common technique was the use of callbacks. And although callbacks worked fine, they were incredibly tough to follow and difficult to maintain.

JavaScript promises are formally defined by <a href="http://wiki.commonjs.org/wiki/Promises" target="_blank">CommonJS</a>. There are many implementations that have been developed, but two that you will see most often in both client and server side development are jQuery and Q implementations. *(jQuery on the client and Q in both client and server)*

Let's create a very basic scenario. 

We are waiting for a countdown to complete before we can continue further processing. The countdown gives us a 'promise' that it will let us know it is done and then blocks, and when the promise is resolved we can continue processing. <a href='/artifacts/javascript-promises/javascript-promises.html' target="_blank">Click here</a> for a live example.

The main application is pretty simple. We create our 'Timer Models' and we wait for processing to occur:

```JavaScript
var jQueryTimer = new app_ns.JQueryTimer(),
    qTimer = new app_ns.QTimer(),
    timerDiv = $('#timer'),
    timer2Div = $('#timer2'),
    messageDiv = $('#message');

jQueryTimer.startCountDown(5).progress(function (data) {
    timerDiv.text(data.time);
}).then(function () {
    timerDiv.text('Blast off!');
}).then(function() {
    qTimer.startCountDown(5).progress(function (data) {
        timer2Div.text(data.time);
    }).then(function () {
        timer2Div.text('Blast off!');
    }).then(function() {
        messageDiv.text('Both promises have been resolved.');
    });
});
```

The actual configuration of the promise happens in the models. We create the deffered object, return it to the caller, and then resolve it when the promise has been fulfilled. The notify method is only used to send progress updates.

The jQuery Model:

```JavaScript
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
  
  var api = function() {
    this.startCountDown = startCountDown;
  };
  
  return api;
})(jQuery);
```

And now the Q model which is identical in all but a couple of minor syntactical nuances:

```JavaScript
app_ns.QTimer = (function (Q) {
    var self = this;
    var currentTime = 0;

    var startCountDown = function (initialTime) {
        var deferred = Q.defer();
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

        return deferred.promise;
    };
  
  var api = function() {
    this.startCountDown = startCountDown;
  };
  
  return api;
})(Q);
```

That's about it for a basic, raw promise. In coming articles we will examine even more scenarios where promises can make our code cleaner and more concise.