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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Project01_BatchRename
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //POCO
        class FileName
        {
            public string name { get; set; }
        }

        //Source of truth - process later :)))
        List<FileName> _fileName = null;

        class FileNameDao
        {
            //get data from database here (...)
        }

        class FileNameBus
        {
            //modify data here
        }

        public MainWindow()
        {
            InitializeComponent();
        }

        private void CreateGUIDNameButton_Click(object sender, RoutedEventArgs e)
        {
            desFileName.Text = Guid.NewGuid().ToString();
        }
    }
}
