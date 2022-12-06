using System;
using System.Threading.Tasks;

using R5T.T0153;


namespace R5T.F0087
{
	public partial interface ISolutionOperations
	{
        public async Task<ProjectContext> AddProjectToSolution_RazorClassLibrary(
            string solutionFilePath,
            string projectName,
            string projectDescription)
        {
            // Create the Razor class library project.
            var solutionDirectoryPath = Instances.SolutionPathsOperator.GetSolutionDirectoryPath_FromSolutionFilePath(solutionFilePath);

            var razorClassLibraryProjectContext = Instances.ProjectContextOperations.GetProjectContext(
                projectName,
                projectDescription,
                solutionDirectoryPath);

            await Instances.ProjectOperations.CreateNewProject_RazorClassLibrary(
                razorClassLibraryProjectContext.ProjectFilePath,
                razorClassLibraryProjectContext.ProjectDescription);

            Instances.SolutionFileOperator.AddProject(
                solutionFilePath,
                razorClassLibraryProjectContext.ProjectFilePath);

            // Add all dependency projects.
            await this.AddMissingDependencies(
                solutionFilePath);

            return razorClassLibraryProjectContext;
        }
    }
}