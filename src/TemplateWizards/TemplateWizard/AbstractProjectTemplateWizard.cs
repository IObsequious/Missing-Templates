// -----------------------------------------------------------------------
// <copyright file="AbstractProjectTemplateWizard.cs" company="Ollon, LLC">
//     Copyright © 2017 Ollon, LLC. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using EnvDTE;

namespace Microsoft.VisualStudio.TemplateWizard
{
    internal abstract class AbstractProjectTemplateWizard : AbstractWizard
    {
        public sealed override void BeforeOpeningFile(ProjectItem projectItem)
        {
        }

        public sealed override void ProjectItemFinishedGenerating(ProjectItem projectItem)
        {
        }

        public sealed override bool ShouldAddProjectItem(string filePath)
        {
            return false;
        }
    }
}
