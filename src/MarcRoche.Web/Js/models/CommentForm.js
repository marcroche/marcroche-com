define(['knockout', 'services/commentService'], function (ko, commentService) {
    var CommentForm = function (title) {
        var self = this;
        self.title = ko.observable(title);
        self.author = ko.observable().extend({ required: true, maxLength: 50 });
        self.content = ko.observable().extend({ required: true, maxLength: 500 });
        self.email = ko.observable().extend({ required: true, maxLength: 50, email: true });
        self.homePage = ko.observable();
        self.showWaiting = ko.observable(false);

        self.commentAdded = ko.observable();
        self.showContentError = ko.observable(false);
        self.showAuthorError = ko.observable(false);
        self.showEmailError = ko.observable(false);
        self.showErrorMessage = ko.observable(false);
    };

    CommentForm.prototype.createComment = function () {
        var self = this;
        var errors = 0;
        if ((self.content() || '').replace(' ', '') === '') {
            self.showErrorMessage(true);
            errors++;
        }
        if ((self.author() || '').replace(' ', '') === '') {
            self.showErrorMessage(true);
            errors++;
        }
        if ((self.email() || '').replace(' ', '') === '') {
            self.showErrorMessage(true);
            errors++;
        }
        if (errors > 0) {
            return;
        }
        self.showErrorMessage(false);
        //self.errors.showAllMessages();
        //if (self.errors().length !== 0) {
        //    return;
        //}
        var self = this;
        self.showWaiting(true);

        var dtoComment = {
            title: self.title(),
            comment: {
                author: self.author(),
                content: self.content(),
                email: self.email(),
                homePage: self.homePage()
            }
        };

        return commentService.createComment(dtoComment).then(function (data) {
            self.showWaiting(false);
            self.commentAdded(new Date);
            self.reset();
        });

    };

    CommentForm.prototype.reset = function () {
        var self = this;
        self.author('');
        self.content('');
        self.email('');
        self.homePage('');
    }

    return CommentForm;
});


//TODO: Move to snippet
//'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function (c) {
//    var r = Math.random() * 16 | 0, v = c == 'x' ? r : (r & 0x3 | 0x8);
//    return v.toString(16);
//});