using System;


namespace R5T.F0087
{
    public class SolutionSetupOperations : ISolutionSetupOperations
    {
        #region Infrastructure

        public static ISolutionSetupOperations Instance { get; } = new SolutionSetupOperations();


        private SolutionSetupOperations()
        {
        }

        #endregion
    }
}
