//$('#submitComment').click(function () {
//    var comment = new MrBlogNs.BlogComment();
//    comment.author;
//    comment.content = $('#commentContent').text();
//    comment.date = 'moment';
//    comment.email = 'blah@blah.com';
//    comment.homePage = 'www.blah.com';
//    comment.isModerated = false;

//    var request = $.ajax({
//        url: "/api/comments",
//        type: "POST",
//        data: JSON.stringify({ title: $('#blogTitle').text(), blogComment: comment }),
//        contentType: 'application/json; charset=utf-8',
//        dataType: 'json'
//    });

//    request.done(function (msg) {
//        //$("#log").html(msg);
//    });

//    request.fail(function (jqXHR, textStatus) {
//        //alert("Request failed: " + textStatus);
//    });
//});

//var request = $.ajax({
//    url: "/api/comments/" + $('#blogTitle').text(),
//    type: "GET",
//    contentType: 'application/json; charset=utf-8',
//    dataType: 'json'
//});

//request.done(function (data) {
//    var myData = data;
//});

//request.fail(function (jqXHR, textStatus) {
//    //alert("Request failed: " + textStatus);
//});