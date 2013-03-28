using System.Linq;
using System.Collections.Generic;
using System;

namespace NecroNet.UnReLoader.Commands
{
	[CommandMetadata("premoveprojects", ShortHand = "pr")]
	public class RemoveProjectsFromProfileCommand : CommandBase
	{
		public override void Execute()
		{
			ValidateSolutionLoaded();

			ValidateInMin(2);

			var name = In(1);

			ProfileManager.RemoveProjectsFromProfile(name, InRange(2), Out);
		}
	}
}