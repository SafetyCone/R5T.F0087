using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using R5T.F0000;
using R5T.T0132;
using R5T.T0153;

using SolutionContext = R5T.T0153.N003.SolutionContext;


namespace R5T.F0087
{
    [FunctionalityMarker]
    public partial interface ISolutionSetupOperations : IFunctionalityMarker
    {
        public IEnumerable<Func<SolutionContext, Task>> SetupSolution_RazorClassLibrary(
            LibraryContext libraryContext,
            SolutionResult solutionResult)
        {
            var output = this.SetupSolution_WithStandardActions(
                solutionResult,
                SolutionOperations.Instance.AddProject(
                    solutionContext => Instances.ProjectContextOperations.GetRazorClassLibraryProjectContext(
                        libraryContext,
                        solutionContext.SolutionDirectoryPath),
                    async razorClassLibraryProjectContext =>
                    {
                        await Instances.ProjectOperations.NewProject_RazorClassLibrary(
                            razorClassLibraryProjectContext.ProjectFilePath,
                            razorClassLibraryProjectContext.ProjectDescription);

                        solutionResult.ProjectContext = razorClassLibraryProjectContext;
                    }));

            return output;
        }

        public IEnumerable<Func<SolutionContext, Task>> SetupSolution_WindowsFormsApplication(
            LibraryContext libraryContext,
            SolutionResult solutionResult)
        {
            var output = this.SetupSolution_WithStandardActions(
                solutionResult,
                SolutionOperations.Instance.AddProject(
                    solutionContext => Instances.ProjectContextOperations.GetProjectContext(
                        libraryContext,
                        solutionContext.SolutionDirectoryPath),
                    async winFormsApplicationProjectContext =>
                    {
                        await Instances.ProjectOperations.NewProject_WindowsFormsApplication(
                            winFormsApplicationProjectContext.ProjectFilePath,
                            winFormsApplicationProjectContext.ProjectDescription);

                        solutionResult.ProjectContext = winFormsApplicationProjectContext;
                    }));

            return output;
        }

        public IEnumerable<Func<SolutionContext, Task>> SetupSolution_WebStaticRazorComponents(
            LibraryContext libraryContext,
            WebStaticRazorComponentsSolutionResult solutionResult)
        {
            var output = this.SetupSolution_WithStandardActions(
                solutionResult,
                SolutionOperations.Instance.AddProject(
                    solutionContext => Instances.ProjectContextOperations.GetWebStaticRazorComponentsProjectContext(
                        libraryContext,
                        solutionContext.SolutionDirectoryPath),
                    async serverProjectContext =>
                    {
                        await Instances.ProjectOperations.NewProject_WebStaticRazorComponents(
                            serverProjectContext.ProjectFilePath,
                            serverProjectContext.ProjectDescription);

                        solutionResult.ServerProjectContext = serverProjectContext;
                    }));

            return output;
        }

        public IEnumerable<Func<SolutionContext, Task>> SetupSolution_WebBlazorClientAndServer(
            LibraryContext libraryContext,
            WebBlazorClientAndServerSolutionResult solutionResult)
        {
            var output = this.SetupSolution_WithStandardActions(
                solutionResult,
                SolutionOperations.Instance.AddProject(
                    solutionContext => Instances.ProjectContextOperations.GetWebServerForBlazorClientProjectContext(
                        libraryContext,
                        solutionContext.SolutionDirectoryPath),
                    async serverProjectContext =>
                    {
                        await Instances.ProjectOperations.NewProject_WebServerForBlazorClient(
                            serverProjectContext.ProjectFilePath,
                            serverProjectContext.ProjectDescription);

                        solutionResult.ServerProjectContext = serverProjectContext;
                    }),
                SolutionOperations.Instance.AddProject(
                    solutionContext => Instances.ProjectContextOperations.GetWebBlazorClientProjectContext(
                        libraryContext,
                        solutionContext.SolutionDirectoryPath),
                    async clientProjectContext =>
                    {
                        await Instances.ProjectOperations.NewProject_WebBlazorClient(
                            clientProjectContext.ProjectFilePath,
                            clientProjectContext.ProjectDescription);

                        // Add the client as a project reference of the server.
                        await F0020.ProjectFileOperator.Instance.AddProjectReference_Idempotent(
                            solutionResult.ServerProjectContext.ProjectFilePath,
                            clientProjectContext.ProjectFilePath);

                        solutionResult.ClientProjectContext = clientProjectContext;
                    }));

            return output;
        }

