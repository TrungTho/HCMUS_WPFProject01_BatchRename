using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
        public int recaseMode = 0; // 0 - Not set, 1 - UPPER, 2 - lower, 3 - Sentence

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

        private void ReplaceFileNameButton_Click(object sender, RoutedEventArgs e)
        {
            string pattern = txtPattern.Text;
            string src = srcFileName.Text;
            string newPattern = txtNewPattern.Text;
            string tmp = "";
            RegexOptions options = RegexOptions.Multiline | RegexOptions.IgnoreCase;
            var myBuilder = new StringBuilder();
            int startPos = 0;

            foreach (Match m in Regex.Matches(src,pattern,options))
            {
                tmp = src.Substring(startPos, m.Index-startPos);
                myBuilder.Append(tmp+newPattern);
                startPos = m.Index + m.Value.Length;
            }
            tmp = src.Substring(startPos);
            myBuilder.Append(tmp);

            desFileName.Text = myBuilder.ToString();
        }

        private void CreateNewCaseButton_Click(object sender, RoutedEventArgs e)
        {
            var screen = new NewCaseOptions();
            screen.BoxChecked += SetCaseMode;
            if (screen.ShowDialog() == true)
            {
                switch (recaseMode)
                {
                    case 1:
                        desFileName.Text = srcFileName.Text.ToUpper();
                        break;
                    case 2:
                        desFileName.Text = srcFileName.Text.ToLower();
                        break;
                    case 3:
                        string tmp = srcFileName.Text.ToLower();
                        string firstChar = tmp.Substring(0, 1).ToUpper();
                        tmp = tmp.Remove(0, 1).Insert(0, firstChar);
                        desFileName.Text = tmp;
                        break;
                }
            }
            else
            {
                recaseMode = 0;
            }
        }

        private void SetCaseMode(int caseMode)
        {
            recaseMode = caseMode;
        }
    }
}
