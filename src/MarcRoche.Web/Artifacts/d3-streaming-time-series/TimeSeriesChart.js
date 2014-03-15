var TimeSeriesChart = (function (appNs) {
    var TimeSeriesChart = function (height, width, container, elementCount) {
        var self = this;
        var w = width;
        var h = height;

        self.data = [];
        self.key = 0;
        self.cnt = 0;
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
            .attr("viewBox", "0 0 800 50")
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

        self.counter = setInterval(runner, 125);

        function runner() {
            self.data.unshift(new self.DataPoint(self.key++, self.getData(self.cnt++ / 3)));
            self.data.pop();

            self.svg.selectAll("rect")
              .data(self.data)
              .transition()
              .duration(125)
              .attr("y", function (d) {
                  return h - self.scaleY(d.value * 10) - 0.5;
              })
              .attr("height", function (d) {
                  return self.scaleY(d.value * 10);
              });
        }

        self.run = function () {
            runner();
        };
    };

    TimeSeriesChart.prototype.initializeData = function (elementCount) {
        var self = this;
        for (var i = 0; i < elementCount; i++) {
            self.data.push(new self.DataPoint(self.key++, 0));
        }
    };

    TimeSeriesChart.prototype.getData = function (x) {
        return 5 * (Math.cos(x) + 1);
    };

    TimeSeriesChart.prototype.start = function () {
        var self = this;
        if (!self.isRunning) {
            self.counter = setInterval(self.run, 125);
        }
        self.isRunning = true;
    };

    TimeSeriesChart.prototype.stop = function () {
        var self = this;
        clearInterval(self.counter);
        self.isRunning = false;
    };

    return TimeSeriesChart;
})(appNs || {});