using System;
using System.Collections.Generic;
using System.Linq;

namespace NecroNet.UnReLoader.Commands
{
	[CommandMetadata("help", ShortHand = "?")]
	public class HelpCommand : CommandBase
	{
		public override void Execute()
		{
			ValidateInExact(0);

			Out("UnReLoader - tool for unloading and reloading projects in a solution.");
			Out(string.Empty);

			Out("These commands are available:");
			Out(string.Empty);

			Out("help|?                                           - displays this message");
			Out("unload|u <project> [projects]                    - unload specified projects");
			Out("reload|r <project> [projects]                    - reload specified projects");
			Out("load|l <project> [projects]                      - load only specified projects, unload the rest");
			Out("pcreate|pc <name> [projects]                     - create profile (optionaly with projects, automatically sets the first project in the list to startup project for the profile)");
			Out("pdelete|pd <name>                                - delete profile");
			Out("paddprojects|pa <name> <project> [projects]      - add projects to profile");
			Out("premoveprojects|pr <name> <project> [projects]   - remove projects from profile");
			Out("plist|pls [name]                                 - list profiles / display project profile");
			Out("pload|pl <name> [names]                          - loads profile with specified name (supports loading multiple profiles)");
			Out("psetstartup|pstart <name> [project]              - each time the profile is loaded, sets startup project to specified project");
			Out("psnapshot|psnap <name>                           - creates a new profile populated from current state of solution (loaded/unloaded projects, startup project)");
			Out(string.Empty);

			Out("Most effective use of this tool is to create profiles and swap between them with 'pload' command.");
		}
	}
}