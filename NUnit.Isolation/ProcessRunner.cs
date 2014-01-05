using System;
using System.Diagnostics;
using System.Reflection;
using NUnit.Framework;

namespace NUnit.Isolation
{
    /// <summary> Helps to run a test in another process. </summary>
    public static class ProcessRunner
    {
        public static void Run(TestMethodInformation testMethodInformation)
        {
            var startInfo = new ProcessStartInfo();
            startInfo.FileName = new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath;
            startInfo.Arguments = string.Format(@" ""{0}"" ""{1}"" ""{2}"" ""{3}"" ",
                                            testMethodInformation.AttachDebugger, 
                                            testMethodInformation.AssemblyName, 
                                            testMethodInformation.TypeAssemblyQualifiedName,
                                            testMethodInformation.TestMethodName);

            startInfo.RedirectStandardOutput = true;
            startInfo.RedirectStandardError = true;
            startInfo.CreateNoWindow = true;
            startInfo.ErrorDialog = false;
            startInfo.UseShellExecute = false;

            var process = new Process();
            process.StartInfo = startInfo;
            process.OutputDataReceived += (sender, eventArgs) => Console.WriteLine(eventArgs.Data);
            process.ErrorDataReceived += (sender, eventArgs) => Console.Error.WriteLine(eventArgs.Data);
            
            try
            {
                process.Start();
                process.BeginOutputReadLine();
                process.BeginErrorReadLine();
                process.WaitForExit();
            }
            finally
            {
                if (!process.HasExited)
                {
                    process.Kill();
                    throw new NotExitingProcessException();
                }
            }

            if (process.ExitCode != 0)
                throw new TestRunInSubProcessFailedException("Test failed. See output for more information about the exception");

            // everything ok
            Assert.Pass();

        }
    }
}