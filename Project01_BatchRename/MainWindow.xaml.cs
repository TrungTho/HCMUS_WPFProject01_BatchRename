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

        private void NormalizeFileNameButton_Click(object sender, RoutedEventArgs e)
        {
            desFileName.Text = Global.NormalizeString(srcFileName.Text);
            
        }

        //private void MoveFileNameButton_Click(object sender, RoutedEventArgs e)
        //{
        //    bool stat = true;
        //    string src = srcFileName.Text;
        //    string decDigits = "0123456789";
        //    char[] arrDigits = decDigits.ToCharArray();
        //    int startPos = src.IndexOfAny(arrDigits);
        //    int endPos = 0;
        //    if (startPos == -1)
        //    {
        //        stat = false;
        //    }
        //    else
        //    {
        //        int count = 1;
        //        endPos = startPos;
        //        do
        //        {
        //            endPos++;

        //            if ((decDigits.IndexOf(src[endPos]) >= 0) || (src[endPos] == '-'))
        //            {
        //                count++;
        //            }
        //            else
        //            {
        //                break;
        //            }

        //        } while (true);
        //        if (count != 13) stat = false;
        //    }
        //    if (!stat)
        //    {
        //        MessageBox.Show("Cannot find ISBN inside file's name!", "Not Found!");
        //        return;
        //    }

        //    string extension = src.Substring(src.LastIndexOf('.'));
        //    src = src.Remove(src.LastIndexOf('.'));
        //    string ISBN = src.Substring(startPos, endPos - startPos);
        //    string bookName;

        //    if (startPos == 0)
        //    {
        //        src = src.Remove(endPos, 1);
        //        bookName = src.Substring(endPos);
        //        desFileName.Text = bookName + " " + ISBN + extension;
        //    }
        //    else
        //    {
        //        bookName = src.Substring(0, startPos - 1);
        //        desFileName.Text = ISBN + " " + bookName + extension;
        //    }
        //}

        private void MoveFileNameButton_Click(object sender, RoutedEventArgs e)
        {
            //"If left blank, move characters at front to last or vice versa. Else, move characters starting from the index to front or to last."
            int numOfChars;
            
            string src = srcFileName.Text;
            string extension = "";
            int dotPos = src.LastIndexOf('.');
            if (dotPos >= 0)
            {
                extension = src.Substring(dotPos);
                src = src.Remove(dotPos);
            }
                      
            try
            {
                numOfChars = int.Parse(txtBoxMoveNumChars.Text);
                if (numOfChars < 0 || numOfChars > src.Length)
                {
                    MessageBox.Show("Number of characters to move is outside valid range!", "Error");
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error at Number of characters");
                return;
            }
            
            if (moveISBNCheckBox.IsChecked == false)
            {
                if (moveRadioToFront.IsChecked == true)
                {
                    string tmp = src.Substring(src.Length - numOfChars);
                    src = src.Remove(src.Length - numOfChars);
                    desFileName.Text = tmp + src + extension;
                }
                if (moveRadioToLast.IsChecked == true)
                {
                    string tmp = src.Substring(0, numOfChars);
                    //src = src.Substring(numOfChars);
                    src = src.Remove(0, numOfChars);
                    desFileName.Text = src + tmp + extension;
                }
            }
            else
            {
                if (moveRadioToFront.IsChecked == true)
                {
                    string tmp = src.Substring(src.Length - numOfChars);
                    src = src.Remove(src.Length - numOfChars);
                    desFileName.Text = tmp.Remove(0, 1) + " " + src + extension;
                }
                if (moveRadioToLast.IsChecked == true)
                {
                    string tmp = src.Substring(0, numOfChars);
                    src = src.Remove(0, numOfChars);
                    desFileName.Text = src + " " + tmp.Remove(tmp.Length - 1) + extension;
                }
            }
        }

        //private void SrcFileName_TextChanged(object sender, TextChangedEventArgs e)
        //{
        //    if (txtBoxMoveNumChars != null)
        //    {
        //        txtBoxMoveNumChars.Text = srcFileName.Text.Length.ToString();
        //    }
        //}

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //txtBoxMoveNumChars.Text = srcFileName.Text.Length.ToString();
        }

    }
}
