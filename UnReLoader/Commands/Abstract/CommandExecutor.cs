using System;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;

namespace NecroNet.UnReLoader.Commands
{
	public static class CommandExecutor
	{
		public static void Execute(string command, Action<string> cout)
		{
			var parameters = command.Split(' ').ToList();

			if(parameters.Count == 0)
			{
				cout("Command not specified.");
				return;
			}
			
			var commandName = parameters[0].ToLowerInvariant();
			parameters.RemoveAt(0);

			var commandInfo = (from type in Assembly.GetExecutingAssembly().GetTypes()
			                   let attribute = type.GetCustomAttributes(typeof (CommandInfoAttribute), false).FirstOrDefault() as CommandInfoAttribute
			                   where attribute != null && attribute.Name == commandName
			                   select new { Type = type, Info = attribute }).FirstOrDefault();
			
			if(commandInfo == null)
			{
				cout(string.Format("Unknown command {0}.", commandName));
				return;
			}

			var instance = (CommandBase)Activator.CreateInstance(commandInfo.Type);

			instance.SetParams(parameters, cout);
			Task.Factory.StartNew(instance.ExecuteCore).ContinueWith(t =>
				{
					if(commandInfo.Info.WriteDoneMessage)
					{
						cout("Operation completed");
					}
				});
		}
	}
}