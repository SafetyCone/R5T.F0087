using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

using R5T.F0000;
using R5T.F0020;
using R5T.T0132;
using R5T.T0153;

using SolutionContext = R5T.T0153.N003.SolutionContext;


namespace R5T.F0087
{
    [FunctionalityMarker]
    public partial interface ISolutionOperator : IFunctionalityMarker
    {
        public async Task AddMissingDependencies<TSolutionContext>(TSolutionContext solutionContext)
            where TSolutionContext : IHasSolutionFilePath
        {
            await F0063.SolutionOperations.Instance.AddAllRecursiveProjectReferenceDependencies(
                solutionContext.SolutionFilePath);
        }

        public async Task<ProjectContext> AddProject_WithoutAddMissingDependencies<TSolutionContext>(
            TSolutionContext solutionContext,
            Func<TSolutionContext, ProjectContext> projectContextConstructor,
            IEnumerable<Func<ProjectContext, Task>> projectActions)
            where TSolutionContext : IHasSolutionFilePath, IHasSolutionDirectoryPath
        {
            var projectContext = projectContextConstructor(solutionContext);

            await ActionOperator.Instance.Run(
                projectContext,
                projectActions);

            Instances.SolutionFileOperator.AddProject(
                solutionContext.SolutionFilePath,
                projectContext.ProjectFilePath);

            return projectContext;
        }

        public Task<ProjectContext> AddProject_WithoutAddMissingDependencies<TSolutionContext>(
            TSolutionContext solutionContext,
            Func<TSolutionContext, ProjectContext> projectContextConstructor,
            params Func<ProjectContext, Task>[] projectActions)
            where TSolutionContext : IHasSolutionFilePath, IHasSolutionDirectoryPath
        {
            return this.AddProject(
                solutionContext,
                projectContextConstructor,
                projectActions.AsEnumerable());
        }

        public async Task<ProjectContext> AddProject<TSolutionContext>(
            TSolutionContext solutionContext,
            Func<TSolutionContext, ProjectContext> projectContextConstructor,
            IEnumerable<Func<ProjectContext, Task>> projectActions)
            where TSolutionContext : IHasSolutionFilePath, IHasSolutionDirectoryPath
        {
            var projectContext = await this.AddProject_WithoutAddMissingDependencies(
                solutionContext,
                projectContextConstructor,
                projectActions);

            await this.AddMissingDependencies(solutionContext);

            return projectContext;
        }

        public Task<ProjectContext> AddProject<TSolutionContext>(
            TSolutionContext solutionContext,
            Func<TSolutionContext, ProjectContext> projectContextConstructor,
            params Func<ProjectContext, Task>[] projectActions)
            where TSolutionContext : IHasSolutionFilePath, IHasSolutionDirectoryPath
        {
            return this.AddProject(
                solutionContext,
                projectContextConstructor,
                projectActions.AsEnumerable());
        }

        public async Task CreateSolution(
            SolutionContext solutionContext,
            IEnumerable<Func<SolutionContext, Task>> solutionActions)
        {
            await ActionOperator.Instance.Run(
                solutionContext,
                solutionActions);
        }

        public async Task CreateSolution(
            SolutionContext solutionContext,
            params Func<SolutionContext, Task>[] solutionActions)
        {
            await this.CreateSolution(
                solutionContext,
                solutionActions.AsEnumerable());
        }

        public async Task CreateSolution(
            LibraryContext libraryContext,
            bool isRepositoryPrivate,
            string repositoryDirectoryPath,
            IEnumerable<Func<SolutionContext, Task>> solutionActions)
        {
            var solutionContext = Instances.SolutionContextOperations.GetSolutionContext(
                libraryContext,
                isRepositoryPrivate,
                repositoryDirectoryPath);

            await this.CreateSolution(
                solutionContext,
                solutionActions);
        }

        public async Task CreateSolution(
            LibraryContext libraryContext,
            bool isRepositoryPrivate,
            string repositoryDirectoryPath,
            Func<string, Task> solutionFileConstructor,
            IEnumerable<Func<SolutionContext, Task>> solutionActions)
        {
            await this.CreateSolution(
                libraryContext,
                isRepositoryPrivate,
                repositoryDirectoryPath,
                solutionActions.Prepend(
                    solutionContext => solutionFileConstructor(solutionContext.SolutionFilePath)));
        }

        public async Task CreateSolution(
            LibraryContext libraryContext,
            bool isRepositoryPrivate,
            string repositoryDirectoryPath,
            Func<string, Task> solutionFileConstructor,
            params Func<SolutionContext, Task>[] solutionActions)
        {
            await this.CreateSolution(
                libraryContext,
                isRepositoryPrivate,
                repositoryDirectoryPath,
                solutionActions.Prepend(
                    solutionContext => solutionFileConstructor(solutionContext.SolutionFilePath)));
        }

        public async Task CreateSolution(
            LibraryContext libraryContext,
            bool isRepositoryPrivate,
            string repositoryDirectoryPath,
            Func<string, Task> solutionFileConstructor,
            Func<IEnumerable<Func<SolutionContext, Task>>> solutionActionsConstructor)
        {
            var solutionActions = solutionActionsConstructor();

            await this.CreateSolution(
                libraryContext,
                isRepositoryPrivate,
                repositoryDirectoryPath,
                solutionFileConstructor,
                solutionActions);
        }
    }
}
