using System;
using NUnit.Framework;
using NUnit.Framework.Interfaces;

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

        public void BeforeTest(ITest test)
        {
            var testMethodInformation = new TestMethodInformation(test.Method.MethodInfo, mAttachDebugger);
            IsolationDispatcher.IsolateTestRun(mIsolation, testMethodInformation);
        }

        public void AfterTest(ITest test)
        {
        }

        public ActionTargets Targets
        {
            get { return ActionTargets.Test; }
        }
    }
}