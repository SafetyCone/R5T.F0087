using System;


namespace R5T.F0087
{
    public static class Instances
    {
        public static F0089.IProjectContextOperations ProjectContextOperations => F0089.ProjectContextOperations.Instance;
        public static F0084.IProjectOperations ProjectOperations => F0084.ProjectOperations.Instance;
        public static F0089.ISolutionContextOperations SolutionContextOperations => F0089.SolutionContextOperations.Instance;
        public static F0024.ISolutionFileOperator SolutionFileOperator => F0024.SolutionFileOperator.Instance;
        public static F0085.ISolutionFileOperations SolutionFileOperations => F0085.SolutionFileOperations.Instance;
        public static F0050.ISolutionPathsOperator SolutionPathsOperator => F0050.SolutionPathsOperator.Instance;
    }
}