using System;
using System.IO;
using System.Reflection;
using System.Security;
using System.Security.Policy;

namespace NUnit.Isolation
{
    public static class AppDomainRunner
    {
        public const string ISOLATED_APP_DOMAIN_NAME = "Isolated AppDomain";

        public static void Run(TestMethodInformation testMethodInformation, bool unloadAppDomain)
        {
            var appDomainSetup = new AppDomainSetup();
            appDomainSetup.ApplicationBase = Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath);
            appDomainSetup.ConfigurationFile = testMethodInformation.ConfigurationFile;            

            AppDomain appDomain = AppDomain.CreateDomain(ISOLATED_APP_DOMAIN_NAME, null, appDomainSetup, GetPermissionSet());
            appDomain.Load(testMethodInformation.AssemblyName);

            var instance = (InAppDomainRunner)appDomain.CreateInstanceAndUnwrap(
              typeof(InAppDomainRunner).Assembly.FullName,
              typeof(InAppDomainRunner).FullName);


            try 
            {
                instance.Execute(testMethodInformation);
            } 
            finally 
            {
                if (unloadAppDomain)
                    AppDomain.Unload(appDomain);
            }
        }


        private static PermissionSet GetPermissionSet()
        {
            var ev = new Evidence();
            ev.AddHostEvidence(new Zone(SecurityZone.MyComputer));
            return SecurityManager.GetStandardSandbox(ev);
        }
    }
}