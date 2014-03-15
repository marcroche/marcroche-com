#JavaScript Priority Queue

<div>
	<time class="postinfo left-50 postdate">January 28, 2014</time>
</div>

One of the new projects I am starting up is using <a href="http://d3js.org/" target="_blank">D3</a> to visualize shortest path problems.

An infrastructure pre-requisite I will need in place to start this is a **Priority Queue**. This is a data structure that allows us to retrieve the minimum value, or maximum (depending on implementation), in the data set very efficiently. During the course of the path finding algorithm we will be pushing the path weights onto the queue and then retrieving the smallest one to determine the next step in the path.

So, let's consider a set of values in an array:<br />

<img src="/artifacts/javascript-priority-queue/array.png" class="array" />

Currently this array is a set of unordered values with a linear structure. However, it could also be thought of as a nearly complete binary tree:<br />

<img src="/artifacts/javascript-priority-queue/tree.png" />

Once we think of the structure in this way, we can begin to use tree properties and methods to manipulate the data.

##Binary Heaps##

The priority queue I am building is implemented on a Binary Heap data structure. Binary Heaps are complete, or nearly complete, binary trees with two additional properties.

* **Shape Property** - The structure is a Binary Tree, or nearly complete Binary Tree. All levels must be fully filled, except possibly the last level. If that level is not filled it must be filled from left to right.
* **Heap Property** - The nodes of the tree are either less than or equal to, or greater than or equal to, each of its children.

Usually Binary Heaps are referred to as Min-Heaps or Max-Heaps depending on how the Heap Property is implemented. For the shortest path problems we will implement a Min-Heap for our Priority Queue.

###Building the Heap###

To start we take the items from FLOOR((A.length - 1) / 2) down to zero and apply our MinHeapify algorithm. The reason for this is that these items will all be nodes, while the remaining items will all be leaves.

```JavaScript
// Size is A.length - 1 to compensate for a zero indexed array.
function buildMinHeap(A, size) {
	for(var i = Math.floor(size / 2); i >= 0; i--) { 
		minHeapify(A, i, size); 
	}
}
```

Now that we have these items selected we will start applying our MinHeapify algorithm. We cycle through and determine if the current element at A[i], the left child, or the right child is the smallest. If the current element is already the smallest then the current sub-tree is a Min-Heap and we can go to the next node. If one of the children is the smallest we swap it with the root and we now have a sub-tree that is a Min-Heap.

In the situation where we swap a child value for the current index value we could have sub-tree rooted at A[smallest] that no longer meets the **Heap Property**. Therefore we need to recursively apply the MinHeapify algorithm to that sub-tree to repeat the process.

```JavaScript
function minHeapify(A, i, size) {
	var l = left(i);
	var r = right(i);
	var smallest;
    
	if(l <= size && A[l] < A[i]) {
		smallest = l; 
	} else {
		smallest = i; 
	}
    
	if(r <= size && A[r] < A[smallest]) {
		smallest = r; 
	}
    
	if(smallest != i) {
		var tmp = A[i];
		A[i] = A[smallest];
		A[smallest] = tmp;
      
		minHeapify(A, smallest, size);
	}
}
```
We now have a Min-Heap structure that meets the **Shape Property** and the **Heap Property**. It's worth noting that it is not necessarily in lowest to highest order within the array itself.<br />
<img src="/artifacts/javascript-priority-queue/minheap.png" />

##Priority Queue##

Now that we have our Binary Heap structure implementing the Priority Queue is trivial.

For our interface we will have four methods into the queue.

* **Peek** will return the smallest value without removing it from the queue.
* **Pop** will return the smallest value and remove it from the queue. This forces a reorganization of the nodes.
* **Push** will insert a new value into the heap and organize it appropriately.
* **Print** is a debugging function that will print out the array structure in order.

###Peek###

This is probably the simplest method in the whole structure. It simply returns the item at the zero index of the array.

