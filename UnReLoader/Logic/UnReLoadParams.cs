using System.Linq;
using System.Collections.Generic;
using System;
using EnvDTE;

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
}