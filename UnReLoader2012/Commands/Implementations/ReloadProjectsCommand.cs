using System.Linq;
using System.Collections.Generic;
using System;

namespace NecroNet.UnReLoader.Commands
{
	[CommandMetadata("reload", ShortHand = "r", WriteDoneMessage = true)]
	public class ReloadProjectsCommand : CommandBase
	{
		public override void Execute()
		{
			ValidateSolutionLoaded();

			ValidateInMin(1);

			var first = In(1);

			if (first == Keywords.All)
			{
				Logic.UnloadOrReloadAllProjects(UnReLoad.Reload, Out);
			}
			else
			{
				Logic.UnloadOrReloadProjects(UnReLoad.Reload, Parameters, Out);
			}
		}
	}
}