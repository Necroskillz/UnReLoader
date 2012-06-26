using System.Linq;
using System.Collections.Generic;
using System;
using EnvDTE;

namespace NecroNet.UnReLoader.Commands
{
	[CommandInfo("pload", WriteDoneMessage = true)]
	public class LoadProfileCommand : CommandBase
	{
		public override void Execute()
		{
			ValidateSolutionLoaded();

			ValidateInMin(1);

			IEnumerable<string> projects = Enumerable.Empty<string>();
			string startUpProject = null;
			var profileParams = Parameters.Select(p => p.ToLowerInvariant());

			if(Parameters.Count == 1)
			{
				var profile = ProfileManager.GetProfile(In(1), Out);
				if(profile != null)
				{
					projects = profile.Projects;
					startUpProject = profile.StartUpProject;
				}
			}
			else
			{
				var profiles = ProfileManager.GetProfiles(Out).Where(p => profileParams.Contains(p.Name.ToLowerInvariant())).ToList();
				if(profiles.Any())
				{
					startUpProject = profiles.First().StartUpProject;
					projects = profiles.SelectMany(p => p.Projects).Distinct();
				}
			}

			Logic.LoadProjects(projects, startUpProject, Out);
		}
	}
}