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
        class FileName : INotifyPropertyChanged
        {
<<<<<<< Updated upstream
            public string name { get; set; }
=======
            public int id { get; set; }
            public string oldName { get; set; }
            public string filePath { get; set; }
            public string newName { get; set; }
            public string err { get; set; }

            public event PropertyChangedEventHandler PropertyChanged;
            public void updateListviewUI()
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("newName"));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("err"));
            }
>>>>>>> Stashed changes
        }

        //Source of truth - process later :)))
        List<FileName> _fileName = null;

<<<<<<< Updated upstream
        class FileNameDao
        {
            //get data from database here (...)
=======
        class FileNameBUS
        {

>>>>>>> Stashed changes
        }

        class FileNameBus
        {
<<<<<<< Updated upstream
            //modify data here
=======
            //load operations to prototype to show in combobox
            var prototype1 = new Replace()
            {
                Arguments = new ReplaceArguments()
                {
                    oldPattern = "old",
                    newPattern = "new"
                }
            };

            var prototype2 = new NewCase()
            {
                Arguments = new NewCaseArguments()
                {
                    isUpper = 1,
                    isLower = 0,
                    isSentence=0
                }
            };

            var prototype3 = new Normalize()
            {
                Arguments = new NormalizeArguments() { }
            };

            var prototype4 = new MoveCharacters()
            {
                Arguments = new MoveCharactersArguments()
                {
                    numbersOfChar = 13,
                    isToFirst = 0,
                    isToLast=1
                }
            };

            var prototype5 = new GUIDGenerate()
            {
                Arguments = new GUIDGenerateArguments() { }
            };

            _prototypes.Add(prototype1);
            _prototypes.Add(prototype2);
            _prototypes.Add(prototype3);
            _prototypes.Add(prototype4);
            _prototypes.Add(prototype5);

            comboBoxToChooseOperations.ItemsSource = _prototypes;
            comboBoxToChooseOperations.SelectedIndex= 0;

            listBoxOperations.ItemsSource = readyLoadOper;
>>>>>>> Stashed changes
        }

        public MainWindow()
        {
            InitializeComponent();
            
        }

        private void CreateGUIDNameButton_Click(object sender, RoutedEventArgs e)
        {
<<<<<<< Updated upstream
            desFileName.Text = Guid.NewGuid().ToString();
=======
            StringOperations newOper = comboBoxToChooseOperations.SelectedItem as StringOperations;
            if (newOper != null)
            {
                readyLoadOper.Add(newOper.Clone());
                refreshListboxUI();
                previewOperations();
            }
            else
                MessageBox.Show("Please choose Operation to add!");
            listBoxOperations.SelectedIndex = listBoxOperations.Items.Count - 1;
            listBoxOperations.ScrollIntoView(listBoxOperations.SelectedItem);
>>>>>>> Stashed changes
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
<<<<<<< Updated upstream
            {
                recaseMode = 0;
=======
                if (readyLoadOper.Count == 0)
                    MessageBox.Show("Nothing to move!!!");
                else
                    MessageBox.Show("Ouch!!! Can't move up anymore!");
        }

        private void superdownButton(object sender, RoutedEventArgs e)
        {
            if (listBoxOperations.SelectedIndex > 0)
            {
                var _tmp = listBoxOperations.SelectedItem as StringOperations;
                int index = listBoxOperations.SelectedIndex;
                readyLoadOper.RemoveAt(index);
                readyLoadOper.Insert(readyLoadOper.Count-1,_tmp);
                listBoxOperations.SelectedIndex = readyLoadOper.Count-1;
            }
            else
                if (readyLoadOper.Count == 0)
                MessageBox.Show("Nothing to move!!!");
            else
                MessageBox.Show("Ouch!!! Can't move down anymore!");
        }

        private void superupButton(object sender, RoutedEventArgs e)
        {
            if (listBoxOperations.SelectedIndex > 0)
            {
                var _tmp = listBoxOperations.SelectedItem as StringOperations;
                int index = listBoxOperations.SelectedIndex;
                readyLoadOper.RemoveAt(index);
                readyLoadOper.Insert(0, _tmp);
                listBoxOperations.SelectedIndex = 0;
            }
            else
                if (readyLoadOper.Count == 0)
                    MessageBox.Show("Nothing to move!!!");
                else
                    MessageBox.Show("Ouch!!! Can't move up anymore!");
        }

        private Tuple<string, string> parseName(string filename)
        {
            string tmp = filename;

            string shortName= tmp.Substring(0, tmp.LastIndexOf("."));
            string extensionName = tmp.Substring(tmp.LastIndexOf(".")+1);

            if (tmp == "")
                return Tuple.Create("", "");
            else
                return Tuple.Create( shortName, extensionName);
        }

        private void checkErr()
        {
            for (int i = 0; i < _fileNames.Count; i++)
            {
                int countErr = 1;
                for (int j = i + 1; j < _fileNames.Count; j++)
                    if ((_fileNames[i].newName == _fileNames[j].newName)&&(_fileNames[i].newName!=""))
                    {
                        _fileNames[j].err = $"Duplicate with No. {i}";
                        var parsedName = parseName(_fileNames[j].newName);

                        _fileNames[j].newName = parsedName.Item1 + countErr.ToString() + "." + parsedName.Item2;
                        countErr++;

                        _fileNames[j].updateListviewUI();
                    }
            }

            
        }

        //main click to start program
        //start to apply all change to all files/folders in listview
        private void fireStartBatchEvent(object sender, RoutedEventArgs e)
        {
            foreach (var file in _fileNames)
            {
                file.oldName = file.newName;
            }

        }

        /*combo preset button**/
        //save preset to an simple txt file
        private void savePresetButton(object sender, RoutedEventArgs e)
        {
            string _tmpInputFilename="preset.txt";

            var screen = new InputFileNameDialog(_tmpInputFilename);
            if (screen.ShowDialog()==true)
            {
                _tmpInputFilename = screen._filename;
            }

            using (StreamWriter sw = new StreamWriter(_tmpInputFilename))
            {

                foreach (StringOperations oper in readyLoadOper)
                {
                    if (oper.NameOfOperation == "Replace")
                    {
                        var _tmpArgs = oper.Arguments as ReplaceArguments;
                        sw.WriteLine($"Replace {_tmpArgs.oldPattern} {_tmpArgs.newPattern}");
                    }
                    else
                        if (oper.NameOfOperation == "Move ISBN")
                    {
                        var _tmpArgs = oper.Arguments as MoveCharactersArguments;
                        sw.WriteLine($"Move {_tmpArgs.numbersOfChar} {_tmpArgs.isToLast} {_tmpArgs.isToFirst}");
                    }
                    else
                            if (oper.NameOfOperation == "Normalize")
                    {
                        var _tmpArgs = oper.Arguments as NormalizeArguments;
                        sw.WriteLine($"Normalize");
                    }
                    else
                                if (oper.NameOfOperation == "GUID Generate")
                    {
                        var _tmpArgs = oper.Arguments as GUIDGenerateArguments;
                        sw.WriteLine($"GUID");
                    }
                    else
                    {
                        var _tmpArgs = oper.Arguments as NewCaseArguments;
                        sw.WriteLine($"NewCase {_tmpArgs.isUpper} {_tmpArgs.isLower} {_tmpArgs.isSentence}");

                    }
                }
            }

            MessageBox.Show($"{readyLoadOper.Count} operations was saved!");
        }

        //load preset(template of a chain of operations) in a simple txt file
        private void loadPresetButton(object sender, RoutedEventArgs e)
        {
            try
            {
                using (StreamReader sr = new StreamReader("textfile.txt"))
                {
                    string line;

                    while ((line = sr.ReadLine()) != null)
                    {
                        Console.WriteLine(line);
                    }

                }

                Console.ReadKey();
            }
            catch (Exception error)
            {
                // thong bao loi.
                MessageBox.Show("Khong the doc du lieu tu file da cho: ");
                MessageBox.Show(error.Message);
>>>>>>> Stashed changes
            }
        }

        private void SetCaseMode(int caseMode)
        {
<<<<<<< Updated upstream
            recaseMode = caseMode;
=======
            while (readyLoadOper.Count != 0)
                readyLoadOper.RemoveAt(0);
            MessageBox.Show("Removed All Operations from ListBox.", "Alert");
>>>>>>> Stashed changes
        }

        private void NormalizeFileNameButton_Click(object sender, RoutedEventArgs e)
        {
            desFileName.Text = Global.NormalizeString(srcFileName.Text);
            
        }

<<<<<<< Updated upstream
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
=======
        private void loadFilesButton(object sender, RoutedEventArgs e)
>>>>>>> Stashed changes
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

<<<<<<< Updated upstream
=======
        //operate operations to data
        private void previewOperations()
        {
            if (readyLoadOper.Count == 0)
            {
                foreach (var file in _fileNames)
                {
                    file.newName = file.oldName;
                    file.updateListviewUI();
                }
            }
            else
            {
                bool firstTimeRun = true;

                foreach (var oper in readyLoadOper)
                {
                    foreach (var datum in _fileNames)
                    {
                        if (firstTimeRun)
                            oper.Arguments.Origin = datum.oldName;
                        else
                            oper.Arguments.Origin = datum.newName;

                        datum.newName = oper.Operate();
                        datum.updateListviewUI();


                        //if (oper.NameOfOperation == "Replace")
                        //{
                        //    var _tmpArgs = oper.Arguments as ReplaceArguments;
                        //    datum.newName = oper.Operate();
                        //}
                        //else
                        //    if (oper.NameOfOperation == "Move ISBN")
                        //    {
                        //        var _tmpArgs = oper.Arguments as MoveCharactersArguments;
                        //        datum.newName = oper.Operate();
                        //    }
                        //    else
                        //        if (oper.NameOfOperation == "Normalize")
                        //        {
                        //            var _tmpArgs = oper.Arguments as NormalizeArguments;
                        //            datum.newName = oper.Operate();
                        //        }    
                        //        else
                        //            if (oper.NameOfOperation == "GUID Generate")
                        //            {
                        //                var _tmpArgs = oper.Arguments as GUIDGenerateArguments;
                        //                datum.newName = oper.Operate();
                        //            }
                        //            else
                        //                {
                        //                    var _tmpArgs = oper.Arguments as NewCaseArguments;
                        //                    datum.newName = oper.Operate();
                        //                }
                    }

                    firstTimeRun = false;

                    checkErr();
                }
            }
        }

        private void refreshListboxUI()
        {
            foreach (var tmp in readyLoadOper)
                tmp.refreshChanged();
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button buttonClose && buttonClose.Tag is ListBoxItem lsBoxItem)
            {
                var operToRemove = lsBoxItem.Content as StringOperations;
                readyLoadOper.Remove(operToRemove);
            }
        }

        private void FileExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            if (MessageBox.Show(this, "Are you sure you want to exit?", "Exit", MessageBoxButton.YesNo) != MessageBoxResult.Yes)
            {
                e.Cancel = true;
            }
        }

        private void HelpAbout_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(this, "Nguyễn Khánh Hoàng - 1712457\n      Trần Trung Thọ      - 1712798", "About Us");
        }

        private void refreshButton_Click(object sender, RoutedEventArgs e)
        {
            previewOperations();
        }
>>>>>>> Stashed changes
    }
}
