using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using R5T.T0153;

using N001 = R5T.T0153.N001;

using SolutionContext = R5T.T0153.N003.SolutionContext;


namespace R5T.F0087
{
	public partial interface ISolutionOperations
	{
        public async Task<ProjectContext> AddProjectToSolution_Library(
            string solutionFilePath,
            string projectName,
            string projectDescription)
        {
            var solutionContext = Instances.SolutionContextOperations.GetSolutionContext(
                solutionFilePath);

            var libraryProjectContext = await SolutionOperator.Instance.AddProject(
                solutionContext,
                Instances.ProjectContextOperations.GetProjectContext<N001.SolutionContext>(projectName, projectDescription),
                Instances.ProjectOperations.NewProject_Library);

            return libraryProjectContext;
        }

        public async Task<ProjectContext> AddProjectToSolution_RazorClassLibrary(
            string solutionFilePath,
            string projectName,
            string projectDescription)
        {
            var solutionContext = Instances.SolutionContextOperations.GetSolutionContext(
                solutionFilePath);

            var razorClassLibraryProjectContext = await SolutionOperator.Instance.AddProject(
                solutionContext,
                Instances.ProjectContextOperations.GetProjectContext<N001.SolutionContext>(projectName, projectDescription),
                Instances.ProjectOperations.NewProject_RazorClassLibrary);

            return razorClassLibraryProjectContext;
        }

        public Func<SolutionContext, Task> AddProject_LibraryForConsole<TSolutionResult>(
            LibraryContext libraryContext,
            TSolutionResult solutionResult)
            where TSolutionResult : IHasConsoleProjectContext, IHasLibraryProjectContext
        {
            return SolutionOperations.Instance.AddProject(
                solutionContext => Instances.ProjectContextOperations.GetConsoleLibraryProjectContext(
                    libraryContext,
                    solutionContext.SolutionDirectoryPath),
                async consoleLibraryProjectContext =>
                {
                    await Instances.ProjectOperations.NewProject_Library(
                        consoleLibraryProjectContext.ProjectFilePath,
                        consoleLibraryProjectContext.ProjectDescription);

                    await F0020.ProjectFileOperator.Instance.AddProjectReference_Idempotent(
                        solutionResult.ConsoleProjectContext.ProjectFilePath,
                        consoleLibraryProjectContext.ProjectFilePath);

                    solutionResult.LibraryProjectContext = consoleLibraryProjectContext;
                });
        }

        public Func<SolutionContext, Task> AddProject_Console<TSolutionResult>(
            LibraryContext libraryContext,
            TSolutionResult solutionResult)
            where TSolutionResult : IHasConsoleProjectContext
        {
            return SolutionOperations.Instance.AddProject(
                solutionContext => Instances.ProjectContextOperations.GetConsoleProjectContext(
                    libraryContext,
                    solutionContext.SolutionDirectoryPath),
                async projectContext =>
                {
                    await Instances.ProjectOperations.NewProject_Console(
                        projectContext.ProjectFilePath,
                        projectContext.ProjectDescription);

                    solutionResult.ConsoleProjectContext = projectContext;
                });
        }

        public Func<TSolutionContext, Task<ProjectContext>> AddProject<TSolutionContext>(
            Func<TSolutionContext, ProjectContext> projectContextConstructor,
            IEnumerable<Func<ProjectContext, Task>> projectActions)
            where TSolutionContext : IHasSolutionFilePath, IHasSolutionDirectoryPath
        {
            return solutionContext => SolutionOperator.Instance.AddProject(
                solutionContext,
                projectContextConstructor,
                projectActions);
        }

        public Func<TSolutionContext, Task<ProjectContext>> AddProject<TSolutionContext>(
            Func<TSolutionContext, ProjectContext> projectContextConstructor,
            params Func<ProjectContext, Task>[] projectActions)
            where TSolutionContext : IHasSolutionFilePath, IHasSolutionDirectoryPath
        {
            return solutionContext => SolutionOperator.Instance.AddProject(
                solutionContext,
                projectContextConstructor,
                projectActions.AsEnumerable());
        }

        public Func<SolutionContext, Task<ProjectContext>> AddProject(
            Func<SolutionContext, ProjectContext> projectContextConstructor,
            IEnumerable<Func<ProjectContext, Task>> projectActions)
        {
            return solutionContext => SolutionOperator.Instance.AddProject(
                solutionContext,
                projectContextConstructor,
                projectActions);
        }

        public Func<SolutionContext, Task<ProjectContext>> AddProject(
            Func<SolutionContext, ProjectContext> projectContextConstructor,
            params Func<ProjectContext, Task>[] projectActions)
        {
            return solutionContext => SolutionOperator.Instance.AddProject(
                solutionContext,
                projectContextConstructor,
                projectActions.AsEnumerable());
        }
    }
}