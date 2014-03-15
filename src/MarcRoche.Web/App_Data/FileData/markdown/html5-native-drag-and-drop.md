#HTML5 Native Drag and Drop

<div>
	<time class="postinfo left-50 postdate">January 12, 2014</time>
</div>

Drag and drop has long been available in many of the JavaScript frameworks. However with HTML5 there is now native support.

Mostly, the latest versions of the major browsers support it about the same. I did find just a couple of minor tweeks that were needed to get Chrome, Firefox, and IE all working the same.

With this sample I have created a grid where the tiles can be dragged and interchanged. 

<div class="dnd-container">
	<div class="dtable">
		<div class="row">
			<div class="cell">
				<div class="tile" draggable="true">1</div>
			</div>
			<div class="cell">
				<div class="tile" draggable="true">2</div>
			</div>
			<div class="cell">
				<div class="tile" draggable="true">3</div>
			</div>
		</div>

		<div class="row">
			<div class="cell">
				<div class="tile" draggable="true">4</div>
			</div>
			<div class="cell">
				<div class="tile" draggable="true">5</div>
			</div>
			<div class="cell">
				<div class="tile" draggable="true">6</div>
			</div>
		</div>

		<div class="row">
			<div class="cell">
				<div class="tile" draggable="true">7</div>
			</div>
			<div class="cell">
				<div class="tile" draggable="true">8</div>
			</div>
			<div class="cell">
				<div class="tile" draggable="true">9</div>
			</div>
		</div>
	</div>
</div>

```JavaScript
var appNs = appNs || {};

appNs.MovingTiles = (function ($, animator) { 
    var self = this;
    var dragSrcEl;
    var sourceHtml;
    var destinationHtml;
    
    function handleDragStart(e) {
        e.dataTransfer.setData('Text', this.id); //Needed for firefox
        e.target.style.opacity = '0.4';
        dragSrcEl = e.target;
        sourceHtml = e.target.innerHTML;
    }

    function handleDragEnd(e) {
        $(e.target).fadeTo('slow', 1);
    }

    function handleDragEnter(e) {
        e.dataTransfer.getData('Text');
    }

    function handleDragOver(e) {
        if (e.preventDefault) {
            e.preventDefault();
        }
        $(e.target).parent().addClass('over');
        return false;
    }

    function handleDragLeave(e) {
        $(e.target).parent().removeClass('over');
    }

    function handleDrop(e) {
        if (e.preventDefault) {
            e.preventDefault();
        }
        if (e.stopPropagation) {
            e.stopPropagation();
        }

        $(e.target).parent().removeClass('over');

        if (e.target === dragSrcEl) {
            return;
        }

        var coords = getMoveToCoords(dragSrcEl, e.target);
        var coords2 = getMoveToCoords(e.target, dragSrcEl);

        move(dragSrcEl, coords.x, coords.y).then(function () {
            move(e.target, coords2.x, coords2.y).then(function () {

                var p1 = $(dragSrcEl).parent();
                var p2 = $(e.target).parent();
                $(dragSrcEl).detach();
                $(e.target).detach();

                $(dragSrcEl).removeAttr('style');
                $(e.target).removeAttr('style');

                var h1 = $(dragSrcEl).clone().wrap('<p>').parent().html();
                var h2 = $(e.target).clone().wrap('<p>').parent().html();

                $(dragSrcEl).remove();
                $(e.target).remove();

                var elem1 = $(h1);
                var elem2 = $(h2);

                attachEvents(elem1[0]);
                attachEvents(elem2[0]);
                elem1.appendTo(p2);
                elem2.appendTo(p1);


            }).then(function () {
                return false;
            });
        });
    }

    function move(element, x, y) {
        var self = this;
        var deferred = $.Deferred();
        $(this.elementSelector).removeAttr('draggable');
        animator().to(element, 1.0, {
            x: -x, y: -y, onComplete: function () {
                $(self.elementSelector).attr('draggable', 'true');
                deferred.resolve();
            }
        });

        return deferred.promise();
    }

    function getMoveToCoords(source, destination) {
        var TilePosition = function (source, destination) {
            this.x = $(source).position().left - $(destination).position().left;
            this.y = $(source).position().top - $(destination).position().top;
        };

        return new TilePosition(source, destination);
    }

    function attachEvents(col) {
        col.addEventListener('dragstart', handleDragStart, false);
        col.addEventListener('dragend', handleDragEnd, false);
        col.addEventListener('dragenter', handleDragEnter, false);
        col.addEventListener('dragover', handleDragOver, false);
        col.addEventListener('dragleave', handleDragLeave, false);
        col.addEventListener('drop', handleDrop, false);
    }

    var api = function (elementSelector) {
        self.elementSelector = elementSelector;

        var cols = document.querySelectorAll(self.elementSelector);
        [].forEach.call(cols, function (col) {
            attachEvents(col);
        });
    };

    return api;
})(jQuery, function () { return TweenLite });

appNs.tiles = new appNs.MovingTiles('.tile');
```

This was a fun little experiment. Next I would like to take this as foundation for dynamic UI's that can rearrange themselves or even a simple logic game.