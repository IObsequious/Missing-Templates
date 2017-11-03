// -----------------------------------------------------------------------
// <copyright file="ProjectTemplateWizardBase.cs" company="Ollon, LLC">
//     Copyright © 2017 Ollon, LLC. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using EnvDTE;
using Microsoft.VisualStudio.Shell.Interop;

namespace Microsoft.VisualStudio.TemplateWizard
{
    internal abstract class ProjectTemplateWizardBase : AbstractProjectTemplateWizard
    {
        protected EnvDTE80.DTE2 AutomationObject { get; set; }

        protected Project CurrentProject { get; set; }

        public override void ProjectFinishedGenerating(Project project)
        {
            CurrentProject = project;
        }

        public override void RunFinished()
        {
            
            if (CurrentProject != null)
                RefreshProject(CurrentProject);
        }

        public sealed override void RunStarted(object automationObject, Dictionary<string, string> replacementsDictionary, WizardRunKind runKind, object[] customParams)
        {
            AutomationObject = (EnvDTE80.DTE2)automationObject;
            RunStartedCore(replacementsDictionary, runKind, customParams);
        }

        internal abstract void RunStartedCore(Dictionary<string, string> replacementsDictionary, WizardRunKind runKind, object[] customParams);

        private void RefreshProject(EnvDTE.Project project)
        {
            IVsSolution solution = GlobalServices.Solution;
            IVsSolution4 solution4 = GlobalServices.Solution4;
            solution.GetProjectOfUniqueName(project.UniqueName, out IVsHierarchy hierarchy);
            solution.GetGuidOfProject(hierarchy, out Guid projectGuid);
            SaveSolution();
            solution.SaveSolutionElement((uint)__VSSLNSAVEOPTIONS.SLNSAVEOPT_ForceSave, hierarchy, 0);
            solution4.UnloadProject(ref projectGuid, (uint)_VSProjectUnloadStatus.UNLOADSTATUS_UnloadedByUser);
            solution4.ReloadProject(projectGuid);
            solution4.EnsureProjectIsLoaded(ref projectGuid, SolutionFlags.SelectAndExpandProjectOnLoad);
        }



        public static IVsHierarchy GetHierarchy(ProjectItem projectItem, out uint itemId)
        {
            string uniqueName = projectItem.ContainingProject.UniqueName;
            GlobalServices.Solution.GetProjectOfUniqueName(uniqueName, out IVsHierarchy hierarchy);
            hierarchy.ParseCanonicalName(projectItem.FileNames[0], out itemId);
            return hierarchy;
        }
    }

    internal static class SolutionFlags
    {
        public const uint SelectAndExpandProjectOnLoad = (uint)(__VSBSLFLAGS.VSBSLFLAGS_ExpandProjectOnLoad | __VSBSLFLAGS.VSBSLFLAGS_SelectProjectOnLoad);
    }
}
