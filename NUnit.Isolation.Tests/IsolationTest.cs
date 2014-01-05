using System;
using System.Configuration;
using NUnit.Framework;
using NUnit.Isolation.Api;

namespace NUnit.Isolation.Tests
{
    [TestFixture]
    public class IsolationTest
    {
        [Test]
        public void AppDomainTest()
        {
            Api.Isolation.ReRun(Isolations.AppDomain);
            Assert.IsTrue(IsolationDispatcher.IsInIsolatedAppDomain);
            Assert.AreEqual(AppDomainRunner.ISOLATED_APP_DOMAIN_NAME, AppDomain.CurrentDomain.FriendlyName);
        }

        [Test]
        public void ProcessTest()
        {
            Api.Isolation.ReRun(Isolations.Process);

            StringAssert.Contains("NUnit.Isolation.exe".ToLower(), Environment.CommandLine.ToLower());
            Assert.IsFalse(IsolationDispatcher.IsRootAppDomainOfIsolatedProcess, "test must run in a separate app domain within the separate process");
            Assert.IsTrue(IsolationDispatcher.IsInIsolatedAppDomain);
            Assert.AreEqual(AppDomainRunner.ISOLATED_APP_DOMAIN_NAME, AppDomain.CurrentDomain.FriendlyName);
        }

        [Test, ExpectedException]
        public void ProcessTest_TestExpectedToThrowAnException()
        {
            Api.Isolation.ReRun(Isolations.Process);
            throw new ApplicationException("voilà");
        }

        [Test]
        public void ProcessTest_AppSettings()
        {
            Api.Isolation.ReRun(Isolations.Process);
            Assert.AreEqual("testValue", ConfigurationManager.AppSettings["testKey"], "app.config expected to be read in separate process and separate appdomain");
        }
    }
}
