using System;
using System.Collections.Generic;
using System.Linq;

namespace NecroNet.UnReLoader.Commands
{
	[CommandInfo("help")]
	public class HelpCommand : CommandBase
	{
		public override void Execute()
		{
			ValidateInExact(0);

			Out("UnReLoader - tool for unloading and reloading projects in a solution.");
			Out(string.Empty);

			Out("These commands are available:");
			Out(string.Empty);

			Out("unload <project> [projects]                    - unload specified projects");
			Out("reload <project> [projects]                    - reload specified projects");
			Out("load <project> [projects]                      - load only specified projects, unload the rest");
			Out("pcreate <name> [projects]                      - create profile (optionaly with projects, automatically sets the first project in the list to startup project for the profile)");
			Out("pdelete <name>                                 - delete profile");
			Out("paddprojects <name> <project> [projects]       - add projects to profile");
			Out("premoveprojects <name> <project> [projects]    - remove projects from profile");
			Out("plist [name]                                   - list profiles / display project profile");
			Out("pload <name> [names]                           - loads profile with specified name (supports loading multiple profiles)");
			Out("psetstartup <name> [project]                   - each time the profile is loaded, sets startup project to specified project");
			Out("psnapshot <name>                               - creates a new profile populated from current state of solution (loaded/unloaded projects, startup project)");
			Out(string.Empty);

			Out("Most effective use of this tool is to create profiles and swap between them with 'pload' command.");
		}
	}
}