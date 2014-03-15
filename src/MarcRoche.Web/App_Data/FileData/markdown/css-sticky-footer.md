#CSS Sticky Footer

<div>
	<time class="postinfo left-50 postdate">November 19, 2013</time>
</div>

A common problem with page layouts is a footer section that does not fill to the bottom of the screen. I find this mostly happens when there is not enough content in the main section to force the layout's height to exceed the window's height. 

However with CSS we can have the footer 'stick' to the bottom of the browser window even when it normally would not.

<a href="/artifacts/css-sticky-footer/css-sticky-footer.html" target="_blank">Click here</a> for an example.

And here is the HTML:

```xml
<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1">
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1">
    <title>CSS Sticky Footer</title>
    <style>
        body {
            font: 14px/1.5 Lato, "Helvetica Neue", Helvetica, Arial, sans-serif;
            color: #333333;
            background-color: #777777;
            margin: 0;
            padding: 0;
        }

        header {
            display: block !important;
            background-color: #f3f3f3;
            height: 38px;
            border-bottom: 2px solid #333333;
        }

        .page-wrapper {
            background-color: #FFFFFF;
        }

        footer {
            border-top: 3px solid #000000;
            background-color: #777777;
            color: #FFFFFF;
            position: relative;
        }

        .fixed-width-container {
            width: 860px;
            margin: 0 auto;
            padding-left: 15px;
            padding-right: 15px;
        }

        h1, h2 {
            font: 20px arial,sans-serif;
            margin: 0;
        }
    </style>
</head>
<body>
    <div class="page-wrapper">
        <header>
            <div class="fixed-width-container">
                <h2>This is the header</h2>
            </div>
        </header>
        <article>
            <div class="fixed-width-container">
                <h1>This is the content</h1>
                <p>Content, content, content.</p>
                <p>Content, content, content.</p>
                <p>Content, content, content.</p>
                <p>Content, content, content.</p>
                <p>Content, content, content.</p>
                <p>Content, content, content.</p>
            </div>
        </article>
        <footer>
            <div class="fixed-width-container">
                <h2>This is the footer</h2>
            </div>
        </footer>
    </div>
</body>
</html>
```