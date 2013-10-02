using System.Linq;
using System.Collections.Generic;
using System;

namespace NecroNet.UnReLoader.Commands
{
	[CommandMetadata("pdelete", ShortHand = "pd")]
	public class DeleteProfileCommand : CommandBase
	{
		public override void Execute()
		{
			ValidateSolutionLoaded();

			ValidateInExact(1);

			var name = In(1);

			ProfileManager.DeleteProfile(name, Out);
		}
	}
}