using System;
using System.Linq;
using System.Collections.Generic;

namespace NecroNet.UnReLoader.Commands
{
	public class CommandMetadataAttribute : Attribute
	{
		public CommandMetadataAttribute(string name)
		{
			Name = name;
		}

		public string Name { get; set; }
		public string ShortHand { get; set; }
		public bool WriteDoneMessage { get; set; }
	}
}