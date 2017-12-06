// -----------------------------------------------------------------------
// <copyright file="AbstractItemTemplateWizard.cs" company="Ollon, LLC">
//     Copyright © 2017 Ollon, LLC. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using EnvDTE;

namespace Microsoft.VisualStudio.TemplateWizard
{
    internal abstract class AbstractItemTemplateWizard : AbstractWizard
    {
        private bool _bailed = false;

        protected void BailOut()
        {
            _bailed = true;
        }
        public sealed override void ProjectFinishedGenerating(Project project)
        {

        }

        public override void RunFinished()
        {

        }

        public override bool ShouldAddProjectItem(string filePath)
        {
            return !_bailed;
        }

        public override void BeforeOpeningFile(ProjectItem projectItem)
        {
            if (!_bailed)
                BeforeOpeningFileCore(projectItem);
        }

        protected abstract void BeforeOpeningFileCore(ProjectItem projectItem);

        public override void ProjectItemFinishedGenerating(ProjectItem projectItem)
        {
            if (!_bailed)
                ProjectItemFinishedGeneratingCore(projectItem);
        }

        protected abstract void ProjectItemFinishedGeneratingCore(ProjectItem projectItem);
    }
}
