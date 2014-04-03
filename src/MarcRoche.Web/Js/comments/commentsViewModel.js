function (ko, commentService, CommentForm) {

    var CommentViewModel = function (title) {
        var self = this;
        self.title = title;
        self.commentsList = ko.observableArray([]);
        self.commentForm = new CommentForm(title);
        self.commentForm.commentAdded.subscribe(function () {
            return self.getCurrentComments();
        });

    };

    CommentViewModel.prototype.getCurrentComments = function () {
        var self = this;
        return commentService.getComments(self.title).then(function (data) {
            self.commentsList(data);
        });
    };

    return CommentViewModel;
}