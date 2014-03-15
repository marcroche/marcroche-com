#Visualizing Path Finding with D3.js

<div>
	<time class="postinfo left-50 postdate">February 15, 2014</time>
</div>

I am continuing my explorations into <a href="http://d3js.org/" target="_blank">D3</a> with visualizing path finding algorithms.

To use the demo below, draw some obstacles for the path finder to go around and then press start. It will search the grid, at a slowed speed for visualization, and find the shortest path to the end square.

<div id="grid" style="height: 500px"></div>
<button onclick='start()'>Start</button>
<button onclick='reset()'>Reset</button>

##The Core D3 Code

The D3 specific code for this application is contained in the following module.

```JavaScript
mr_com.Grid = (function (d3, Vertex, Edge) {
    var self;
    var id, rows, cols, squareSize, data;
    var isMouseDown = false;
    var isGridReady = true;
    var grid, row, cell;

    d3.select(window).on('mousedown', function () { isMouseDown = true; });
    d3.select(window).on('mouseup', function () { isMouseDown = false; });

    function init() {
        grid = d3.select(id).append("svg")
            .attr("width", cols * squareSize)
            .attr("height", rows * squareSize)
            .attr("class", "chart");

        row = grid.selectAll('.row')
            .data(data, function (d) {
                return data.indexOf(d);
            })
            .enter()
            .append("svg:g")
          .attr("class", "row");

        cell = row.selectAll('.cell')
          .data(function (d) {
              return d;
          },
              function (d) {
                  return d.key;
              })
          .enter()
          .append('rect')
          .attr('x', function (d, i, r) {
              return i * squareSize;
          })
          .attr('y', function (d, i, r) {
              return r * squareSize;
          })
          .attr('width', squareSize)
          .attr('height', squareSize)
              .attr('class', 'cell')
          .style('fill', function (d) {
              return d.visited === false ? 'white' : 'blue';
          })
          .style("stroke", '#A5A5A5')
          .on('mouseover', function (d, i, a, p) {
              if (d.isBlocked === false && isGridReady) {
                  d3.select(this).style("fill", "#E2E2E2");
              }

              if (isMouseDown && isGridReady) {
                  d.isBlocked = true;
                  d3.select(this).style("fill", "#05056B");
                  d3.select(this).style("stroke", "#676767");
              }
          })
        .on('mouseout', function (d, i) {
            if (d.isBlocked === false && isGridReady) {
                d3.select(this).style("fill", "white");
            }
        })
        .on('mousedown', function (d, i) {
            if (d.isBlocked === false && isGridReady) {
                d.isBlocked = true;
                d3.select(this).style("fill", "#05056B");
                d3.select(this).style("stroke", "#676767");
            } else if (isGridReady) {
                d.isBlocked = false;
                d3.select(this).style("fill", "#E2E2E2");
                d3.select(this).style("stroke", '#A5A5A5');
            }
        });
    }

    function update() {
        var r = grid.selectAll('.row')
          .data(data, function (d) {
              return data.indexOf(d);
          });

        var c = r.selectAll('rect')
              .data(function (d) {
                  return d;
              }, function (d) {
                  return d.key;
              })
            .transition()
            .style('fill', function (d) {
                var fill = 'white';
                if (d.visited) {
                    fill = '#67CCFE';
                }
                if (d.isBlocked) {
                    fill = "#05056B";
                }
                if (d.isStart) {
                    fill = '#87FF6F';
                }
                if (d.isEnd) {
                    fill = '#FF0F0F';
                }
                if (d.isOnPath) {
                    fill = '#FFF886';
                }
                return fill;
            });
    }

    function complete() {
        isGridReady = false;
    }

    function reset(_data) {
        data = _data;
        isGridReady = true;
        update();
    }

    var api = function (_id, _rows, _cols, _squareSize, _data) {
        self = this;
        data = _data;
        id = _id;
        cols = _cols;
        rows = _rows;
        squareSize = _squareSize;

        init();

        this.update = update;
        this.reset = reset;
        this.complete = complete;
    };

    return api;
})(d3, mr_com.Vertex, mr_com.Edge);
```

The important areas to note are the init() and update() functions. These guys handle the D3 selections and this is where most of the magic happens.

**init()**<br />
In this function we handle the D3 enter selection and we add the svg and grid elements. We also handle basic formatting and setup our event handlers.

One interesting aspect to point out is how the nested selections work. Select svg, select row, select cells.

We handle the events in the .on() functions that D3 provides.

**update()**<br />
This function uses the transition selection to handle updates to our data. It then applies these changes to the existing elements.

Since we don't remove elements after the initial creation we never use the exit selection.

The other code is for managing the state of the data.

##The Search Algorithm

For the initial implementation I am using Dijkstra's algorithm to search the grid. The implementation follows:

