using System.Linq;
using System.Collections.Generic;
using System;

namespace NecroNet.UnReLoader.Commands
{
	[CommandMetadata("paddprojects", ShortHand = "pa")]
	public class AddProjectsToProfileCommand : CommandBase
	{
		public override void Execute()
		{
			ValidateSolutionLoaded();

			ValidateInMin(2);

			var name = In(1);

			ProfileManager.AddProjectsToProfile(name, InRange(2), Out);
		}
	}
}