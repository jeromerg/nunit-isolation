using System;
using System.Runtime.Serialization;

namespace NUnit.Isolation
{
    public class NotExitingProcessException : Exception
    {
        public NotExitingProcessException()
        {
        }

        public NotExitingProcessException(string message) : base(message)
        {
        }

        public NotExitingProcessException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected NotExitingProcessException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}