using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
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

            string targetFramework = "net47";
            string targetFrameworkVersion = replacementsDictionary["$targetframeworkversion$"];

            switch (targetFrameworkVersion)
            {
                case "v4.5":
                    targetFramework = "net45";
                    break;
                case "v4.5.2":
                    targetFramework = "net452";
                    break;
                case "v4.6":
                    targetFramework = "net46";
                    break;
                case "v4.7":
                    targetFramework = "net47";
                    break;
                case "v4.7.1":
                    targetFramework = "net471";
                    break;
            }


            infra.ViewModel.AssemblyName = projectName;
            infra.ViewModel.DefaultNamespace = projectName;

            bool? result = infra.View.ShowDialog();

            if (result == true)
            {
                ProjectTemplateDataModel model = infra.Model;

                replacementsDictionary["$AssemblyName$"] = model.AssemblyName;
                replacementsDictionary["$DefaultNamespace$"] = model.DefaultNamespace;
                replacementsDictionary["$TargetFramework$"] = targetFramework;
            }
        }
    }
}
