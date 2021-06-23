using System;

namespace Mx.Library.ExceptionHandling
{
    [Serializable]
    public class MxMultipleFoundException : MxException
    {
        public MxMultipleFoundException() : base() { }

        public MxMultipleFoundException(string message) : base(message) { }

        public MxMultipleFoundException(string message, System.Exception innerException) : base(message, innerException) { }

        protected MxMultipleFoundException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}