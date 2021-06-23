

using System;

namespace Mx.Library.ExceptionHandling
{
	[Serializable]
	public class MxAuthenticationException : MxException
	{
		public MxAuthenticationException() : base() { }

		public MxAuthenticationException(string message) : base(message) { }

		public MxAuthenticationException(string message, System.Exception innerException) : base(message, innerException) { }

		protected MxAuthenticationException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
	}
}
