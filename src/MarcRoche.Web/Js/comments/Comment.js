var marcroche_blog = marcroche_blog || {};

marcroche_blog.Comment = function () {
    this.author;
    this.email;
    this.content;

    this.isValid = function () {
        return validate(this.author) && validate(this.email) && validate(this.content);
    };

    function validate(field) {
        return field !== undefined && field !== null && field.length > 0;
    }
};