using System;
using NUnit.Framework;

namespace NUnit.Isolation.Api
{
    public enum Isolations
    {
        AppDomain,
        Process
    }
 
    [AttributeUsage(AttributeTargets.Method)]
    public class IsolationAttribute : Attribute, ITestAction
    {
        private readonly Isolations mIsolation;
        private readonly bool mAttachDebugger;

        public IsolationAttribute(Isolations isolation, bool attachDebugger = false)
        {
            mIsolation = isolation;
            mAttachDebugger = attachDebugger;
        }

        public void BeforeTest(TestDetails testDetails)
        {
            var testMethodInformation = new TestMethodInformation(testDetails.Method, mAttachDebugger);
            IsolationDispatcher.IsolateTestRun(mIsolation, testMethodInformation);
        }

        public void AfterTest(TestDetails testDetails)
        {
        }

        public ActionTargets Targets
        {
            get { return ActionTargets.Test; }
        }
    }
}