using Microsoft.VisualStudio.PlatformUI;
using System.Windows;

namespace $rootnamespace$
{
    public partial class $safeitemrootname$ : DialogWindow
    {
        public $safeitemrootname$($fileinputname$ViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
