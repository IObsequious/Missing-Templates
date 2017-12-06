// -----------------------------------------------------------------------
// <copyright file="ClassHierarchyItemTemplateWizard.cs" company="Ollon, LLC">
//     Copyright (c) 2017 Ollon, LLC. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using EnvDTE;
using Microsoft.VisualStudio.PlatformUI;

namespace Microsoft.VisualStudio.TemplateWizard
{
    internal class ClassHierarchyItemTemplateWizard : ItemTemplateWizardBase
    {
        internal override void RunStarted(ReplacementsDictionary dictionary, WizardRunKind runKind, object[] customParams)
        {
            if (TextInputDialog.Show("New Class Hierarchy",
                "Enter Name for Class Hierarchy",
                "Implementation",
                out string hierarchyName))
            {
                dictionary["HierarchyName"] = hierarchyName;
            }
            else
            {
                BailOut();
            }
        }

        protected override void BeforeOpeningFileCore(ProjectItem projectItem)
        {
        }

        protected override void ProjectItemFinishedGeneratingCore(ProjectItem projectItem)
        {
        }
    }
}
