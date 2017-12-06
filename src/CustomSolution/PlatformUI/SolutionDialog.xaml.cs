using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Microsoft.VisualStudio.PlatformUI;

namespace Microsoft.VisualStudio.PlatformUI
{
    /// <summary>
    /// Interaction logic for SolutionDialog.xaml
    /// </summary>
    public partial class SolutionDialog : DialogWindow
    {
        private readonly NewSolutionInfoViewModel _viewModel;

        public SolutionDialog()
        {
            InitializeComponent();

        }
        internal SolutionDialog(NewSolutionInfoViewModel viewModel)
        {
            _viewModel = viewModel;
            InitializeComponent();
            _mainGrid.DataContext = _viewModel;
        }

        private void OnOkButtonClicked(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }

        private void OnCancelButtonClicked(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }


        private void OnBrowseFolderButtonClicked(object sender, RoutedEventArgs e)
        {
            using (System.Windows.Forms.FolderBrowserDialog fbd = new System.Windows.Forms.FolderBrowserDialog())
            {
                fbd.Description = "Choose a directory for the solution repository.";
                fbd.RootFolder = Environment.SpecialFolder.MyComputer;
                fbd.ShowNewFolderButton = true;

                if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    TextBoxRepositoryDirectory.SetValue(TextBox.TextProperty, fbd.SelectedPath);
                }
            }
        }
    }
}
