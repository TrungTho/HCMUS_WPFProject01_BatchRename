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
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class NewCaseOptions : Window
    {
        public NewCaseOptions()
        {
            InitializeComponent();
        }

        public delegate void BoxStateDelegate(int caseMode);
        public event BoxStateDelegate BoxChecked = null;

        private void RadioUpperCase_Checked(object sender, RoutedEventArgs e)
        {
            BoxChecked?.Invoke(Global.upperCase);
        }

        private void RadioLowerCase_Checked(object sender, RoutedEventArgs e)
        {
            BoxChecked?.Invoke(Global.lowerCase);
        }

        private void RadioSentenceCase_Checked(object sender, RoutedEventArgs e)
        {
            BoxChecked?.Invoke(Global.sentenceCase);
        }


        private void BtnOK_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }


    }
}
