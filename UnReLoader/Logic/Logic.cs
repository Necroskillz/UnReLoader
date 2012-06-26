using System;
using System.Diagnostics;
using System.Linq;
using System.Collections.Generic;
using EnvDTE;
using EnvDTE80;

namespace NecroNet.UnReLoader
{
	public class UnReLoadParams
	{
		public UnReLoadParams(string solutionName, UIHierarchy solutionExplorer, Action<string> @out)
		{
			SolutionName = solutionName;
			SolutionHierarchy = solutionExplorer;
			Out = @out;
		}

		public string SolutionName { get; set; }
		public UIHierarchy SolutionHierarchy { get; set; }
		public Action<string> Out { get; set; }
	}

	public static class Logic
	{
		private static UnReLoadParams CreateParams(Action<string> cout)
		{
			var dte = AppData.Current.DTE;
			var solutionName = dte.Solution.Properties.Item("Name").Value.ToString();
			var solutionExplorer = dte.Windows.Item(Constants.vsWindowKindSolutionExplorer);

			solutionExplorer.Activate();

			var solutionHierarchy = (UIHierarchy)solutionExplorer.Object;

			return new UnReLoadParams(solutionName, solutionHierarchy, cout);
		}

		public static void UnloadOrReloadProjects(UnReLoad unReLoad, IEnumerable<string> projects, Action<string> cout)
		{
			var hash = new HashSet<string>(projects.Select(p => p.ToLowerInvariant()));

			var parameters = CreateParams(cout);

			ExecuteOnProjectsInSolution(project =>
				{
					var lowerProjectName = project.Name.ToLowerInvariant();
					if (hash.Contains(lowerProjectName))
					{
						UnloadOrReloadProject(project, parameters, unReLoad);
						hash.Remove(lowerProjectName);
					}
				});

			foreach (var project in hash.OrderBy(p => p))
			{
				cout(string.Format(Resources.ErrorMessageProjectWasNotFound, project));
			}
		}

		private static void UnloadOrReloadProject(VSProject project, UnReLoadParams parameters, UnReLoad unReLoad, bool setAsStartup = false)
		{
			try
			{
				var path = string.Format("{0}\\{1}", parameters.SolutionName, project.Path);

				var obj = parameters.SolutionHierarchy.GetItem(path);
				obj.Select(vsUISelectionType.vsUISelectionTypeSelect);
				
				switch (unReLoad)
				{
					case UnReLoad.Unload:
						if (project.IsLoaded)
						{
							parameters.Out(string.Format(Resources.MessageUnloadingProject, project.Name));
							AppData.Current.DTE.ExecuteCommand(SolutionExplorerCommands.UnloadProject);
						}
						break;
					case UnReLoad.Reload:
						if (!project.IsLoaded)
						{
							parameters.Out(string.Format(Resources.MessageReloadingProject, project.Name));
							AppData.Current.DTE.ExecuteCommand(SolutionExplorerCommands.ReloadProject);
						}

						if (setAsStartup)
						{
							parameters.Out(string.Format(Resources.MessageSettingProjectAsStartup, project.Name));
							AppData.Current.DTE.ExecuteCommand(SolutionExplorerCommands.SetAsStartup);
						}
						break;
					default:
						throw new ArgumentOutOfRangeException("unReLoad");
				}
			}
			catch (Exception ex)
			{
				//parameters.Out(string.Format("Failed to {0} project {1}, {2}", unReLoad == UnReLoad.Unload ? "unload" : "reload", project.Name, ex.Message));
			}
		}

		public static void UnloadOrReloadAllProjects(UnReLoad unReLoad, Action<string> cout)
		{
			var parameters = CreateParams(cout);

			ExecuteOnProjectsInSolution(project => UnloadOrReloadProject(project, parameters, unReLoad));
		}

		public static void LoadProjects(IEnumerable<string> projects, string startupProject, Action<string> cout)
		{
			var hash = new HashSet<string>(projects.Select(p => p.ToLowerInvariant()));

			var parameters = CreateParams(cout);
			var lowerStartUpProjectName = string.IsNullOrEmpty(startupProject) ? null : startupProject.ToLowerInvariant();

			ExecuteOnProjectsInSolution(project =>
				{
					var lowerProjectName = project.Name.ToLowerInvariant();
					UnloadOrReloadProject(project, parameters, hash.Contains(lowerProjectName) ? UnReLoad.Reload : UnReLoad.Unload, lowerProjectName == lowerStartUpProjectName);
				});
		}

		public static Snapshot GetSnapshot()
		{
			var dte = AppData.Current.DTE;
		
			string startupProjectUniqueName = null;
			if (dte.Solution.SolutionBuild != null && dte.Solution.SolutionBuild.StartupProjects != null)
			{
				startupProjectUniqueName = (string)((object[])dte.Solution.SolutionBuild.StartupProjects)[0];
			}

			var projects = GetProjectsFlat().Where(p => p.IsLoaded).ToList();
			var startUpProject = projects.FirstOrDefault(p => p.UniqueName == startupProjectUniqueName);

			return new Snapshot(projects.Select(p => p.Name), startUpProject != null ? startUpProject.Name : null);
		}

		private static void ExecuteOnProjectsInSolution(Action<VSProject> action)
		{
			foreach (var project in GetProjectsFlat())
			{
				action(project);
			}
		}

		private static IEnumerable<VSProject> GetProjectsFlat()
		{
			foreach (var project in AppData.Current.DTE.Solution.Projects.Cast<Project>().Where(p => p.Kind != Constants.vsProjectKindMisc))
			{
				if (project.Kind == Constants.vsProjectKindSolutionItems)
				{
					foreach (var item in GetProjectItemsFlat(project.ProjectItems, string.Format("{0}\\", project.Name)))
					{
						yield return item;
					}
				}
				else
				{
					yield return new VSProject(project.Name, project.Name, string.Compare(Constants.vsProjectKindUnmodeled, project.Kind, StringComparison.OrdinalIgnoreCase) != 0, project.UniqueName);
				}
			}
		}

		private static IEnumerable<VSProject> GetProjectItemsFlat(ProjectItems items, string prefix)
		{
			foreach (ProjectItem item in items)
			{
				var vsProject = new VSProject(item.Name, prefix + item.Name);
				if (item.SubProject != null)
				{
					if (item.SubProject.Kind == Constants.vsProjectKindSolutionItems)
					{
						if (item.SubProject.ProjectItems != null)
						{
							foreach (var childItem in GetProjectItemsFlat(item.SubProject.ProjectItems, string.Format("{0}\\", item.Name)))
							{
								yield return childItem;
							}
						}
						
						continue;
					}

					vsProject.IsLoaded = true;
					vsProject.UniqueName = item.SubProject.UniqueName;
				}
				else
				{
					vsProject.IsLoaded = false;
				}

				yield return vsProject;
			}
		}
	}
}