using System;

using R5T.T0142;
using R5T.T0153;

using SolutionContext = R5T.T0153.N003.SolutionContext;


namespace R5T.F0087
{
    [DataTypeMarker]
    public class WebStaticRazorComponentsSolutionResult :
        IHasSolutionContext,
        IHasServerProjectContext
    {
        public SolutionContext SolutionContext { get; set; }
        public ProjectContext ServerProjectContext { get; set; }
    }
}
