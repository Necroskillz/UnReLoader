using System.Linq;
using System.Collections.Generic;
using System;

namespace NecroNet.UnReLoader
{
	public class Snapshot
	{
		public Snapshot(IEnumerable<string> projects, string startUpProject)
		{
			Projects = projects;
			StartUpProject = startUpProject;
		}

		public IEnumerable<string> Projects { get; set; }
		public string StartUpProject { get; set; }
	}
}