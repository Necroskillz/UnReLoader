using System;
using System.Linq;
using System.Collections.Generic;

namespace NecroNet.UnReLoader.Commands
{
	public class ParameterValidationException : Exception
	{
		public ParameterValidationException(params string[] lines)
		{
			Lines = lines;
		}

		public IEnumerable<string> Lines { get; set; }
	}
}