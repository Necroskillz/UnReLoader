using System;
using System.Linq;
using System.Collections.Generic;

namespace NecroNet.UnReLoader.Commands
{
	public class CommandInfoAttribute : Attribute
	{
		public CommandInfoAttribute(string name)
		{
			Name = name;
		}

		public string Name { get; set; }
		public bool WriteDoneMessage { get; set; }
	}
}