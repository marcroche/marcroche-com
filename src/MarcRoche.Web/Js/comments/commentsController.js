app.controller("CommentsCtrl", function ($scope) {
    var comment = new marcroche_blog.Comment();

    $scope.newComment = comment;
    $scope.comments = [];

    $scope.newCommentIsInvalid = false;

    $scope.createComment = function (form) {

        if (form.$valid === false) {
            $scope.newCommentIsInvalid = true;
        }

        console.log("Creating comment");
        console.log("Author: " + $scope.newComment.author);
    };
}); 