var appNs = appNs || {};

appNs.TimeSeriesChart = function (height, container, elementCount) {
    var self = this;
    var margin = { top: 0, right: 00, bottom: 00, left: 0 };
    var w = parseInt(d3.select(container).style('width'), 10);
    w = w - margin.left - margin.right;
    var h = height;
    var hColor = 0;
    d3.select(window).on('resize', resize);
    
    self.data = [];
    self.key = 0;
    self.cnt = 0;
    self.hColor = 0;
    self.isRunning = true;
    self.DataPoint = function (key, value) {
        this.key = key;
        this.value = value;
    };
    self.initializeData(elementCount);
    self.scaleX = d3.scale.linear()
		.domain([0, 1])
		.range([0, w]);
    self.scaleY = d3.scale.linear()
		.domain([0, 100])
		.rangeRound([0, h]);
    self.svg = d3.select(container)
		.append("svg")
		.attr("width", w)
		.attr("height", h)
        .attr("preserveAspectRatio", "xMinYMin meet")
        .attr("viewBox", "0 0 " + w + " 50")
        .attr("class", "svg-content");
    self.svg.selectAll("rect")
		.data(self.data)
		.enter()
		.append("rect")
		.attr("x", function (obj, index) {
		    return index * (w / elementCount);
		})
		.attr("y", 0)
		.attr("width", w / elementCount - 1.5)
		.attr("height", function (obj) {
		    return obj.value * 10;
		});

    self.counter = setInterval(runner, 150);

    function runner() {
        for (var i = self.data.length - 1; i > 0; i--) {
            self.data[i].value = self.data[i - 1].value;
        }
        self.data[0].value = self.getData(self.cnt++ / 3);

        self.svg.selectAll("rect")
            .data(self.data)
            .transition()
            .duration(150)
            .attr("x", function (obj, index) {
                return index * (w / elementCount);
            })
            .attr("y", function (d) {
                return h - self.scaleY(d.value * 10) - 0.5;
            })
            .attr("width", w / elementCount - 1.5)
            .attr("height", function (d) {
                return self.scaleY(d.value * 10);
            })
            .attr("fill", function (a, b) {
                self.hColor + 0.1 > 360 ? self.hColor = 0 : self.hColor += 0.1;
                return "hsl(" + self.hColor + ", 75%, 25%)";
            });
    }

    function resize() {
        w = parseInt(d3.select(container).style('width'), 10);
        w = w - margin.left - margin.right;

        self.scaleX.range([0, w]);

        self.svg
            .attr("width", w)
            .attr("viewBox", "0 0 " + w + " 50");
    }


    self.run = function () {
        runner();
    };
};

appNs.TimeSeriesChart.prototype.initializeData = function (elementCount) {
    var self = this;
    for (var i = 0; i < elementCount; i++) {
        self.data.push(new self.DataPoint(self.key++, 0));
    }
};

appNs.TimeSeriesChart.prototype.getData = function (x) {
    return 5 * (Math.cos(x / 1.5) + 1);
};

appNs.TimeSeriesChart.prototype.start = function () {
    var self = this;
    if (!self.isRunning) {
        self.counter = setInterval(self.run, 150);
    }
    self.isRunning = true;
};

appNs.TimeSeriesChart.prototype.stop = function () {
    var self = this;
    clearInterval(self.counter);
    self.isRunning = false;
};

appNs.chart = new appNs.TimeSeriesChart(50, '.chart-container', 25);
