app.directive('comments', function () {
    return {
        restrict: 'AE',
        replace: true,
        templateUrl: 'Js/views/comments.html' //,
        //link: function (scope, elem, attrs) {
        //    elem.bind('click', function () {
        //        elem.css('background-color', 'white');
        //        scope.$apply(function () {
        //            scope.color = "white";
        //        });
        //    });
        //    elem.bind('mouseover', function () {
        //        elem.css('cursor', 'pointer');
        //    });
        //}
    };
});