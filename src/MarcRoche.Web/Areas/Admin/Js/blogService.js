angular.module('admin')
    .factory('blogService', ['$http', '$q',
        function ($http, $q) {
            return {
                getBlogTitles: function () {
                    var deferred = $q.defer();
                    $http({
                        method: 'GET',
                        url: '/api/admin/blog/titles'
                    }).success(function (result) {
                        deferred.resolve(result);
                    }).error(function (err) {
                        deferred.reject(err);
                    });
                    return deferred.promise;
                },

                getBlogPost: function (title) {
                    var deferred = $q.defer();
                    $http({
                        method: 'GET',
                        url: '/api/admin/blog/' + title
                    }).success(function (result) {
                        deferred.resolve(result);
                    }).error(function (err) {
                        deferred.reject(err);
                    });
                    return deferred.promise;
                }
            }
        }]);