        public IEnumerable<Func<SolutionContext, Task>> SetupSolution_ConsoleWithLibrary(
            LibraryContext libraryContext,
            ConsoleWithLibrarySolutionResult solutionResult)
        {
            var output = this.SetupSolution_WithStandardActions(
                solutionResult,
                SolutionOperations.Instance.AddProject_Console(libraryContext, solutionResult),
                SolutionOperations.Instance.AddProject_LibraryForConsole(libraryContext, solutionResult))
                ;

            return output;
        }

        public IEnumerable<Func<SolutionContext, Task>> SetupSolution_Console(
            LibraryContext libraryContext,
            ConsoleSolutionResult solutionResult)
        {
            var output = this.SetupSolution_WithStandardActions(
                solutionResult,
                SolutionOperations.Instance.AddProject_Console(libraryContext, solutionResult))
                ;

            return output;
        }

        public IEnumerable<Func<SolutionContext, Task>> SetupSolution_DeployScripts(
            LibraryContext libraryContext,
            ConsoleSolutionResult solutionResult,
            string targetProjectName)
        {
            var output = this.SetupSolution_WithStandardActions(
                solutionResult,
                SolutionOperations.Instance.AddProject(
                    solutionContext => Instances.ProjectContextOperations.GetConsoleProjectContext(
                        libraryContext,
                        solutionContext.SolutionDirectoryPath),
                    async projectContext =>
                    {
                        await Instances.ProjectOperations.NewProject_DeployScripts(
                            projectContext.ProjectFilePath,
                            projectContext.ProjectDescription,
                            targetProjectName);

                        solutionResult.ConsoleProjectContext = projectContext;
                    }));

            return output;
        }

        /// <summary>
        /// The standard actions are:
        /// 1. Before, set the solution context in the solution result.
        /// 2. After, add all recursive project dependencies to the solution.
        /// </summary>
        public IEnumerable<Func<SolutionContext, Task>> SetupSolution_WithStandardActions<TSolutionResult>(
            TSolutionResult solutionResult,
            IEnumerable<Func<SolutionContext, Task>> setupSolutionActions)
            where TSolutionResult : IHasSolutionContext
        {
            var output = EnumerableOperator.Instance.From(
                this.SetSolutionContext(solutionResult))
                .AppendRange(setupSolutionActions)
                .Append(
                    SolutionOperator.Instance.AddMissingDependencies)
                ;

            return output;
        }

        /// <inheritdoc cref="SetupSolution_WithStandardActions{TSolutionResult}(TSolutionResult, IEnumerable{Func{SolutionContext, Task}})"/>
        public IEnumerable<Func<SolutionContext, Task>> SetupSolution_WithStandardActions<TSolutionResult>(
            TSolutionResult solutionResult,
            params Func<SolutionContext, Task>[] setupSolutionActions)
            where TSolutionResult : IHasSolutionContext
        {
            return this.SetupSolution_WithStandardActions(
                solutionResult,
                setupSolutionActions.AsEnumerable());
        }

        public IEnumerable<Func<SolutionContext, Task>> SetupSolution_SetSolutionContext<TSolutionResult>(TSolutionResult solutionResult)
            where TSolutionResult : IHasSolutionContext
        {
            var output = EnumerableOperator.Instance.From(
                this.SetSolutionContext(solutionResult));

            return output;
        }

        public Func<SolutionContext, Task> SetSolutionContext<TSolutionResult>(TSolutionResult solutionResult)
            where TSolutionResult : IHasSolutionContext
        {
            return solutionContext =>
            {
                solutionResult.SolutionContext = solutionContext;

                return Task.CompletedTask;
            };
        }
    }
}