```JavaScript
function peek() {
	if(array.length < 1) {
		throw 'Heap Underflow';
	}
    
	return array[0];
}
```

###Pop###

This method is only slightly different from Peek. We return the minimum value from the array at index zero, but we also remove that item from the array. We then take the last item and move it to the front and run MinHeapify on the array to restructure it.

```JavaScript
function pop() {
	if(array.length < 1) {
		throw 'Heap Underflow';
	}
    
	var min = array[0];
	array[0] = array[array.length - 1];
	array.splice(array.length - 1, 1);
    
	minHeapify(array, 0, array.length - 1);
    
	return min;
}
```

###Push###

For push we add the new value to the end of the array bubble is up to its appropriate location. We assume that we are already working with a structure that meets the **Heap Property**.

```JavaScript
function queue(value) {
	array.push(value);
	heapDecreaseKey(array, array.length - 1, value);
}

function heapDecreaseKey(A, i, key) {
	while(i > 0 && A[parent(i)] > A[i]) {
		var tmp = A[i];
		A[i] = A[parent(i)];
		A[parent(i)] = tmp;
		i = parent(i);
	}
}
```

###Print###

With print we just cycle through the array and log the current value.

```JavaScript
function print() {
	for(var i = 0; i < array.length; i++) {
		console.log(array[i]); 
	}
}
```

###The Complete Code###

That is it! We now have our Min-Priority Queue ready to go. The complete code looks like this:

```JavaScript
app_ns.MinPriorityQueue = (function() {
  var array;
  
  function left(i) {
    return 2 * i + 1;
  }
  
  function right(i) {
    return 2 * i + 2;
  }
  
  function parent(i) {
    return Math.floor(i / 2); 
  }
  
  function minHeapify(A, i, size) {
    var l = left(i);
    var r = right(i);
    var largest;
    
    if(l <= size && A[l] < A[i]) {
     largest = l; 
    } else {
     largest = i; 
    }
    
    if(r <= size && A[r] < A[largest]) {
     largest = r; 
    }
    
    if(largest != i) {
      var tmp = A[i];
      A[i] = A[largest];
      A[largest] = tmp;
      
      minHeapify(A, largest, size);
    }
  }
  
  function buildMinHeap(A, size) {
    for(var i = Math.floor(size / 2); i >= 0; i--) { 
     minHeapify(A, i, size); 
    }
  }
  
  function peek() {
    if(array.length < 1) {
      throw 'Heap Underflow';
    }
    
    return array[0];
  }
  
  function pop() {
    if(array.length < 1) {
      throw 'Heap Underflow';
    }
    
    var min = array[0];
    array[0] = array[array.length - 1];
    array.splice(array.length - 1, 1);
    
    minHeapify(array, 0, array.length - 1);
    
    return min;
  }

  function push(value) {
    array.push(value);
    heapDecreaseKey(array, array.length - 1, value);
  }
  
  function print() {
    for(var i = 0; i < array.length; i++) {
     console.log(array[i]); 
    }
  }
  
  function heapDecreaseKey(A, i, key) {
    while(i > 0 && A[parent(i)] > A[i]) {
      var tmp = A[i];
      A[i] = A[parent(i)];
      A[parent(i)] = tmp;
      i = parent(i);
    }
  }
  

  
  var api = function(arr) {
    array = arr;
    buildMinHeap(array, array.length -1);
    
    this.peek = peek;
    this.pop = pop;
    this.queue = queue;
    this.print = print;
  };
  
  return api;
  
})();
```
Using this structure is very simple. To create a queue and initialize it with values looks like this: 

```JavaScript
var minQueue = new app_ns.MinPriorityQueue([11, 24, 22, 13, 9, 7, 8, 10, 14, 16]);
```

To push values onto the queue:

```JavaScript
minQueue.push(1);
```

And to pop values from the queue:

```JavaScript
var value = minQueue.pop();
```