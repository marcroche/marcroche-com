var app = angular.module('angular-manager', ["ngAnimate", "ngRoute"]);

app.config(['$routeProvider', function ($routeProvider) {
    $routeProvider.when('/', {
        templateUrl: '/Demos/AngularManager/Pages/Home.html',
        controller: 'HomeCtrl'
    });
    $routeProvider.when('/1', {
        templateUrl: '/Demos/AngularManager/Pages/Page1.html',
        controller: 'Page1Ctrl'
    });
    $routeProvider.when('/2', {
        templateUrl: '/Demos/AngularManager/Pages/Page2.html',
        controller: 'Page2Ctrl'
    });
    $routeProvider.otherwise({ redirectTo: '/' });
}]);

app.controller('HomeCtrl', function ($scope) {
    $scope.firstName = "Marc";
});

app.controller('Page1Ctrl', function ($scope) {
    $scope.firstName = "Joe";
});

app.controller('Page2Ctrl', function ($scope) {
    $scope.firstName = "Buddy";
});