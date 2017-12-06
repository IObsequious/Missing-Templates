using System;
using System.Collections.Generic;
$if$ ($targetframeworkversion$ >= 3.5)using System.Linq;
$endif$using System.Text;
$if$ ($targetframeworkversion$ >= 4.5)using System.Threading.Tasks;
$endif$using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Microsoft.VisualStudio.PlatformUI;

namespace $rootnamespace$
{
    /// <summary>
    /// Interaction logic for $safeitemrootname$.xaml
    /// </summary>
    public partial class $safeitemrootname$ : DialogWindow
    {
        public $safeitemrootname$()
        {
            InitializeComponent();
        }
    }
}
