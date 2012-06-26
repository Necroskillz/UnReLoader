using System.IO;
using System.Linq;
using System.Collections.Generic;
using System;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace NecroNet.UnReLoader
{
	public static class ProfileManager
	{
		private static void SaveProfiles(IEnumerable<Profile> profiles)
		{
			var configFilePath = GetConfigFilePath();

			var document = new XDocument(
				new XElement("profiles", 
					profiles.Select(p => 
						new XElement("profile",
							new XElement("startup", p.StartUpProject),
							new XElement("name", p.Name),
							new XElement("projects",
								p.Projects != null ? p.Projects.Select(name => new XElement("project", name)) : null)
							)
						)
					)
				);

			if(!File.Exists(configFilePath))
			{
				var stream = File.Create(configFilePath);
				stream.Close();
			}
			else
			{
				File.SetAttributes(configFilePath, FileAttributes.Normal);
			}

			document.Save(configFilePath);
		}

		private static List<Profile> LoadProfiles(Action<string> cout)
		{
			var configFilePath = GetConfigFilePath();

			if (!File.Exists(configFilePath))
			{
				SaveProfiles(new List<Profile>());
			}

			try
			{
				return (from profile in XDocument.Parse(File.ReadAllText(configFilePath)).Descendants("profile")
				        select new Profile((string) profile.Element("name"), (from project in profile.Descendants("project")
				                                                              select (string) project).ToList(), (string)profile.Element("startup"))).ToList();
			}
			catch (Exception)
			{
				cout(string.Format("Invalid configuration file ({0}), couldn't load profiles.", configFilePath));
			}

			return null;
		}

		private static string GetConfigFilePath()
		{
			var solution = AppData.Current.DTE.Solution;
			var folder = Path.GetDirectoryName(solution.FullName);
			var configFilePath = Path.Combine(folder, "unreloader.xml");
			return configFilePath;
		}

		public static void CreateProfile(string name, IEnumerable<string> projects, string startUpProject, Action<string> cout)
		{
			var profiles = LoadProfiles(cout);

			if(profiles == null)
			{
				return;
			}

			if (profiles.Any(p => string.Equals(p.Name, name, StringComparison.InvariantCultureIgnoreCase)))
			{
				cout("Profile with that name already exists.");
				return;
			}

			profiles.Add(new Profile(name, projects.Distinct().ToList(), startUpProject));

			SaveProfiles(profiles);
		}

		public static void DeleteProfile(string name, Action<string> cout)
		{
			var profiles = LoadProfiles(cout);

			if (profiles == null)
			{
				return;
			}

			if (profiles.All(p => !string.Equals(p.Name, name, StringComparison.InvariantCultureIgnoreCase)))
			{
				cout("Profile with that name doesn't exists.");
				return;
			}

			profiles.RemoveAll(p => string.Equals(p.Name, name, StringComparison.InvariantCultureIgnoreCase));

			SaveProfiles(profiles);
		}

		public static void AddProjectsToProfile(string name, IEnumerable<string> projects, Action<string> cout)
		{
			var profiles = LoadProfiles(cout);

			if(profiles == null)
			{
				return;
			}

			var profile = profiles.FirstOrDefault(p => string.Equals(p.Name, name, StringComparison.InvariantCultureIgnoreCase));

			if(profile == null)
			{
				cout("Profile with that name doesn't exists.");
				return;
			}

			profile.Projects = profile.Projects.Union(projects).ToList();

			SaveProfiles(profiles);
		}

		public static void RemoveProjectsFromProfile(string name, IEnumerable<string> projects, Action<string> cout)
		{
			var profiles = LoadProfiles(cout);

			if (profiles == null)
			{
				return;
			}

			var profile = profiles.FirstOrDefault(p => string.Equals(p.Name, name, StringComparison.InvariantCultureIgnoreCase));

			if (profile == null)
			{
				cout("Profile with that name doesn't exists.");
				return;
			}

			profile.Projects = profile.Projects.Except(projects).ToList();

			SaveProfiles(profiles);
		}

		public static void SetStartupProjectForProfile(string name, string startUpProject, Action<string> cout)
		{
			var profiles = LoadProfiles(cout);

			if (profiles == null)
			{
				return;
			}

			var profile = profiles.FirstOrDefault(p => string.Equals(p.Name, name, StringComparison.InvariantCultureIgnoreCase));

			if (profile == null)
			{
				cout("Profile with that name doesn't exists.");
				return;
			}

			profile.StartUpProject = startUpProject;

			SaveProfiles(profiles);
		}

		public static List<Profile> GetProfiles(Action<string> cout)
		{
			return LoadProfiles(cout);
		}

		public static Profile GetProfile(string name, Action<string> cout)
		{
			var profiles = LoadProfiles(cout);
			var profile = profiles.FirstOrDefault(p => string.Equals(p.Name, name, StringComparison.InvariantCultureIgnoreCase));

			if (profile == null)
			{
				cout("Profile with that name doesn't exists.");
			}

			return profile;
		}
	}
}