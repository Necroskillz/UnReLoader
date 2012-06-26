﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace NecroNet.UnReLoader.Commands
{
	[CommandInfo("psetstartup")]
	public class SetStartupProjectForProfileCommand : CommandBase
	{
		public override void Execute()
		{
			ValidateSolutionLoaded();

			ValidateInExact(2);

			ProfileManager.SetStartupProjectForProfile(In(1), In(2), Out);
		}
	}
}