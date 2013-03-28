using System;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;

namespace NecroNet.UnReLoader.Commands
{
	public class CommandInfo
	{
		public Type Type { get; set; }
		public CommandMetadataAttribute Metadata { get; set; }
	}

	public static class CommandExecutor
	{
		private static readonly Dictionary<string, CommandInfo> CommandCache = new Dictionary<string, CommandInfo>();

		static CommandExecutor()
		{
			var info = (from type in Assembly.GetExecutingAssembly().GetTypes()
			            let attribute = type.GetCustomAttributes(typeof (CommandMetadataAttribute), false).FirstOrDefault() as CommandMetadataAttribute
			            where attribute != null
			            select new CommandInfo
				                   {
					                   Type = type,
					                   Metadata = attribute
				                   }).ToList();

			foreach (var commandInfo in info)
			{
				CommandCache.Add(commandInfo.Metadata.Name, commandInfo);
				CommandCache.Add(commandInfo.Metadata.ShortHand, commandInfo);
			}
		}

		public static void Execute(string command, Action<string> cout, Action afterExecute)
		{
			var parameters = command.Split(' ').ToList();

			if(parameters.Count == 0)
			{
				cout("Command not specified.");
				return;
			}
			
			var commandName = parameters[0].ToLowerInvariant();
			parameters.RemoveAt(0);
			
			if(!CommandCache.ContainsKey(commandName))
			{
				cout(string.Format("Unknown command {0}.", commandName));
				return;
			}

			var commandInfo = CommandCache[commandName];
			var instance = (CommandBase)Activator.CreateInstance(commandInfo.Type);

			instance.SetParams(parameters, cout);
			Task.Factory.StartNew(instance.ExecuteCore).ContinueWith(t =>
				{
					if(commandInfo.Metadata.WriteDoneMessage)
					{
						cout("Operation completed");
					}

					if (afterExecute != null)
					{
						afterExecute();
					}
				});
		}
	}
}