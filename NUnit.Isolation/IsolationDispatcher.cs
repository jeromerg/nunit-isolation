using System;
using NUnit.Framework;
using NUnit.Isolation.Api;

namespace NUnit.Isolation
{
    public static class IsolationDispatcher
    {
        public static bool IsRootAppDomainOfIsolatedProcess { get; set; }
        public static bool IsInIsolatedAppDomain { get; set; }

        public static void IsolateTestRun(Isolations isolation, TestMethodInformation testMethodInformation)
        {
            if (IsInIsolatedAppDomain)
                return;

            switch (isolation)
            {
                case Isolations.AppDomain:
                    AppDomainRunner.Run(testMethodInformation, true);
                    break;
                case Isolations.Process:
                    ProcessRunner.Run(testMethodInformation);
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
            Assert.Pass();
        }


    }
}
