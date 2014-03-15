var gridGraph = new mr_com.GridGraph(25, 35, mr_com.Vertex, mr_com.Edge);
var grid = new mr_com.Grid('#grid', 25, 35, 20, gridGraph.vertices);
var dijkstra = new mr_com.Dijkstra(gridGraph.vertices[1][2], gridGraph.vertices[20][30], grid);

function reset() {
    for (var i = 0; i < gridGraph.vertices.length; i++) {
        for (var j = 0; j < gridGraph.vertices[i].length; j++) {
            gridGraph.vertices[i][j].reset();
        }
    }
    grid.reset(gridGraph.vertices);
}

function start() {
    gridGraph.vertices[1][2].cost = 0.0;
    gridGraph.vertices[1][2].isStart = true;
    gridGraph.vertices[20][30].isEnd = true;

    dijkstra.search().progress(function () {
        grid.update();
    }).then(function () {
        grid.update();
        grid.complete(function () {
            for (var i = 0; i < rows; i++) {
                for (var j = 0; j < cols; j++) {
                    gridGraph.vertices[i][j].reset();
                }
            }
        });
    });
}