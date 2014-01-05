using System;
using NUnit.Framework;
using NUnit.Isolation.Api;

namespace NUnit.Isolation.Tests
{
    // TODO (requires Nunit 2.6 (resp. resharper 7)!!
    [TestFixture]
    public class IsolationAttributeTest
    {
        [Test, Isolation(Isolations.AppDomain)]
        public void AppDomainTest()
        {
            Assert.IsTrue(IsolationDispatcher.IsInIsolatedAppDomain);
            Assert.AreEqual(AppDomainRunner.ISOLATED_APP_DOMAIN_NAME, AppDomain.CurrentDomain.FriendlyName);
        }

        [Test, Isolation(Isolations.Process)]
        public void ProcessTest()
        {
            Assert.IsTrue(IsolationDispatcher.IsRootAppDomainOfIsolatedProcess);
            Assert.IsTrue(IsolationDispatcher.IsInIsolatedAppDomain);
            Assert.AreEqual(AppDomainRunner.ISOLATED_APP_DOMAIN_NAME, AppDomain.CurrentDomain.FriendlyName);
        }
    }
}
