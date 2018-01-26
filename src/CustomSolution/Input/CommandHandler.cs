// -----------------------------------------------------------------------
// <copyright file="CommandHandler.cs" company="Ollon, LLC">
//     Copyright © 2017 Ollon, LLC. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

using EnvDTE;

using EnvDTE90;

using Microsoft.Build.Evaluation;
using Microsoft.VisualStudio.LanguageServices;
using Microsoft.VisualStudio.PlatformUI;
using Microsoft.VisualStudio.ProjectSystem;
using Microsoft.VisualStudio.ProjectSystem.Properties;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;

using SaveFileDialog = Microsoft.Win32.SaveFileDialog;

namespace Microsoft.VisualStudio.Input
{
    internal partial class CommandHandler
    {
        private VisualStudioWorkspace _workspace;

        //public override void OnExecuteCmdidAdjustNamespace(object sender, EventArgs e)
        //{
        //    _workspace = GlobalServices.GetComponentModelService<VisualStudioWorkspace>();
        //    OnExecuteOnExecuteCmdidAdjustNamespaceAsync(new CancellationToken());
        //}


        private async System.Threading.Tasks.Task OnExecuteOnExecuteCmdidAdjustNamespaceAsync(CancellationToken cancellationToken)
        {


            var selectedItem = GlobalServices.DTE2.SelectedItems.Item(1);

            if (selectedItem.Project != null)
            {
                string projectName = selectedItem.Project.Name;



                Microsoft.CodeAnalysis.Project project = _workspace.CurrentSolution.Projects.FirstOrDefault(p => p.Name == projectName);
                if (project != null)
                {

                }
            }
        }

        public override void OnExecuteCmdidOpenIntOutput(object sender, EventArgs e)
        {
            ProcessOpenOutput("IntermediateOutputPath");
        }

        public override void OnExecuteCmdidOpenOutput(object sender, EventArgs e)
        {
            ProcessOpenOutput("OutputPath");
        }


        private void ProcessOpenOutput(string propertyName)
        {
            EnvDTE.Project envdteProject = GlobalServices.SelectedItem.Project;
            if (envdteProject != null)
            {
                GlobalServices.Solution.GetProjectOfUniqueName(envdteProject.UniqueName, out var hierarchy);
                if (hierarchy.IsCpsProject())
                {
                    UnconfiguredProject unconfiguredProject = GetUnconfiguredProject((IVsProject)hierarchy);

                    IProjectLockService lockService = unconfiguredProject.ProjectService.Services.ProjectLockService;

                    ThreadHelper.JoinableTaskFactory.RunAsync(async () =>
                    {
                        await OpenOutputAsync(propertyName, unconfiguredProject, lockService).ConfigureAwait(false);
                    });
                }
                else
                {
                    ProjectCollection collection = ProjectCollection.GlobalProjectCollection;

                    Microsoft.Build.Evaluation.Project currentProject = collection.LoadProject(envdteProject.FullName);

                    Microsoft.Build.Execution.ProjectInstance instance = currentProject.CreateProjectInstance(Build.Execution.ProjectInstanceSettings.ImmutableWithFastItemLookup);

                    var property = instance.GetPropertyValue(propertyName);

                    if (Directory.Exists(property))
                    {
                        System.Diagnostics.Process.Start(property);
                    }
                    else
                    {
                        Microsoft.VisualStudio.PlatformUI.MessageDialog.Show(
                            "Error",
                            $"Could not determine the value of the {propertyName} through msbuild with legacy project {envdteProject.Name}.",
                            MessageDialogCommandSet.Ok);

                    }

                    //string projectDirectory = Path.GetDirectoryName(envdteProject.FullName);

                    //string projectOutputPath =
                    //        envdteProject.ConfigurationManager.ActiveConfiguration.Properties.Item(propertyName).Value.ToString();

                    //string fullPath = Path.Combine(projectDirectory, projectOutputPath);

                    //if (Directory.Exists(fullPath))
                    //{
                    //    System.Diagnostics.Process.Start(fullPath);
                    //}
                    //else
                    //{
                    //    //if the directory doesnt exist, open the project directory.
                    //    System.Diagnostics.Process.Start(projectDirectory);
                    //}


                }
            }
        }

        //public override void OnExecuteCmdidOpenOutput(object sender, EventArgs e)
        //{
        //    Project project = GlobalServices.DTE2.SelectedItems?.Item(1)?.Project;
        //    if (project != null)
        //    {
        //        GlobalServices.Solution.GetProjectOfUniqueName(project.UniqueName, out var hierarchy);

        //        if (hierarchy.IsCpsProject())
        //        {
        //            UnconfiguredProject unconfiguredProject = GetUnconfiguredProject((IVsProject)hierarchy);

