using System;
using System.Collections.Generic;
using System.Linq;

namespace NecroNet.UnReLoader
{
	public class Profile
	{
		public Profile()
		{
		}
		
		public Profile(string name, List<string> projects, string startUpProject)
		{
			Name = name;
			Projects = projects;
			StartUpProject = startUpProject;
		}

		public string Name { get; set; }

		public string StartUpProject { get; set; }

		public List<string> Projects { get; set; }
	}
}