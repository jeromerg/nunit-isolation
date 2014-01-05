using System.Diagnostics;
using System.Reflection;

namespace NUnit.Isolation.Api
{
    public static class Isolation
    {
        public static void ReRun(Isolations isolation, bool attachDebugger = false)
        {
            MethodBase methodInfo = new StackTrace().GetFrame(1).GetMethod();
            var testMethodInformation = new TestMethodInformation(methodInfo, attachDebugger);
            IsolationDispatcher.IsolateTestRun(isolation, testMethodInformation);
        }
    }
}