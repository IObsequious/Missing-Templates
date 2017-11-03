using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.VisualStudio.TemplateWizard.UI
{
    public class ProjectTemplateDataModel
    {
        public string AssemblyName { get; set; }

        public string DefaultNamespace { get; set; }
    }

    public class ProjectTemplateDataViewModel : INotifyPropertyChanged
    {
        private ProjectTemplateDataModel _model;

        public ProjectTemplateDataViewModel(ProjectTemplateDataModel model)
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

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChaged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class ProjectTemplateDataInfrastructure
    {
        public ProjectTemplateDataInfrastructure(
            ProjectTemplateDataModel model,
            ProjectTemplateDataViewModel viewModel,
            ProjectTemplateDialogWindow view)
        {
            Model = model;
            ViewModel = viewModel;
            View = view;
        }

        public ProjectTemplateDataModel Model { get; }
        public ProjectTemplateDataViewModel ViewModel { get; }
        public ProjectTemplateDialogWindow View { get; }
    }

    public static class ProjectTemplateDataFactory
    {
        public static ProjectTemplateDataInfrastructure Create()
        {
            ProjectTemplateDataModel model = new ProjectTemplateDataModel();
            ProjectTemplateDataViewModel viewModel = new ProjectTemplateDataViewModel(model);
            ProjectTemplateDialogWindow view = new ProjectTemplateDialogWindow(viewModel);
            return new ProjectTemplateDataInfrastructure(model, viewModel, view);
        }
    }
}
