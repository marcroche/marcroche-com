function (ko, commentService, CommentForm) {

    var CommentViewModel = function (title) {
        var self = this;
        this.title = title;
        this.commentsList = ko.observableArray([]);
        this.commentForm = new CommentForm(title);
        this.commentForm.commentAdded.subscribe(function () {
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