using System;
using System.Linq;
using System.Collections.Generic;

namespace NecroNet.UnReLoader.Commands
{
	public abstract class CommandBase
	{
		protected List<string> Parameters { get; set; }
		protected Action<string> Out { get; set; }

		protected string In(int index)
		{
			return Parameters[index - 1];
		}

		protected IEnumerable<string> InRange(int from, int? count = null)
		{
			if (Parameters.Count < from) return Enumerable.Empty<string>();

			var query = Parameters.Skip(from - 1);

			return count.HasValue ? query.Take(count.Value) : query;
		}

		protected string GetCommandName()
		{
			return GetType().GetCustomAttributes(typeof (CommandInfoAttribute), false).Cast<CommandInfoAttribute>().First().Name;
		}

		protected void ValidateInMin(int minParameterCount)
		{
			if(Parameters.Count < minParameterCount)
			{
				throw new ParameterValidationException(string.Format("Command {0} requires {1} or more parameters. Type help for list of commands.", GetCommandName(), minParameterCount));
			}
		}

		protected void ValidateInExact(int parameterCount)
		{
			if (Parameters.Count != parameterCount)
			{
				throw new ParameterValidationException(string.Format("Command {0} requires exactly {1} parameter(s). Type help for list of commands.", GetCommandName(), parameterCount));
			}
		}

		protected void ValidateIn(int minParameterCount, int maxParameterCount)
		{
			if (Parameters.Count < minParameterCount && Parameters.Count > maxParameterCount)
			{
				throw new ParameterValidationException(string.Format("Command {0} requires between {1} and {2} parameter(s). Type help for list of commands.", GetCommandName(), minParameterCount, maxParameterCount));
			}
		}

		protected void Validate(bool condition, string message, params object[] args)
		{
			if(!condition)
			{
				throw new ParameterValidationException(string.Format(message, args));
			}
		}

		protected void ValidateSolutionLoaded()
		{
			Validate(AppData.Current.DTE.Solution.Count != 0, Resources.ErrorMessageNoSolutionLoaded);
		}

		public void SetParams(List<string> parameters, Action<string> cout)
		{
			Parameters = parameters;
			Out = cout;
		}

		public abstract void Execute();

		public void ExecuteCore()
		{
			try
			{
				Execute();
			}
			catch (ParameterValidationException e)
			{
				e.Lines.ToList().ForEach(Out);
			}
		}
	}
}