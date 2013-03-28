using System.Linq;
using System.Collections.Generic;
using System;

namespace NecroNet.UnReLoader.Commands
{
	[CommandMetadata("plist", ShortHand = "pls")]
	public class ListProfileCommand : CommandBase
	{
		public override void Execute()
		{
			ValidateSolutionLoaded();

			ValidateIn(0, 1);

			if(Parameters.Count == 0)
			{
				var profiles = ProfileManager.GetProfiles(Out);
				if (profiles != null)
				{
					profiles.ForEach(DumpProfileInfo);
				}
			}
			else
			{
				var name = In(1);

				var profile = ProfileManager.GetProfile(name, Out);

				if(profile != null)
				{
					DumpProfileInfo(profile);
				}
			}
		}

		private void DumpProfileInfo(Profile p)
		{
			Out(string.Format("Profile '{0}'{1}", p.Name, string.IsNullOrEmpty(p.StartUpProject) ? null : string.Format(" (startup {0})",p.StartUpProject)));
			p.Projects.ForEach(project => Out(string.Format(" - {0}", project)));
		}
	}
}