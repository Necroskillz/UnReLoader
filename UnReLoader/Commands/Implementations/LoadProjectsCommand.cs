using System;
using System.Collections.Generic;
using System.Linq;

namespace NecroNet.UnReLoader.Commands
{
	[CommandInfo("load", WriteDoneMessage = true)]
	public class LoadProjectsCommand : CommandBase
	{
		public override void Execute()
		{
			ValidateSolutionLoaded();

			ValidateInMin(1);

			Logic.LoadProjects(Parameters, null, Out);
		}
	}
}