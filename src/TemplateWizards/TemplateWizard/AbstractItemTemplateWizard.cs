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
        public sealed override void ProjectFinishedGenerating(Project project)
        {

        }

        public override void RunFinished()
        {

        }

        public override bool ShouldAddProjectItem(string filePath)
        {
            return true;
        }
    }
}