```JavaScript
//TODO: Update this with hasPathTo
 mr_com.Dijkstra = (function(MinPriorityQueue, Q) {

 	var priorityQueue;
 	var distTo = [];
 	var shortestEdges = [];
 	var startVertex, endVertex;

 	function relax(edge) {
	  var source = edge.source;
	  var target = edge.target;
	  
	  if(target.isBlocked) {
	   return; 
	  }
	  
	  if(target.cost > source.cost + edge.weight) {
	    target.cost = source.cost + edge.weight;
	    
	    var se = _.findWhere(shortestEdges, { key: target.key });
	    if(se !== undefined) {
	      se.edge = edge;
	    } else {
	      shortestEdges.push({ key: target.key, edge: edge});
	    }
	    
	    if(priorityQueue.contains(target.key)) {
	       priorityQueue.decreaseKey(target.heapIndex, target.cost);
	    } else {
	      priorityQueue.push(target);
	    }
	  }
	};

	function shortestPath(destinationKey) {
	  var path = [];
	  var e = _.findWhere(shortestEdges, { key: destinationKey });
	  
	  if (!hasPathTo(e)) {
	      return path;
	  }

	  while(e !== undefined) {
	    e = _.findWhere(shortestEdges, { key: e.edge.source.key });
	    path.unshift(e);
	  }
	  return path;
	};

	function search() {
		var deferred = Q.defer();
		var runner = setInterval(function () {
		  var v = priorityQueue.pop();
		  v.heapIndex = -1;
		  for(var i = 0; i < v.edges.length; i++) {
		    relax(v.edges[i]);
		  }
		  v.visited = true;
		  deferred.notify({
            status: 'visited'
	      });
		  
		  if(priorityQueue.isEmpty()) {
		   clearInterval(runner);
		    var sp = shortestPath(endVertex.key);
		    for(var s = 0; s < sp.length; s++) {
		      if(sp[s] !== undefined) {
		       sp[s].edge.target.isOnPath = true; 
		      }
		    }
		    deferred.notify({
	          status: 'complete'
		    });
		    deferred.resolve();
		  }
		}, 25);

		return deferred.promise;
	};

 	var api = function(_startVertex, _endVertex) {
 		startVertex = _startVertex;
 		endVertex = _endVertex;
 		priorityQueue = new MinPriorityQueue('key', 'cost', []);
 		priorityQueue.push(startVertex);

 		this.search = search;
 	};

 	return api;

 })(mr_com.MinPriorityQueue, Q);
```


##The Core Data Structures

```JavaScript
mr_com.GridGraph = (function(Vertex, Edge) {
	var vertices = [];

	var self;
	var cols;
	var rows;
	
	function findNeighbors() {
	  for(var r = 0; r < rows; r++) {
	    for(var c = 0; c < cols; c++) {
	      if(c - 1 >= 0) {
	        vertices[r][c].edges.push(new Edge(vertices[r][c], vertices[r][c - 1], 1.0));
	      }
	      
	      if(c + 1 < cols) {
	        vertices[r][c].edges.push(new Edge(vertices[r][c], vertices[r][c + 1], 1.0));
	      }
	      
	      if(r - 1 >= 0) {
	        vertices[r][c].edges.push(new Edge(vertices[r][c], vertices[r - 1][c], 1.0));
	      }
	      
	      if(r + 1 < rows) {
	        vertices[r][c].edges.push(new Edge(vertices[r][c], vertices[r + 1][c], 1.0));
	      }
	    }
	  }
	}

	function createGraph(_rows, _cols) {
		vertices = [];
		for(var i = 0; i < rows; i++) {
		  var r = [];
		  for (var j = 0; j < cols; j++) {
		    r.push(new Vertex(i, j));
		  }
		  vertices.push(r);
		}
	}

	var api = function(_rows, _cols) {
		self = this;
		rows = _rows;
		cols = _cols;

		createGraph(rows, cols);
		findNeighbors();

		this.vertices = vertices;
	};

	return api;
})(mr_com.Vertex, mr_com.Edge);
```

```JavaScript
mr_com.Vertex = function(row, column) {
  
  this.key = '[' + row + ',' + column + ']';
  this.row = row;
  this.column = column;
  

  this.heapIndex = -1;
  this.visited = false;
  this.isOnPath = false;
  this.cost = Number.POSITIVE_INFINITY;
  this.edges = [];


  this.isBlocked = false;
  this.isStart = false;
  this.isEnd = false;

  this.reset = function() {
    this.heapIndex = -1;
    this.visited = false;
    this.isOnPath = false;
    this.cost = Number.POSITIVE_INFINITY;
    this.isBlocked = false;
    this.isStart = false;
    this.isEnd = false;
  }
};
```

```JavaScript
mr_com.Edge = function(source, target, weight) {
  this.weight = weight;
  this.target = target;
  this.source = source;
};
```