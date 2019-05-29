using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;
using System.Reflection;
using System.Threading;

namespace CsSeleniumFrame.src.Logger
{
    public static class CsSeTestMetaFinder
    {
        public static string GetTestMethodName()
        {
            // for when it runs via Visual Studio locally
            var stackTrace = new StackTrace();
            foreach (var stackFrame in stackTrace.GetFrames())
            {
                MethodBase methodBase = stackFrame.GetMethod();
                Object[] attributes = methodBase.GetCustomAttributes(typeof(TestMethodAttribute), false);
                if (attributes.Length >= 1)
                {
                    return methodBase.Name;
                }
            }
            return "Not called from a test method";
        }

        public static string GetTestClassName()
        {
            // for when it runs via Visual Studio locally
            var stackTrace = new StackTrace();
            foreach (var stackFrame in stackTrace.GetFrames())
            {
                MethodBase methodBase = stackFrame.GetMethod();
                Object[] attributes = methodBase.GetCustomAttributes(typeof(TestMethodAttribute), false);
                if (attributes.Length >= 1)
                {
                    return methodBase.DeclaringType.Name;
                }
            }
            return "Not able to identify test class.";
        }

        public static string GetTestModuleName()
        {
            // for when it runs via Visual Studio locally
            var stackTrace = new StackTrace();
            foreach (var stackFrame in stackTrace.GetFrames())
            {
                MethodBase methodBase = stackFrame.GetMethod();
                Object[] attributes = methodBase.GetCustomAttributes(typeof(TestMethodAttribute), false);
                if (attributes.Length >= 1)
                {
                    return methodBase.Module.Name;
                }
            }
            return "Not able to identify test module.";
        }

        public static string GetMachineName()
        {
            return Environment.MachineName;
        }

        public static int GetTestThreadId()
        {
            return Thread.CurrentThread.ManagedThreadId;
        }
    }
}
