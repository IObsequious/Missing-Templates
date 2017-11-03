// -----------------------------------------------------------------------
// <copyright file="AbstractWizard.cs" company="Ollon, LLC">
//     Copyright © 2017 Ollon, LLC. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using EnvDTE;
using Microsoft.VisualStudio.Shell.Interop;

namespace Microsoft.VisualStudio.TemplateWizard
{
    public abstract class AbstractWizard : IWizard
    {
        public abstract void BeforeOpeningFile(ProjectItem projectItem);
        public abstract void ProjectFinishedGenerating(Project project);
        public abstract void ProjectItemFinishedGenerating(ProjectItem projectItem);
        public abstract void RunFinished();
        public abstract void RunStarted(object automationObject, Dictionary<string, string> replacementsDictionary, WizardRunKind runKind, object[] customParams);
        public abstract bool ShouldAddProjectItem(string filePath);

        protected static void SaveSolution()
        {
            GlobalServices.Solution.SaveSolutionElement((uint)__VSSLNSAVEOPTIONS.SLNSAVEOPT_ForceSave, null, 0);
        }
    }
}
