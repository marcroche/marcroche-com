define(['knockout', 'jQuery', 'text!views/comments.html', 'models/CommentViewModel'],
    function (ko, $, commentsView, CommentsViewModel) {

        function start() {
            $("#comments").empty();
            var element = $(commentsView).appendTo("#comments");

            var title = $('#comments').attr('title').toLowerCase().replace(' ', '-');
            var viewModel = new CommentsViewModel(title);

            ko.applyBindings(viewModel, element[0]);
            viewModel.getCurrentComments();
        }

        return {
            start: start
        };
    });