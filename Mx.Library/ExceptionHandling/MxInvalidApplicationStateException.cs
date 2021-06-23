using System;


namespace Mx.Library.ExceptionHandling
{
	[Serializable]
	public class MxApplicationStateException : MxException
	{
		public MxApplicationStateException() : base() { }

		public MxApplicationStateException(string message) : base(message) { }

		public MxApplicationStateException(string message, System.Exception innerException) : base(message, innerException) { }

		protected MxApplicationStateException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
	}
}
