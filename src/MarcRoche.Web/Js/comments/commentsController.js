app.controller('CommentsCtrl', ['$scope', 'commentsService', function ($scope, commentsService) {
    var comment = new marcroche_blog.Comment();
    $scope.blogTitle = document.getElementById('blogTitle').innerHTML;
    $scope.newComment = comment;
    $scope.comments = [];
    commentsService.getComments($scope.blogTitle).then(function (data) {
        if (data !== null) {
            _.each(data, function (obj) {
                obj.date = moment(obj.date).format('MMM Do, YYYY');
            });
            $scope.comments = data;
        }
    });

    $scope.newCommentIsInvalid = false;

    $scope.createComment = function (form) {
        console.log("Creating comment");
        console.log("Author: " + $scope.newComment.author);

        if (form.$valid === false) {
            $scope.newCommentIsInvalid = true;
        } else {
            var dtoComment = {
                title: $scope.blogTitle,
                comment: {
                    author: $scope.newComment.author,
                    content: $scope.newComment.content,
                    email: $scope.newComment.email,
                    homePage: 'homePage'
                }
            };
            commentsService.createComment(dtoComment).then(function () {
                //self.showWaiting(false);
                //self.commentAdded(new Date);
                //self.reset();
            });
        }
    };
}]); 