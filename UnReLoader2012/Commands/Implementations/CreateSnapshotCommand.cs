using System;
using System.Collections.Generic;
using System.Linq;

namespace NecroNet.UnReLoader.Commands
{
	[CommandMetadata("psnapshot", ShortHand = "psnap")]
	public class CreateSnapshotCommand : CommandBase
	{
		public override void Execute()
		{
			ValidateSolutionLoaded();

			ValidateInExact(1);

			var snapshot = Logic.GetSnapshot();

			ProfileManager.CreateProfile(In(1), snapshot.Projects, snapshot.StartUpProject, Out);
		}
	}
}