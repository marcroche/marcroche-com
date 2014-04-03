app.controller("CommentsCtrl", function ($scope) {
    var comment = new marcroche_blog.Comment();
    $scope.message = "This is some angular shit";

    $scope.newComment = comment;

    $scope.newCommentIsInvalid = true;

    $scope.createComment = function (form) {

        if (form.$valid === false) {
            $scope.newCommentIsInvalid = true;
        }

        console.log("Creating comment");
        console.log("Author: " + $scope.newComment.author);
    };
}); 