using System.Linq;
using System.Collections.Generic;
using System;

namespace NecroNet.UnReLoader.Commands
{
	[CommandInfo("paddprojects")]
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