#Unit Testing Services with Moq

<time class="postinfo left-50 postdate">August 25, 2013</time>

Service patterns have become a standard method of modelling the backend features of modern applications. For a little background, Eric Evans (link) describes a service as some piece of logic or processing that is outside the normal operations for a domain model or entity. 

Even though the intent is to separate concerns and simplify your application, services can become complex and depend on other services themselves. As a result of these dependencies your services can become more and more difficult to test.

The me











This can create a cascading effect especially when you include dependency injection. 

One way to test these complex scenarios and still be able to isolate the concerns is with Mocking.


In a well designed MVC application, the business logic is cleanly abstracted from the presentation by the controllers. This establishes a clean separation of concerns and allows the controllers to be tested for the simple act of requesting and receiving data.

I will assume that most readers have already explored Test Driven Development to some extent. If you have not it is a key tenant behind modern software development and you should take a little time to research it.

When setting up the tests for the web component of an MVC application, I am generally concerned with the controllers. What I am really wanting to test is that when a controller action is called it attempts to retrieve the data from a domain service. I am not concerned with the implemenation of the domain service at all during this stage. What I want to know is that when the controller invokes the domain service it responds with some data and the controller handles the data and passes it off to the view.

Mocking and Stubbing are two of the more popular approaches to establishing domain service contexts for the tests, and I use both techniques. But for the unit tests in this example I am using Mocking. This allows me to instantiate a domain service with a fake data context. This can be used to isolate the service to the facts that it can be instantiated and can return data, with no regards to the actual business logic that would usually be involved in the process.

**Introducing Moq!**

There are many Mocking frameworks that available and I recommend playing with as many as possible. For this example I am using Moq. The source and binaries can be downloaded from the link or simply added to your project using Nuget.

In addition to Moq, I am using MSTest as my unit testing framework. Again, there are many testing frameworks and I encourage you to try out as many as you can.

So, let's see a simple test:

```c#
[TestMethod]
public void BlogControllerViewPostTest()
{
	// Mock my data service
	Mock<IBlogDataService> messagingService = new Mock<IBlogDataService>();

	// Setup the mock with some test implementation
	messagingService.Setup(m => m.ViewPost("Closures in C#")).Returns(() =>
	{
		return new BlogPost { 
			Title = "Testing with Moq in MVC", 
			Author = "Test Author", 
			Body = "Moq Rocks!", 
			PostDate = DateTime.Now 
		};
	});

	// Create the controller and call an action
	BlogController controller = new BlogController(messagingService.Object);
	ActionResult result = controller.ViewPost("Testing with Moq in MVC");
	ViewResult actual = (ViewResult)result;

	// Assert expectations
	Assert.AreEqual("Testing with Moq in MVC", actual.Model.Title);
}
```

And that's it! What we have done is created our service with a Mock implementation. Then we call the controller action and verify that the action is indeed passing the data on to the view.

The tests can become more involved, but I generally try to keep my controllers slim and push all of the logic back into the domain model.

One interesting note specific to Moq from the example above is that the Mock method is expecting the controller action to pass in "Testing with Moq in MVC". Any other value would not have went into Mock's implementation of ViewPost.