using System;

namespace Mx.Library.ExceptionHandling
{
	[Serializable]
	public class MxValidationException : MxException
	{
		public MxValidationException() : base() { }

		public MxValidationException(string message) : base(message) { }

		public MxValidationException(string message, System.Exception innerException) : base(message, innerException) { }

		protected MxValidationException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
	}
}
