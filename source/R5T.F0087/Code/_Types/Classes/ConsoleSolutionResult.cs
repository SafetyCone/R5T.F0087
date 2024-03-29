﻿using System;

using R5T.T0142;
using R5T.T0153;

using SolutionContext = R5T.T0153.N003.SolutionContext;


namespace R5T.F0087
{
    [DataTypeMarker]
    public class ConsoleSolutionResult :
        IHasSolutionContext,
        IHasConsoleProjectContext
    {
        public SolutionContext SolutionContext { get; set; }
        public ProjectContext ConsoleProjectContext { get; set; }
    }
}
