angular.module('admin').controller('BlogCtrl', ['$scope', 'blogService',
    function ($scope, blogService) {
        $scope.selectedBlogPost = null;
        $scope.blogPostTitles = [];
        $scope.content = '';
        $scope.markdown = '';
        $scope.blogPost;

        blogService.getBlogTitles().then(function (data) {
            data.unshift('Select a Title');
            $scope.selectedBlogPost = data[0];
            $scope.blogPostTitles = data;
        });

        $scope.loadPost = function () {
            blogService.getBlogPost($scope.selectedBlogPost).then(function (data) {
                $scope.blogPost = data;
                $scope.content = data.htmlContent;
                $scope.markdown = data.content;
            });
        };
    }]);