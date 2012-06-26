using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace NecroNet.UnReLoader.Commands
{
	[CommandInfo("pcreate")]
	public class CreateProfileCommand : CommandBase
	{
		public override void Execute()
		{
			ValidateSolutionLoaded();

			ValidateInMin(1);

			var name = In(1);

			ProfileManager.CreateProfile(name, InRange(2), Parameters.Count > 1 ? In(2) : null, Out);
		}
	}
}