using System;


namespace R5T.F0087
{
	public class SolutionOperations : ISolutionOperations
	{
		#region Infrastructure

	    public static ISolutionOperations Instance { get; } = new SolutionOperations();

	    private SolutionOperations()
	    {
        }

	    #endregion
	}
}