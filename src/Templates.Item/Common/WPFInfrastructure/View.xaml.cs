using System.Windows;

namespace $rootnamespace$
{
    public partial class $safeitemrootname$ : Window
    {
        public $safeitemrootname$()
        {
            InitializeComponent();
        }

        internal $safeitemrootname$($fileinputname$ViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
