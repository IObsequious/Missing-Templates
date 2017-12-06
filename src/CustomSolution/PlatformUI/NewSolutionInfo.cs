using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.VisualStudio.PlatformUI
{
    public class NewSolutionInfo
    {
        public string SolutionName { get; set; }
        public string RepositoryDirectory { get; set; }
    }

    public class NewSolutionInfoViewModel : INotifyPropertyChanged
    {
        private readonly NewSolutionInfo _model;

        public NewSolutionInfoViewModel(NewSolutionInfo model)
        {
            _model = model;
        }

        public string SolutionName
        {
            get
            {
                return _model.SolutionName;
            }
            set
            {
                if (_model.SolutionName != value)
                {
                    _model.SolutionName = value;
                    OnPropertyChanged();
                }
            }
        }

        public string RepositoryDirectory
        {
            get
            {
                return _model.RepositoryDirectory;
            }
            set
            {
                if (_model.RepositoryDirectory != value)
                {
                    _model.RepositoryDirectory = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }


    public class NewSolutionInfoInfrastructure
    {
        public NewSolutionInfoInfrastructure(NewSolutionInfo model, NewSolutionInfoViewModel viwwModel, SolutionDialog view)
        {
            Model = model;
            ViwwModel = viwwModel;
            View = view;
        }

        public NewSolutionInfo Model { get; }

        public NewSolutionInfoViewModel ViwwModel { get; }

        public SolutionDialog View { get; }
    }


    public static class NewSolutionInfoFactory
    {
        public static NewSolutionInfoInfrastructure Create()
        {
            NewSolutionInfo model = new NewSolutionInfo();
            NewSolutionInfoViewModel viewModel = new NewSolutionInfoViewModel(model);
            SolutionDialog view = new SolutionDialog(viewModel);
            return new NewSolutionInfoInfrastructure(model, viewModel, view);
        }
    }
}