        //            IProjectLockService lockService = unconfiguredProject.ProjectService.Services.ProjectLockService;

        //            ThreadHelper.JoinableTaskFactory.RunAsync(async () =>
        //            {
        //                await OpenOutputAsync("OutputPath", unconfiguredProject, lockService);


        //            });
        //        }
        //        else
        //        {

        //            //Get the active projects within the solution.
        //            Array _activeProjects = (Array)GlobalServices.DTE2.ActiveSolutionProjects;

        //            //loop through each active project
        //            foreach (Project _activeProject in _activeProjects)
        //            {
        //                //get the directory path based on the project file.
        //                string _projectPath = Path.GetDirectoryName(_activeProject.FullName);

        //                //get the output path based on the active configuration
        //                //Dictionary<string, object> Properties = new Dictionary<string, object>();
        //                //foreach (Property property in _activeProject.ConfigurationManager.ActiveConfiguration.Properties)
        //                //{
        //                //    Properties[property.Name] = property.Value;
        //                //}
        //                string _projectOutputPath =
        //                    _activeProject.ConfigurationManager.ActiveConfiguration.Properties.Item("OutputPath").Value.ToString();

        //                //combine the project path and output path to get the bin path
        //                string _projectBinPath = Path.Combine(_projectPath, _projectOutputPath);

        //                //if the directory exists (already built) then open that directory
        //                //in windows explorer using the diagnostics.process object
        //                if (Directory.Exists(_projectBinPath))
        //                {
        //                    System.Diagnostics.Process.Start(_projectBinPath);
        //                }
        //                else
        //                {
        //                    //if the directory doesnt exist, open the project directory.
        //                    System.Diagnostics.Process.Start(_projectPath);
        //                }
        //            }
        //        }
        //    }



        //}

        private async System.Threading.Tasks.Task OpenOutputAsync(string propertyName, UnconfiguredProject unconfiguredProject, IProjectLockService lockService)
        {
            DirectoryInfo outputPath = await GetFolderPath(propertyName, unconfiguredProject, lockService).ConfigureAwait(false);

            while (!outputPath.Exists)
            {
                outputPath = outputPath.Parent;
            }

            System.Diagnostics.Process.Start("explorer.exe", outputPath.FullName);
        }


        private async Task<DirectoryInfo> GetFolderPath(string propertyName, UnconfiguredProject unconfiguredProject, IProjectLockService lockService)
        {
            string retVal = string.Empty;

            ConfiguredProject configuredProject = unconfiguredProject.Services.ActiveConfiguredProjectProvider.ActiveConfiguredProject;


            using (ProjectWriteLockReleaser access = await lockService.WriteLockAsync())
            {
                Microsoft.Build.Evaluation.Project project = await access.GetProjectAsync(configuredProject).ConfigureAwait(false);
                //Microsoft.Build.Execution.ProjectInstance instance = project.CreateProjectInstance();

                retVal = project.GetPropertyValue(propertyName);

                // party on it, respecting the type of lock you've acquired. 

                // If you're going to change the project in any way, 
                // check it out from SCC first:
                //await access.CheckoutAsync(ConfiguredProject.UnconfiguredProject.FullPath);
            }

            return new DirectoryInfo(retVal);
        }

        private string RepositoryDirectory { get; set; }

        private string SrcDirectory => Path.Combine(RepositoryDirectory, "src");

        private string DocsDirectory => Path.Combine(RepositoryDirectory, "docs");
        private string RulesetsDirectory => Path.Combine(RepositoryDirectory, "build\\rulesets");
        private string StrongNameKeysDirectory => Path.Combine(RepositoryDirectory, "build\\strong name keys");

