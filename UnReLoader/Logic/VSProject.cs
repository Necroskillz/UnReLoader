using System.Linq;
using System.Collections.Generic;
using System;

namespace NecroNet.UnReLoader
{
	public class VSProject
	{
		public VSProject(string name, string path)
		{
			Name = name;
			Path = path;
		}

		public VSProject(string name, string path, bool isLoaded, string uniqueName)
		{
			Name = name;
			Path = path;
			IsLoaded = isLoaded;
			UniqueName = uniqueName;
		}

		public string Name { get; set; }
		public string Path { get; set; }
		public bool IsLoaded { get; set; }
		public string UniqueName { get; set; }
	}
}