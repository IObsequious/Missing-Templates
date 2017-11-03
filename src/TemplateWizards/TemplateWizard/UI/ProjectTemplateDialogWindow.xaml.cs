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

namespace Microsoft.VisualStudio.TemplateWizard.UI
{
    /// <summary>
    /// Interaction logic for ProjectTemplateDialogWindow.xaml
    /// </summary>
    public partial class ProjectTemplateDialogWindow : DialogWindow
    {
        private ProjectTemplateDataViewModel _viewModel;

        public ProjectTemplateDialogWindow()
        {
            InitializeComponent();
        }

        public ProjectTemplateDialogWindow(ProjectTemplateDataViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            DataContext = _viewModel;
            PreviewKeyDown += OnPreviewKeyDown;
            AssemblyNameTextBox.Focus();
        }

        private void OnPreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                CloseSad();
            }
        }

        private void CloseHappy()
        {
            DialogResult = true;
            Close();
        }

        private void CloseSad()
        {
            DialogResult = false;
            Close();
        }

        private void OnOkButtonClicked(object sender, RoutedEventArgs e)
        {
            CloseHappy();
        }

        private void OnCancelButtonClicked(object sender, RoutedEventArgs e)
        {
            CloseSad();
        }
    }
}
