using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Microsoft.VisualStudio.TemplateWizard.UI
{
    public class ExtensibilityProjectData
    {
        public string AssemblyName { get; set; }

        public string DefaultNamespace { get; set; }

        public string ExtensionName { get; set; }
    }

    public class ExtensibilityProjectViewModel : INotifyPropertyChanged
    {
        private readonly ExtensibilityProjectData _model;

        public ExtensibilityProjectViewModel(ExtensibilityProjectData model)
        {
            _model = model;
        }

        public string AssemblyName
        {
            get
            {
                return _model.AssemblyName;
            }
            set
            {
                if (_model.AssemblyName != value)
                {
                    _model.AssemblyName = value;
                }
            }
        }

        public string DefaultNamespace
        {
            get
            {
                return _model.DefaultNamespace;
            }
            set
            {
                if (_model.DefaultNamespace != value)
                {
                    _model.DefaultNamespace = value;
                }
            }
        }

        public string ExtensionName
        {
            get
            {
                return _model.ExtensionName;
            }
            set
            {
                if (_model.ExtensionName != value)
                {
                    _model.ExtensionName = value;
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChaged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class ExtensibilityProjectInfrastructure
    {
        public ExtensibilityProjectInfrastructure(
            ExtensibilityProjectData model,
            ExtensibilityProjectViewModel viewModel,
            ExtensibilityProjectView view)
        {
            Model = model;
            ViewModel = viewModel;
            View = view;
        }

        public ExtensibilityProjectData Model { get; }
        public ExtensibilityProjectViewModel ViewModel { get; }
        public ExtensibilityProjectView View { get; }
    }

    public static class ExtensibilityProjectFactory
    {
        public static ExtensibilityProjectInfrastructure Create()
        {
            ExtensibilityProjectData model = new ExtensibilityProjectData();
            ExtensibilityProjectViewModel viewModel = new ExtensibilityProjectViewModel(model);
            ExtensibilityProjectView view = new ExtensibilityProjectView(viewModel);
            return new ExtensibilityProjectInfrastructure(model, viewModel, view);
        }
    }
}
