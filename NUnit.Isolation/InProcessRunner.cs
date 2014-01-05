using System.Diagnostics;

namespace NUnit.Isolation
{
    public class InProcessRunner
    {
        public static void Main(string[] args)
        {
            IsolationDispatcher.IsRootAppDomainOfIsolatedProcess = true;

            bool attachDebugger = bool.Parse(args[0]);
            string testAssemblyFullName = args[1];
            string typeAssemblyQualifiedName = args[2];
            string testMethodName = args[3];

            var testMethodInformation = new TestMethodInformation(testAssemblyFullName, typeAssemblyQualifiedName, testMethodName, attachDebugger);

            if (testMethodInformation.AttachDebugger)
                Debugger.Launch();

            // remark: no need to close the appdomain, as we close the process
            AppDomainRunner.Run(testMethodInformation, false);
        }
    }
}
