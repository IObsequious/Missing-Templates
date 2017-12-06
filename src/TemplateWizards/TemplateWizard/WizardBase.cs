// -----------------------------------------------------------------------
// <copyright file="AbstractWizard.cs" company="Ollon, LLC">
//     Copyright © 2017 Ollon, LLC. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using EnvDTE;

namespace Microsoft.VisualStudio.TemplateWizard
{
    public abstract class WizardBase : AbstractWizard
    {
        protected EnvDTE80.DTE2 AutomationObject { get; private set; }


        public override void BeforeOpeningFile(ProjectItem projectItem)
        {

        }

        public override void ProjectFinishedGenerating(Project project)
        {

        }

        public override void ProjectItemFinishedGenerating(ProjectItem projectItem)
        {

        }

        public override void RunFinished()
        {

        }

        public sealed override void RunStarted(object automationObject, Dictionary<string, string> replacementsDictionary, WizardRunKind runKind, object[] customParams)
        {
            AutomationObject = (EnvDTE80.DTE2)automationObject;
            var dictionary = ReplacementsDictionary.From(replacementsDictionary);
            RunStarted(dictionary, runKind, customParams);
            replacementsDictionary = dictionary.ToDictionary();
        }

        internal abstract void RunStarted(ReplacementsDictionary dictionary, WizardRunKind runKind, object[] customParams);

        public override bool ShouldAddProjectItem(string filePath)
        {
            return true;
        }
    }
}
