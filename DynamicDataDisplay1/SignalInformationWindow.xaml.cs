using Microsoft.Research.DynamicDataDisplay;
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

namespace DynamicDataDisplay1
{
    /// <summary>
    /// Interaction logic for SignalInformationWindow.xaml
    /// </summary>
    public partial class SignalInformationWindow : Window
    {
        public SignalInformationWindow()
        {
            InitializeComponent();
            DataContext = new ViewMoedelSignalInformationWindow();
        }
    }
}
