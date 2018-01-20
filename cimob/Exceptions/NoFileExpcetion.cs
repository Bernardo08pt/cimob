using System;
using System.Runtime.Serialization;

namespace cimob.Extensions
{
    [Serializable]
    internal class NoFileExpcetion : Exception
    {
        public NoFileExpcetion() {}

        public NoFileExpcetion(string message) : base(message)
        {
        }

        public NoFileExpcetion(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected NoFileExpcetion(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}