using System;

using R5T.T0142;
using R5T.T0153.N003;


namespace R5T.F0087
{
    /// <summary>
    /// Interface is mutable instead of following the mutable-in-implementing-base-type strategy, since implementing types are forecast to have multiple equally mutable sibling interfaces which would require multiple base types (and base types are impossible in C#).
    /// </summary>
    [DataTypeMarker]
    public interface IHasSolutionContext
    {
        SolutionContext SolutionContext { get; set; }
    }
}
