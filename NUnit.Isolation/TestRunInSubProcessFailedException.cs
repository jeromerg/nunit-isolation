using System;
using System.Runtime.Serialization;

namespace NUnit.Isolation
{
    public class TestRunInSubProcessFailedException : Exception
    {
        public TestRunInSubProcessFailedException()
        {
        }

        public TestRunInSubProcessFailedException(string message) : base(message)
        {
        }

        public TestRunInSubProcessFailedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected TestRunInSubProcessFailedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}