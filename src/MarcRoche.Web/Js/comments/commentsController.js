app.controller("CommentsCtrl", ['$scope', 'commentsService', function ($scope, commentsService) {
    var comment = new marcroche_blog.Comment();

    $scope.newComment = comment;
    commentsService.getComments().then(function (data) {
        if (data === null) {
            $scope.comments = [];
        } else {
            $scope.comments = data;
        }
    });

    $scope.newCommentIsInvalid = false;

    $scope.createComment = function (form) {

        if (form.$valid === false) {
            $scope.newCommentIsInvalid = true;
        } else {
            commentsService.createComment($scope.newComment);
        }

        console.log("Creating comment");
        console.log("Author: " + $scope.newComment.author);
    };
}]); 