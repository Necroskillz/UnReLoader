using System;
using System.Linq;
using System.Collections.Generic;
using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio.Shell;

namespace NecroNet.UnReLoader
{
	public class AppData
	{
		private static AppData _current;
		public static AppData Current
		{
			get { return _current ?? (_current = new AppData()); }
		}

		private DTE2 _dte;
		public DTE2 DTE
		{
			get { return _dte ?? (_dte = Package.GetGlobalService(typeof (DTE)) as DTE2); }
		}
	}
}