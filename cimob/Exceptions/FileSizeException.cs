using System;
using System.Runtime.Serialization;

namespace cimob.Extensions
{
    [Serializable]
    internal class FileSizeException : Exception
    {
        public FileSizeException() {}

        public FileSizeException(string message) : base(message)
        {
        }

        public FileSizeException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected FileSizeException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}