        public override void OnExecuteCmdidNewSolution(object sender, EventArgs e)
        {
            IVsSolution solution = GlobalServices.Solution;

            IVsSolution4 solution4 = GlobalServices.Solution4;

            IVsSolution5 solution5 = GlobalServices.Solution5;


            NewSolutionInfoInfrastructure infra = NewSolutionInfoFactory.Create();

            bool? result = infra.View.ShowDialog();

            if (result == true)
            {
                RepositoryDirectory = infra.Model.RepositoryDirectory;

                CreateDirectory(@"build\rulesets");
                CreateDirectory(@"build\strong name keys");
                CreateDirectory(@"docs\archive");
                CreateDirectory(@"tools");
                CreateDirectory(@"src\lib");
                CreateDirectory(@"src\shared");


                SolutionFileGenerator.WriteSolutionFiles(
                    RepositoryDirectory,
                    SrcDirectory,
                    RulesetsDirectory,
                    StrongNameKeysDirectory,
                    DocsDirectory
                    );

                solution.CreateSolution(SrcDirectory, infra.Model.SolutionName, SolutionOptions.CreateSilent);

                solution.OpenSolutionFile(SolutionOptions.OpenSilent, Path.Combine(SrcDirectory, $"{infra.Model.SolutionName}.sln"));

                solution.SaveSolutionElement((uint)__VSSLNSAVEOPTIONS.SLNSAVEOPT_ForceSave, null, 0);


                Guid guid = new Guid("FAE04EC0-301F-11D3-BF4B-00C04F79EFBC");
                Guid nguid = Guid.NewGuid();




                Solution3 solution3 = GlobalServices.DTE2.Solution as Solution3;

                EnvDTE.Project buildFolder = solution3.AddSolutionFolder("Build");
                EnvDTE.Project commonFolder = solution3.AddSolutionFolder("Common");
                EnvDTE.Project visualStudio = solution3.AddSolutionFolder("VisualStudio");

                buildFolder.ProjectItems.AddFromFile(Path.Combine(SrcDirectory, "Directory.Build.props"));
                buildFolder.ProjectItems.AddFromFile(Path.Combine(SrcDirectory, "Directory.Build.targets"));
                buildFolder.ProjectItems.AddFromFile(Path.Combine(RepositoryDirectory, ".editorconfig"));
                buildFolder.ProjectItems.AddFromFile(Path.Combine(RepositoryDirectory, ".gitignore"));


            }
        }

        private void CreateDirectory(string path)
        {
            string fullPath = Path.Combine(RepositoryDirectory, path);
            if (Directory.Exists(fullPath))
            {
                Debug.Print($"Deleting directory '{path}' because it already exists...");
                Directory.Delete(path, true);
            }

            Directory.CreateDirectory(fullPath);
        }

        private string GetRepositoryRootDirectory()
        {
            string retVal = string.Empty;
            using (FolderBrowserDialog fbd = new FolderBrowserDialog())
            {
                fbd.Description = "Choose a directory for the solution repository.";
                fbd.RootFolder = Environment.SpecialFolder.MyComputer;
                fbd.ShowNewFolderButton = true;

                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    retVal = fbd.SelectedPath;
                }

            }
            return retVal;
        }

        private UnconfiguredProject GetUnconfiguredProject(IVsProject project)
        {
            IVsBrowseObjectContext context = project as IVsBrowseObjectContext;
            if (context == null)
            { // VC implements this on their DTE.Project.Object
                IVsHierarchy hierarchy = project as IVsHierarchy;
                if (hierarchy != null)
                {
                    object extObject;
                    if (ErrorHandler.Succeeded(hierarchy.GetProperty((uint)VSConstants.VSITEMID.Root, (int)__VSHPROPID.VSHPROPID_ExtObject, out extObject)))
                    {
                        EnvDTE.Project dteProject = extObject as EnvDTE.Project;
                        if (dteProject != null)
                        {
                            context = dteProject.Object as IVsBrowseObjectContext;
                        }
                    }
                }
            }

            return context != null ? context.UnconfiguredProject : null;
        }

        private UnconfiguredProject GetUnconfiguredProject(EnvDTE.Project project)
        {
            IVsBrowseObjectContext context = project as IVsBrowseObjectContext;
            if (context == null && project != null)
            { // VC implements this on their DTE.Project.Object
                context = project.Object as IVsBrowseObjectContext;
            }

            return context != null ? context.UnconfiguredProject : null;
        }
    }


    public static class SolutionOptions
    {
        public const int CreateSilent = 1;
        public const int CreateAndOverwriteExisting = 2;
        public const int CreateTemporary = 4;
        public const int CreateWithDelayedNotification = 8;
        public const int CreateAndHideSolutionNodeAlways = 16;
        public const int CreateDeferredSaveSolution = 32;
        public const int CreateAndSkipSolutionAccessCheck = 64;


        public const uint OpenSilent = 1;
        public const uint OpenAndAddToCurrent = 2;
        public const uint OpenWithoutConversion = 4;


        public const uint BackgroundNone = 0;
        public const uint BackgroundNotCancelable = 1;
        public const uint BackgroundLoadBuildDependencies = 2;
        public const uint BackgroundExpandProjectOnLoad = 4;
        public const uint BackgroundSelectProjectOnLoad = 8;
        public const uint BackgroundLoadAllPendingProjects = 16;
    }

    public enum EmbeddedFile
    {
        BuildCmd,
        CleanCmd,
        ContributorCovenant,
        DirectoryBuildProps,
        DirectoryBuildTargets,
        Editorconfig,
        Gitignore,
        LicenseTxt,
        ReadmeMD,
        RebuildCmd,
        RestoreCmd,
        ShellCmd,
        SolutionRulesRuleSet,
        StrongNameKeySNK,
        Tfignore,
    }

}
