using System;

using R5T.T0142;
using R5T.T0153;

using N003 = R5T.T0153.N003;


namespace R5T.F0087
{
    [DataTypeMarker]
    public class WebBlazorClientAndServerSolutionResult
    {
        public N003.SolutionContext SolutionContext { get; set; }
        public ProjectContext ServerProjectContext { get; set; }
        public ProjectContext ClientProjectContext { get; set; }
    }
}
