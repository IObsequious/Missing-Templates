using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TemplateWizard.UI;
using EnvDTE;

namespace Microsoft.VisualStudio.TemplateWizard
{
    internal class VSIXProjectTemplateWizard : ProjectTemplateWizardBase
    {

        private string _manifestFileContent;
        public override void ProjectFinishedGenerating(Project project)
        {
            string projectDirectory = Path.GetDirectoryName(project.FileName);
            string manifestFilePath = Path.Combine(projectDirectory, "source.extension.vsixmanifest");
            File.WriteAllText(manifestFilePath, _manifestFileContent);
            project.ProjectItems.AddFromFile(manifestFilePath);
            CurrentProject = project;
        }

        internal override void RunStartedCore(Dictionary<string, string> replacementsDictionary, WizardRunKind runKind, object[] customParams)
        {
            ExtensibilityProjectInfrastructure infra = ExtensibilityProjectFactory.Create();

            string projectName = replacementsDictionary["$safeprojectname$"];

            infra.ViewModel.AssemblyName = projectName;
            infra.ViewModel.DefaultNamespace = projectName;
            infra.ViewModel.ExtensionName = projectName;


            bool? result = infra.View.ShowDialog();

            if (result == true)
            {
                ExtensibilityProjectData model = infra.Model;

                replacementsDictionary["$AssemblyName$"] = model.AssemblyName;
                replacementsDictionary["$DefaultNamespace$"] = model.DefaultNamespace;
                replacementsDictionary["$ExtensionName$"] = model.ExtensionName;

                _manifestFileContent = GetManifestContent(replacementsDictionary["$guid2$"], replacementsDictionary["$registeredorganization$"], model.ExtensionName);
            }
        }
        //HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion
        private string GetManifestContent(string id, string org, string extensionName)
        {
            var sb = new System.Text.StringBuilder();
            sb.AppendLine(@"<?xml version=""1.0"" encoding=""utf-8""?>");
            sb.AppendLine(@"<PackageManifest Version=""2.0.0"" xmlns=""http://schemas.microsoft.com/developer/vsx-schema/2011"" xmlns:d=""http://schemas.microsoft.com/developer/vsx-schema-design/2011"">");
            sb.AppendLine(@"    <Metadata>");
            sb.AppendLine($@"        <Identity Id=""{id}"" Version=""1.0"" Language=""en-US"" Publisher=""{org ?? "CompanyName"}"" />");
            sb.AppendLine($@"        <DisplayName>{extensionName}</DisplayName>");
            sb.AppendLine($@"        <Description xml:space=""preserve"">{extensionName} extension for Microsoft Visual Studio</Description>");
            sb.AppendLine(@"    </Metadata>");
            sb.AppendLine(@"    <Installation>");
            sb.AppendLine(@"        <InstallationTarget Id=""Microsoft.VisualStudio.Community"" Version=""[15.0]"" />");
            sb.AppendLine(@"    </Installation>");
            sb.AppendLine(@"    <Dependencies>");
            sb.AppendLine(@"        <Dependency Id=""Microsoft.Framework.NDP"" DisplayName=""Microsoft .NET Framework"" d:Source=""Manual"" Version=""[4.5,)"" />");
            sb.AppendLine(@"    </Dependencies>");
            sb.AppendLine(@"    <Prerequisites>");
            sb.AppendLine(@"        <Prerequisite Id=""Microsoft.VisualStudio.Component.CoreEditor"" Version=""[15.0,16.0)"" DisplayName=""Visual Studio core editor"" />");
            sb.AppendLine(@"    </Prerequisites>");
            sb.AppendLine(@"</PackageManifest>");
            return sb.ToString();
        }
    }
}
