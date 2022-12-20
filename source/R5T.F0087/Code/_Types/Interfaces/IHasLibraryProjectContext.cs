using System;

using R5T.T0142;
using R5T.T0153;


namespace R5T.F0087
{
    [DataTypeMarker]
    public interface IHasLibraryProjectContext
    {
        public ProjectContext LibraryProjectContext { get; set; }
    }
}
