using System;
using System.Linq;
using System.Collections.Generic;

namespace NecroNet.UnReLoader.Commands
{
	[CommandMetadata("unload", ShortHand = "u", WriteDoneMessage = true)]
	public class UnloadProjectsCommand : CommandBase
	{
		public override void Execute()
		{
			ValidateSolutionLoaded();

			ValidateInMin(1);

			var first = In(1);

			if (first == Keywords.All)
			{
				Logic.UnloadOrReloadAllProjects(UnReLoad.Unload, Out);
			}
			else
			{
				Logic.UnloadOrReloadProjects(UnReLoad.Unload, Parameters, Out);
			}
		}
	}
}