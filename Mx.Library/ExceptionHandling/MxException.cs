using System;

namespace Mx.Library.ExceptionHandling
{

	[Serializable]
	public class MxException : ApplicationException
	{
		public MxException() : base() { }

		public MxException(string message) : base(message) { }

		public MxException(string message, System.Exception innerException) : base(message, innerException) { }

		protected MxException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
	}
}
