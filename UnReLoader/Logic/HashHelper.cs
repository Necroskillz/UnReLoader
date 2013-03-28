using System;
using System.Linq;
using System.Collections.Generic;

namespace NecroNet.UnReLoader
{
	public static class HashHelper
	{
		public static Dictionary<string, string> MakeHashWithOriginal(IEnumerable<string> input, Action<string> cout)
		{
			var hash = new Dictionary<string, string>();

			foreach (var s in input)
			{
				var key = s.ToUpperInvariant();

				if (hash.ContainsKey(key))
				{
					cout(string.Format(Resources.ErrorMessageDuplicateProject, s));
				}
				else
				{
					hash.Add(key, s);
				}
			}

			return hash;
		}

		public static HashSet<string> MakeHash(IEnumerable<string> input, Action<string> cout)
		{
			var hash = new HashSet<string>();

			foreach (var s in input)
			{
				var key = s.ToUpperInvariant();

				if (hash.Contains(key))
				{
					cout(string.Format(Resources.ErrorMessageDuplicateProject, s));
				}
				else
				{
					hash.Add(key);
				}
			}

			return hash;
		} 
	}
}