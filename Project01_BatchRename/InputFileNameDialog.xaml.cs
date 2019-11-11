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
        public string _filename { get; set; }
        public InputFileNameDialog(string tmp)
        {
            InitializeComponent();

            _filename = tmp;
            textboxFilenameInput.Text = tmp;
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            _filename = textboxFilenameInput.Text;
            if (_filename.Contains(".txt")==false)
                _filename += ".txt";
            DialogResult = true;
            Close();
        }
    }
}
