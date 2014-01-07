nunit-isolation
===============

nunit-isolation enables running every nunit test in a separate process or appdomain. The motivation is to enable UI-component testing in the same way as you test model and workflow classes: you need to isolate the component under test, mock dependencies, perform a few actions and finally assert the resulting state. Without isolating the UI display and automation in a separate appdomain or process, it is impossible to keep a stable state from test to test. 

This project is inspirated from the project NUnit.ApplicationDomain (https://www.nuget.org/packages/NUnit.ApplicationDomain). It re-uses the ability of executing the test in a separate app-domain. The usage has been extended to be able to execute the test in a separate process. 

Executing a test in separate app-domain has the drawback that you cannot ensure that the app-domain is succesfully unloaded at the end (AppDomain.Unload() throws an exception if any foreground thread remains in the AppDomain). That's why you sometimes need a full isolation by running each test in a separate process... 

On the other side, it is easier to debug test within a single process, as tools like NUnit debugger attach automatically to the testing-process.

Usage without annotation
-------------------------

See the unit test class 'IsolationTest'

	[Test]
	public void AppDomainTest()
	{
		// re-run the test in a separate app-domain
		Api.Isolation.ReRun(Isolations.AppDomain);
		
		// todo test here
	}

Usage with annotation (under construction)
------------------------------------------

See the unit test class 'IsolationAttributeTest'

	[Test, Isolation(Isolations.Process)]
	public void AppDomainTest()
	{
		// todo test here
	}

Attach debugger to separate thread
----------------------------------
While developing or fixing unit test, you may need to attach visual studio debugger to the test running in a separate process. Here is how you can attach the debugger by changing the test configuration.

	[Test]
	public void AppDomainTest()
	{
		Api.Isolation.ReRun(Isolations.AppDomain, true /*attachDebugger*/);
    //...		
	}

	[Test, Isolation(Isolations.Process, true /*attachDebugger*/)]
	public void AppDomainTest()
	{
		// todo test here
	}

Installation
------------
- get source
- replace the third-party nunit framework library
- compile with the required platform AnyCPU or x86
- add compilation result to your test project
- add annotation or re-run command to your test
- enjoy
