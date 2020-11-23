using System;
using System.Runtime.Serialization;

namespace CodersAcademy.API.Exception
{
	[Serializable]
	public class CodersAcademyException : System.Exception
	{
		public CodersAcademyException(string message) : base(message)
		{

		}

		protected CodersAcademyException(SerializationInfo info,StreamingContext streamingContext) : base(info, streamingContext)
		{

		}
	}
}
