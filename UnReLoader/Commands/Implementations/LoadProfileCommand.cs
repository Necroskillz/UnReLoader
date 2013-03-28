using System.Linq;
using System.Collections.Generic;
using System;
using EnvDTE;

namespace NecroNet.UnReLoader.Commands
{
	[CommandMetadata("pload", ShortHand = "pl", WriteDoneMessage = true)]
	public class LoadProfileCommand : CommandBase
	{
		public override void Execute()
		{
			ValidateSolutionLoaded();

			ValidateInMin(1);

			IEnumerable<string> projects;
			string startUpProject;

			if(Parameters.Count == 1)
			{
				var profile = ProfileManager.GetProfile(In(1), Out);
				if(profile == null) return;
			
				projects = profile.Projects;
				startUpProject = profile.StartUpProject;
			}
			else
			{
				var profileParams = Parameters.Select(p => p.ToLowerInvariant());
				var profiles = ProfileManager.GetProfiles(Out).Where(p => profileParams.Contains(p.Name.ToLowerInvariant())).ToList();
				if(!profiles.Any()) return;
			
				startUpProject = profiles.First().StartUpProject;
				projects = profiles.SelectMany(p => p.Projects).Distinct();
			}

			Logic.LoadProjects(projects, startUpProject, Out);
		}
	}
}