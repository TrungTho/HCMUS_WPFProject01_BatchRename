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

namespace Project01_BatchRename
{
    /// <summary>
    /// Interaction logic for InputFileNameDialog.xaml
    /// </summary>
    public partial class InputFileNameDialog : Window
    {
        public string filename { get; set; }
        public InputFileNameDialog(string tmp)
        {
            InitializeComponent();

            textboxToInput.Text = tmp;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            filename = textboxToInput.Text;
            DialogResult = true;
            Close();
        }

        private void cancelButton(object sender, RoutedEventArgs e)
        {

        }
    }
}
