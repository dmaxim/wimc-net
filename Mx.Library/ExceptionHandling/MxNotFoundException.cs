
using System;

namespace Mx.Library.ExceptionHandling
{
	[Serializable]
	public class MxNotFoundException : MxException
	{
		public MxNotFoundException() : base() { }

		public MxNotFoundException(string message) : base(message) { }

		public MxNotFoundException(string message, System.Exception innerException) : base(message, innerException) { }

		protected MxNotFoundException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
	}
}
