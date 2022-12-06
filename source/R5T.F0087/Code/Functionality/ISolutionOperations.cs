using System;
using System.Threading.Tasks;

using R5T.T0132;
using R5T.T0153;

using N003 = R5T.T0153.N003;


namespace R5T.F0087
{
	[FunctionalityMarker]
	public partial interface ISolutionOperations : IFunctionalityMarker,
        F0063.ISolutionOperations
	{
        public async Task<WebStaticRazorComponentsSolutionResult> CreateSolution_WebStaticRazorComponents(
            LibraryContext libraryContext,
            bool isRepositoryPrivate,
            string repositoryDirectoryPath)
        {
            var solutionContext = Instances.SolutionContextOperations.GetSolutionContext(
                libraryContext,
                isRepositoryPrivate,
                repositoryDirectoryPath);

            WebStaticRazorComponentsSolutionResult result = default;

            async Task SetupSolutionAction()
            {
                // Create the server project.
                var serverProjectContext = Instances.ProjectContextOperations.GetWebStaticRazorComponentsProjectContext(
                    libraryContext,
                    solutionContext.SolutionDirectoryPath);

                await Instances.ProjectOperations.CreateNewProject_WebStaticRazorComponents(
                    serverProjectContext.ProjectFilePath,
                    serverProjectContext.ProjectDescription);

                F0024.SolutionFileOperator.Instance.AddProject(
                    solutionContext.SolutionFilePath,
                    serverProjectContext.ProjectFilePath);

                result = new WebStaticRazorComponentsSolutionResult
                {
                    SolutionContext = solutionContext,
                    ServerProjectContext = serverProjectContext,
                };
            }

            await this.CreateSolution(
                 solutionContext,
                 SetupSolutionAction);

            return result;
        }

        public async Task<WebBlazorClientAndServerSolutionResult> CreateSolution_WebBlazorClientAndServer(
            LibraryContext libraryContext,
            bool isRepositoryPrivate,
            string repositoryDirectoryPath)
        {
            var solutionContext = Instances.SolutionContextOperations.GetSolutionContext(
                libraryContext,
                isRepositoryPrivate,
                repositoryDirectoryPath);

            WebBlazorClientAndServerSolutionResult result = default;
            
            async Task SetupSolutionAction()
            {
                // Create the server project.
                var serverProjectContext = Instances.ProjectContextOperations.GetWebServerForBlazorClientProjectContext(
                    libraryContext,
                    solutionContext.SolutionDirectoryPath);

                await Instances.ProjectOperations.CreateNewProject_WebServerForBlazorClient(
                    serverProjectContext.ProjectFilePath,
                    serverProjectContext.ProjectDescription);

                F0024.SolutionFileOperator.Instance.AddProject(
                    solutionContext.SolutionFilePath,
                    serverProjectContext.ProjectFilePath);

                // Creeate the client project.
                var clientProjectContext = Instances.ProjectContextOperations.GetWebBlazorClientProjectContext(
                    libraryContext,
                    solutionContext.SolutionDirectoryPath);

                await Instances.ProjectOperations.CreateNewProject_WebBlazorClient(
                    clientProjectContext.ProjectFilePath,
                    clientProjectContext.ProjectDescription);

                F0024.SolutionFileOperator.Instance.AddProject(
                    solutionContext.SolutionFilePath,
                    clientProjectContext.ProjectFilePath);

                // Add the client as a project reference of the server.
                await F0020.ProjectFileOperator.Instance.AddProjectReference_Idempotent(
                    serverProjectContext.ProjectFilePath,
                    clientProjectContext.ProjectFilePath);

                result = new WebBlazorClientAndServerSolutionResult
                {
                    SolutionContext = solutionContext,
                    ServerProjectContext = serverProjectContext,
                    ClientProjectContext = clientProjectContext,
                };
            }

            await this.CreateSolution(
                 solutionContext,
                 SetupSolutionAction);

            return result;
        }

        public async Task<ConsoleWithLibrarySolutionResult> CreateSolution_ConsoleWithLibrary(
            LibraryContext libraryContext,
            bool isRepositoryPrivate,
            string repositoryDirectoryPath)
        {
            var solutionContext = Instances.SolutionContextOperations.GetSolutionContext(
                libraryContext,
                isRepositoryPrivate,
                repositoryDirectoryPath);

            ConsoleWithLibrarySolutionResult result = default;

            async Task SetupSolutionAction()
            {
                // Create the console project.
                var consoleProjectContext = Instances.ProjectContextOperations.GetConsoleProjectContext(
                    libraryContext,
                    solutionContext.SolutionDirectoryPath);

                await Instances.ProjectOperations.CreateNewProject_Console(
                    consoleProjectContext.ProjectFilePath,
                    consoleProjectContext.ProjectDescription);

                F0024.SolutionFileOperator.Instance.AddProject(
                    solutionContext.SolutionFilePath,
                    consoleProjectContext.ProjectFilePath);

                // Creeate the console library project.
                var consoleLibraryProjectContext = Instances.ProjectContextOperations.GetConsoleLibraryProjectContext(
                    libraryContext,
                    solutionContext.SolutionDirectoryPath);

                await Instances.ProjectOperations.CreateNewProject_Library(
                    consoleLibraryProjectContext.ProjectFilePath,
                    consoleLibraryProjectContext.ProjectDescription);

                F0024.SolutionFileOperator.Instance.AddProject(
                    solutionContext.SolutionFilePath,
                    consoleLibraryProjectContext.ProjectFilePath);

                await F0020.ProjectFileOperator.Instance.AddProjectReference_Idempotent(
                    consoleProjectContext.ProjectFilePath,
                    consoleLibraryProjectContext.ProjectFilePath);

                result = new ConsoleWithLibrarySolutionResult
                {
                    SolutionContext = solutionContext,
                    ConsoleProjectContext = consoleProjectContext,
                    ConsoleLibraryProjectContext = consoleLibraryProjectContext,
                };
            }

            await this.CreateSolution(
                 solutionContext,
                 SetupSolutionAction);

            return result;
        }

        public async Task<ConsoleSolutionResult> CreateSolution_Console(
			LibraryContext libraryContext,
            bool isRepositoryPrivate,
            string repositoryDirectoryPath)
        {
            var solutionContext = Instances.SolutionContextOperations.GetSolutionContext(
                libraryContext,
                isRepositoryPrivate,
                repositoryDirectoryPath);

            var solutionResult = await this.CreateSolution_Console(
                libraryContext,
                solutionContext);

            return solutionResult;
        }

        public async Task<ConsoleSolutionResult> CreateSolution_Console(
            LibraryContext libraryContext,
            N003.SolutionContext solutionContext)
        {
            ConsoleSolutionResult result = default;

            async Task SetupSolutionAction()
            {
                // Create the console project.
                var consoleProjectContext = Instances.ProjectContextOperations.GetConsoleProjectContext(
                    libraryContext,
                    solutionContext.SolutionDirectoryPath);

                await Instances.ProjectOperations.CreateNewProject_Console(
                    consoleProjectContext.ProjectFilePath,
                    consoleProjectContext.ProjectDescription);

                Instances.SolutionFileOperator.AddProject(
                    solutionContext.SolutionFilePath,
                    consoleProjectContext.ProjectFilePath);

                result = new ConsoleSolutionResult
                {
                    SolutionContext = solutionContext,
                    ConsoleProjectContext = consoleProjectContext,
                };
            }

            await this.CreateSolution(
                solutionContext,
                SetupSolutionAction);

            return result;
        }

        public async Task<ConsoleSolutionResult> CreateSolution_RazorClassLibrary(
            LibraryContext libraryContext,
            N003.SolutionContext solutionContext)
        {
            ConsoleSolutionResult result = default;

            async Task SetupSolutionAction()
            {
                // Create the Razor class library project.
                var consoleProjectContext = Instances.ProjectContextOperations.GetRazorClassLibraryProjectContext(
                    libraryContext,
                    solutionContext.SolutionDirectoryPath);

                await Instances.ProjectOperations.CreateNewProject_RazorClassLibrary(
                    consoleProjectContext.ProjectFilePath,
                    consoleProjectContext.ProjectDescription);

                Instances.SolutionFileOperator.AddProject(
                    solutionContext.SolutionFilePath,
                    consoleProjectContext.ProjectFilePath);

                result = new ConsoleSolutionResult
                {
                    SolutionContext = solutionContext,
                    ConsoleProjectContext = consoleProjectContext,
                };
            }

            await this.CreateSolution(
                solutionContext,
                SetupSolutionAction);

            return result;
        }

        public async Task CreateSolution(
            N003.SolutionContext solutionContext,
            Func<Task> setupSolutionAction = default)
        {
            await Instances.SolutionFileOperations.CreateNew_VS2022(
                solutionContext.SolutionFilePath);

            await F0000.ActionOperator.Instance.Run(setupSolutionAction);

            // Add all dependency projects.
            await this.AddMissingDependencies(
                solutionContext.SolutionFilePath);
        }
    }
}