using System;
using System.Runtime.Serialization;

namespace CodersAcademy.API.Exception
{
	[Serializable]
	public class CodersAcademyExceptions : System.Exception
	{
		public CodersAcademyExceptions(string message) : base(message)
		{

		}

		protected CodersAcademyExceptions(SerializationInfo info,StreamingContext streamingContext) : base(info, streamingContext)
		{

		}
	}
}
