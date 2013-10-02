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
				cout(string.Format(Resources.ErrorMessageInvalidConfigFile, configFilePath));
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
		
		private static T WithProfiles<T>(Action<string> cout, bool save, Func<List<Profile>, T> action)
			where T : class
		{
			var profiles = LoadProfiles(cout);

			if (profiles == null)
			{
				return null;
			}

			var ret = action(profiles);

			if(save) SaveProfiles(profiles);

			return ret;
		}

		private static void WithProfiles(Action<string> cout, Action<List<Profile>> action)
		{
			WithProfiles(cout, true, p =>
				{
					action(p);
					return (object) null;
				});
		}

		private static T WithProfile<T>(string name, Action<string> cout, bool save, Func<Profile, T> action)
			where T : class
		{
			return WithProfiles(cout, save, profiles =>
				{
					var profile = profiles.FirstOrDefault(p => string.Equals(p.Name, name, StringComparison.OrdinalIgnoreCase));

					if (profile == null)
					{
						cout(Resources.ErrorMessageProfileDoesntExist);
						return null;
					}

					return action(profile);
				});
		}

		private static void WithProfile(string name, Action<string> cout, Action<Profile> action)
		{
			WithProfile(name, cout, true, p =>
				{
					if (p == null)
					{
						cout(Resources.ErrorMessageProfileDoesntExist);
						return null;
					}

					action(p);
					
					return (object) null;
				});
		}

		public static void CreateProfile(string name, IEnumerable<string> projects, string startUpProject, Action<string> cout)
		{
			WithProfiles(cout, profiles =>
				{
					if (profiles.Any(p => string.Equals(p.Name, name, StringComparison.OrdinalIgnoreCase)))
					{
						cout(Resources.ErrorMessageProfileAlreadyExists);
						return;
					}

					profiles.Add(new Profile(name, HashHelper.MakeHashWithOriginal(projects, cout).Values.ToList(), startUpProject));
				});
		}

		public static void DeleteProfile(string name, Action<string> cout)
		{
			WithProfiles(cout, profiles =>
				{
					var profile = profiles.FirstOrDefault(p => string.Equals(p.Name, name, StringComparison.OrdinalIgnoreCase));
					if (profile == null)
					{
						cout(Resources.ErrorMessageProfileDoesntExist);
						return;
					}

					profiles.Remove(profile);
				});
		}

		public static void AddProjectsToProfile(string name, IEnumerable<string> projects, Action<string> cout)
		{
			WithProfile(name, cout, profile =>
				{
					var profileProjects = HashHelper.MakeHash(profile.Projects, cout);

					foreach (var project in projects)
					{
						var upperProjectName = project.ToUpperInvariant();
						if (profileProjects.Contains(upperProjectName))
						{
							cout(string.Format(Resources.ErrorMessageProjectAlreadyInProfile, project, profile.Name));
						}
						else
						{
							profile.Projects.Add(project);
						}
					}
				});
		}

		public static void RemoveProjectsFromProfile(string name, IEnumerable<string> projects, Action<string> cout)
		{
			WithProfile(name, cout, profile =>
				{
					var remove = HashHelper.MakeHashWithOriginal(projects, cout);
					var updatedProjects = new List<string>();

					foreach (var project in profile.Projects)
					{
						var upperProjectName = project.ToUpperInvariant();
						if (remove.ContainsKey(upperProjectName))
						{
							remove.Remove(upperProjectName);
						}
						else
						{
							updatedProjects.Add(project);
						}
					}

					profile.Projects = updatedProjects;

					foreach (var project in remove.Values)
					{
						cout(string.Format(Resources.ErrorMessageProjectWasNotFoundInProfile, project, profile.Name));
					}
				});
		}
		
		public static void SetStartupProjectForProfile(string name, string startUpProject, Action<string> cout)
		{
			WithProfile(name, cout, profile =>
				{
					if (!profile.Projects.Any(p => string.Equals(p, startUpProject, StringComparison.OrdinalIgnoreCase)))
					{
						cout(string.Format(Resources.ErrorMessageProjectWasNotFoundInProfile, startUpProject, profile.Name));
					}

					profile.StartUpProject = startUpProject;
				});
		}

		public static List<Profile> GetProfiles(Action<string> cout)
		{
			return WithProfiles(cout, false, p => p);
		}

		public static Profile GetProfile(string name, Action<string> cout)
		{
			return WithProfile(name, cout, false, p => p);
		}
	}
}