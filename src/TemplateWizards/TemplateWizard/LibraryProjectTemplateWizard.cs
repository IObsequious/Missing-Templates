using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TemplateWizard.UI;

namespace Microsoft.VisualStudio.TemplateWizard
{
    internal class LibraryProjectTemplateWizard : ProjectTemplateWizardBase
    {
        internal override void RunStartedCore(Dictionary<string, string> replacementsDictionary, WizardRunKind runKind, object[] customParams)
        {
            ProjectTemplateDataInfrastructure infra = ProjectTemplateDataFactory.Create();

            string projectName = replacementsDictionary["$safeprojectname$"];

            infra.ViewModel.AssemblyName = projectName;
            infra.ViewModel.DefaultNamespace = projectName;

            bool? result = infra.View.ShowDialog();

            if (result == true)
            {
                ProjectTemplateDataModel model = infra.Model;

                replacementsDictionary["$AssemblyName$"] = model.AssemblyName;
                replacementsDictionary["$DefaultNamespace$"] = model.DefaultNamespace;
            }
        }
    }
}
