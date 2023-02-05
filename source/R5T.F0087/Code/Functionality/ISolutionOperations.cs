using System;
using System.Threading.Tasks;

using R5T.F0000;
using R5T.F0085;
using R5T.T0132;
using R5T.T0153;

using N003 = R5T.T0153.N003;


namespace R5T.F0087
{
	[FunctionalityMarker]
	public partial interface ISolutionOperations : IFunctionalityMarker,
        F0063.ISolutionOperations
	{
        public async Task<SolutionResult> NewSolution_RazorClassLibrary(
            LibraryContext libraryContext,
            bool isRepositoryPrivate,
            string repositoryDirectoryPath)
        {
            var solutionResult = new SolutionResult();

            await SolutionOperator.Instance.CreateSolution(
                libraryContext,
                isRepositoryPrivate,
                repositoryDirectoryPath,
                SolutionFileOperations.Instance.NewSolutionFile_VS2022_NoActions,
                SolutionSetupOperations.Instance.SetupSolution_RazorClassLibrary(
                    libraryContext,
                    solutionResult));

            return solutionResult;
        }

        /// <summary>
        /// Creates a solution containing a WinForms application project.
        /// </summary>
        public async Task<SolutionResult> NewSolution_Blog(
            LibraryContext libraryContext,
            bool isRepositoryPrivate,
            string repositoryDirectoryPath)
        {
            var solutionResult = new SolutionResult();

            await SolutionOperator.Instance.CreateSolution(
                libraryContext,
                isRepositoryPrivate,
                repositoryDirectoryPath,
                SolutionFileOperations.Instance.NewSolutionFile_VS2022_NoActions,
                SolutionSetupOperations.Instance.SetupSolution_Blog(
                    libraryContext,
                    solutionResult));

            return solutionResult;
        }

        /// <summary>
        /// Creates a solution containing a WinForms application project.
        /// </summary>
        public async Task<SolutionResult> NewSolution_WindowsFormsApplication(
            LibraryContext libraryContext,
            bool isRepositoryPrivate,
            string repositoryDirectoryPath)
        {
            var solutionResult = new SolutionResult();

            await SolutionOperator.Instance.CreateSolution(
                libraryContext,
                isRepositoryPrivate,
                repositoryDirectoryPath,
                SolutionFileOperations.Instance.NewSolutionFile_VS2022_NoActions,
                SolutionSetupOperations.Instance.SetupSolution_WindowsFormsApplication(
                    libraryContext,
                    solutionResult));

            return solutionResult;
        }

        public async Task<WebStaticRazorComponentsSolutionResult> NewSolution_WebStaticRazorComponents(
            LibraryContext libraryContext,
            bool isRepositoryPrivate,
            string repositoryDirectoryPath)
        {
            var solutionResult = new WebStaticRazorComponentsSolutionResult();

            await SolutionOperator.Instance.CreateSolution(
                libraryContext,
                isRepositoryPrivate,
                repositoryDirectoryPath,
                SolutionFileOperations.Instance.NewSolutionFile_VS2022_NoActions,
                SolutionSetupOperations.Instance.SetupSolution_WebStaticRazorComponents(
                    libraryContext,
                    solutionResult));

            return solutionResult;
        }

        public async Task<WebBlazorClientAndServerSolutionResult> NewSolution_WebBlazorClientAndServer(
            LibraryContext libraryContext,
            bool isRepositoryPrivate,
            string repositoryDirectoryPath)
        {
            var solutionResult = new WebBlazorClientAndServerSolutionResult();

            await SolutionOperator.Instance.CreateSolution(
                libraryContext,
                isRepositoryPrivate,
                repositoryDirectoryPath,
                SolutionFileOperations.Instance.NewSolutionFile_VS2022_NoActions,
                SolutionSetupOperations.Instance.SetupSolution_WebBlazorClientAndServer(
                    libraryContext,
                    solutionResult));

            return solutionResult;
        }

        public async Task<ConsoleWithLibrarySolutionResult> NewSolution_ConsoleWithLibrary(
            LibraryContext libraryContext,
            bool isRepositoryPrivate,
            string repositoryDirectoryPath)
        {
            var solutionResult = new ConsoleWithLibrarySolutionResult();

            await SolutionOperator.Instance.CreateSolution(
                libraryContext,
                isRepositoryPrivate,
                repositoryDirectoryPath,
                SolutionFileOperations.Instance.NewSolutionFile_VS2022_NoActions,
                SolutionSetupOperations.Instance.SetupSolution_ConsoleWithLibrary(
                    libraryContext,
                    solutionResult));

            return solutionResult;
        }

        public async Task<ConsoleSolutionResult> NewSolution_Console(
			LibraryContext libraryContext,
            bool isRepositoryPrivate,
            string repositoryDirectoryPath)
        {
            var solutionResult = new ConsoleSolutionResult();

            await SolutionOperator.Instance.CreateSolution(
                libraryContext,
                isRepositoryPrivate,
                repositoryDirectoryPath,
                SolutionFileOperations.Instance.NewSolutionFile_VS2022_NoActions,
                SolutionSetupOperations.Instance.SetupSolution_Console(
                    libraryContext,
                    solutionResult));

            return solutionResult;
        }

        public async Task<ConsoleSolutionResult> NewSolution_DeployScripts(
            LibraryContext libraryContext,
            bool isRepositoryPrivate,
            string repositoryDirectoryPath,
            string targetProjectName)
        {
            var solutionResult = new ConsoleSolutionResult();

            await SolutionOperator.Instance.CreateSolution(
                libraryContext,
                isRepositoryPrivate,
                repositoryDirectoryPath,
                SolutionFileOperations.Instance.NewSolutionFile_VS2022_NoActions,
                SolutionSetupOperations.Instance.SetupSolution_DeployScripts(
                    libraryContext,
                    solutionResult,
                    targetProjectName));

            return solutionResult;
        }

        public async Task NewSolution(
            N003.SolutionContext solutionContext,
            Func<Task> setupSolutionAction = default)
        {
            await Instances.SolutionFileOperations.NewSolutionFile_VS2022(
                solutionContext.SolutionFilePath);

            await ActionOperator.Instance.Run(setupSolutionAction);

            // Add all dependency projects.
            await this.AddMissingDependencies(
                solutionContext.SolutionFilePath);
        }
    }
}