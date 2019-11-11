using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
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
<<<<<<< HEAD
=======
<<<<<<< Updated upstream
            public string name { get; set; }
=======
>>>>>>> almost-finish2
            public int id { get; set; }
            public string oldName { get; set; }
            public string filePath { get; set; }
            public string newName { get; set; }
            public string err { get; set; }
<<<<<<< HEAD
<<<<<<< HEAD
=======
>>>>>>> almost-finish2

            public event PropertyChangedEventHandler PropertyChanged;
            public void updateListviewUI()
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("newName"));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("err"));
            }
<<<<<<< HEAD
=======
>>>>>>> almost-finish
=======
<<<<<<< Updated upstream
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
>>>>>>> almost-finish2
        }

        BindingList<FileName> _fileNames = new BindingList<FileName>(); //list of file loaded to listview

<<<<<<< HEAD
        List<StringOperations> _prototypes = new List<StringOperations>(); //list of operations can perform
        BindingList<StringOperations> readyLoadOper = new BindingList<StringOperations>(); //list of operation that was chosen
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //load operations to prototype to show in combobox
            var prototype1 = new Replace()
            {
                Arguments = new ReplaceArguments()
                {
                    oldPattern = "old",
                    newPattern = "new"
                }
            };

<<<<<<< HEAD
=======
        //operate operations to data
        private void previewOperations()
        {
            foreach (var datum in _fileNames)
            {
                foreach (var oper in readyLoadOper)
                {
                    
                }
            }
=======
<<<<<<< Updated upstream
<<<<<<< Updated upstream
        class FileNameDao
        {
            //get data from database here (...)
=======
        class FileNameBUS
        {

>>>>>>> Stashed changes
=======
        class FileNameBUS
        {

>>>>>>> Stashed changes
>>>>>>> almost-finish2
        }

        List<StringOperation> _prototypes = new List<StringOperation>(); //list of operations can perform
        BindingList<StringOperation> readyLoadOper = new BindingList<StringOperation>(); //list of operation that was chosen
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
<<<<<<< HEAD
=======
<<<<<<< Updated upstream
            //modify data here
=======
>>>>>>> almost-finish2
            //load operations to prototype to show in combobox
            var prototype1 = new Replace()
            {
                Arguments = new ReplaceArguments()
                {
<<<<<<< HEAD
                    oldPattern = "",
                    newPattern = ""
                }
            };

>>>>>>> almost-finish
=======
                    oldPattern = "old",
                    newPattern = "new"
                }
            };

>>>>>>> almost-finish2
            var prototype2 = new NewCase()
            {
                Arguments = new NewCaseArguments()
                {
<<<<<<< HEAD
<<<<<<< HEAD
                    isUpper = 1,
                    isLower = 0,
                    isSentence=0
=======
                    typeOfNewCase = 1
>>>>>>> almost-finish
=======
                    isUpper = 1,
                    isLower = 0,
                    isSentence=0
>>>>>>> almost-finish2
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
<<<<<<< HEAD
<<<<<<< HEAD
                    isToFirst = 0,
                    isToLast=1
=======
                    typeOfMove = 1
>>>>>>> almost-finish
=======
                    isToFirst = 0,
                    isToLast=1
>>>>>>> almost-finish2
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
<<<<<<< HEAD
<<<<<<< HEAD
            comboBoxToChooseOperations.SelectedIndex= 0;

=======
            comboBoxToChooseOperations.SelectedIndex = 2;
>>>>>>> almost-finish
            listBoxOperations.ItemsSource = readyLoadOper;
=======
            comboBoxToChooseOperations.SelectedIndex= 0;

            listBoxOperations.ItemsSource = readyLoadOper;
<<<<<<< Updated upstream
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
>>>>>>> almost-finish2
        }

        public MainWindow()
        {
            InitializeComponent();
        }

        private void addOperation_Click(object sender, RoutedEventArgs e)
        {
<<<<<<< HEAD
<<<<<<< HEAD
=======
<<<<<<< Updated upstream
            desFileName.Text = Guid.NewGuid().ToString();
=======
>>>>>>> almost-finish2
            StringOperations newOper = comboBoxToChooseOperations.SelectedItem as StringOperations;
            if (newOper != null)
            {
                readyLoadOper.Add(newOper.Clone());
<<<<<<< HEAD
                refreshUI();
                previewOperations();
            }
=======
            StringOperation newOper = comboBoxToChooseOperations.SelectedItem as StringOperation;
            if (newOper != null)
                readyLoadOper.Add(newOper.Clone());
>>>>>>> almost-finish
=======
                refreshListboxUI();
                previewOperations();
            }
>>>>>>> almost-finish2
            else
                MessageBox.Show("Please choose Operation to add!");
            listBoxOperations.SelectedIndex = listBoxOperations.Items.Count - 1;
            listBoxOperations.ScrollIntoView(listBoxOperations.SelectedItem);
<<<<<<< HEAD
=======
<<<<<<< Updated upstream
>>>>>>> Stashed changes
>>>>>>> almost-finish2
        }

        private void downButton(object sender, RoutedEventArgs e)
        {
            if (listBoxOperations.SelectedIndex < readyLoadOper.Count - 1)
            {
<<<<<<< HEAD
                var _tmp = listBoxOperations.SelectedItem as StringOperations;
=======
                var _tmp = listBoxOperations.SelectedItem as StringOperation;
>>>>>>> almost-finish
                int index = listBoxOperations.SelectedIndex;
                readyLoadOper.RemoveAt(index);
                readyLoadOper.Insert(index + 1, _tmp);
                listBoxOperations.SelectedIndex = index + 1;
            }
            else
                if (readyLoadOper.Count == 0)
                    MessageBox.Show("Nothing to move!!!");
                else
                    MessageBox.Show("Ouch!!! Can't move down anymore!");
        }

        private void upButton(object sender, RoutedEventArgs e)
        {
            if (listBoxOperations.SelectedIndex > 0)
            {
<<<<<<< HEAD
                var _tmp = listBoxOperations.SelectedItem as StringOperations;
=======
                var _tmp = listBoxOperations.SelectedItem as StringOperation;
>>>>>>> almost-finish
                int index = listBoxOperations.SelectedIndex;
                readyLoadOper.RemoveAt(index);
                readyLoadOper.Insert(index - 1, _tmp);
                listBoxOperations.SelectedIndex = index - 1;
            }
            else
                if (readyLoadOper.Count == 0)
                    MessageBox.Show("Nothing to move!!!");
                else
                    MessageBox.Show("Ouch!!! Can't move up anymore!");
        }
        
        //main click to start program
        //start to apply all change to all files/folders in listview
        private void fireStartBatchEvent(object sender, RoutedEventArgs e)
<<<<<<< HEAD
        {
            previewOperations();
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
<<<<<<< HEAD

            MessageBox.Show($"{readyLoadOper.Count} operations was saved!");
        }

        //load preset(template of a chain of operations) in a simple txt file
        private void loadPresetButton(object sender, RoutedEventArgs e)
        {
            try
            {
=======
            else
<<<<<<< Updated upstream
            {
                recaseMode = 0;
=======
                if (readyLoadOper.Count == 0)
                    MessageBox.Show("Nothing to move!!!");
                else
                    MessageBox.Show("Ouch!!! Can't move up anymore!");
=======
>>>>>>> Stashed changes
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

<<<<<<< Updated upstream
=======
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

>>>>>>> Stashed changes
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
<<<<<<< Updated upstream

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

=======

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

>>>>>>> Stashed changes
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
>>>>>>> almost-finish2
                using (StreamReader sr = new StreamReader("textfile.txt"))
                {
                    string line;

                    while ((line = sr.ReadLine()) != null)
                    {
                        Console.WriteLine(line);
                    }
<<<<<<< HEAD

                }

                Console.ReadKey();
=======
<<<<<<< Updated upstream

                }

=======

                }

>>>>>>> Stashed changes
                Console.ReadKey();
            }
            catch (Exception error)
            {
                // thong bao loi.
                MessageBox.Show("Khong the doc du lieu tu file da cho: ");
                MessageBox.Show(error.Message);
<<<<<<< Updated upstream
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
>>>>>>> almost-finish2
            }
            catch (Exception error)
            {
                // thong bao loi.
                MessageBox.Show("Khong the doc du lieu tu file da cho: ");
                MessageBox.Show(error.Message);
=======
        {

        }
        //save preset to an simple txt file
        private void savePresetButton(object sender, RoutedEventArgs e)
        {

        }

        //load preset(template of a chain of operations) in a simple txt file
        private void loadPresetButton(object sender, RoutedEventArgs e)
        {
<<<<<<< HEAD

=======
<<<<<<< Updated upstream
            recaseMode = caseMode;
=======
            while (readyLoadOper.Count != 0)
                readyLoadOper.RemoveAt(0);
            MessageBox.Show("Removed All Operations from ListBox.", "Alert");
<<<<<<< Updated upstream
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
>>>>>>> almost-finish2
        }

        private void clearAllOperationsButton(object sender, RoutedEventArgs e)
        {
            while (readyLoadOper.Count != 0)
                readyLoadOper.RemoveAt(0);
            MessageBox.Show("Removed All Operations from ListBox.", "Alert");
        }

<<<<<<< HEAD
        private void resetAllList()
        {
            while (_fileNames.Count != 0)//reset listview and list of data
                _fileNames.RemoveAt(0);
            while (readyLoadOper.Count != 0) //clear all operations in listbox
                readyLoadOper.RemoveAt(0);
        }

        //private string getShortName(string path, string fullname)
=======
<<<<<<< Updated upstream
<<<<<<< Updated upstream
        //private void MoveFileNameButton_Click(object sender, RoutedEventArgs e)
>>>>>>> almost-finish2
        //{
        //    return fullname.Substring(fullname.LastIndexOf(@"\") + 1);
        //}

<<<<<<< HEAD
        private void loadFilesButton(object sender, RoutedEventArgs e)
=======
        private void MoveFileNameButton_Click(object sender, RoutedEventArgs e)
=======
=======
>>>>>>> Stashed changes
        private void loadFilesButton(object sender, RoutedEventArgs e)
>>>>>>> Stashed changes
>>>>>>> almost-finish2
        {
            resetAllList();

            var screen = new CommonOpenFileDialog(); // show dialog to chose directory
            screen.IsFolderPicker = true; //directory pick option

            if (screen.ShowDialog() == CommonFileDialogResult.Ok)
            {
                int tmpID = 0;

                string[] _tmpFiles= Directory.GetFiles(screen.FileName); //get all folders in directory
                foreach (var newFile in _tmpFiles)  //add to data list
                {
                    var newItem = new FileName()
                    {
                        id = ++tmpID,
                        filePath = newFile,
                        oldName = System.IO.Path.GetFileName(newFile),
                        newName = "",
                        err = ""
                    };

                    _fileNames.Add(newItem);
                }

            }

            listviewOfFiles.ItemsSource = _fileNames; //update to UI of listview
        }

        private void loadFoldersButton(object sender, RoutedEventArgs e)
        {
            resetAllList(); 

            var screen = new CommonOpenFileDialog(); // show dialog to chose directory
            screen.IsFolderPicker = true; //directory pick option

            if (screen.ShowDialog() == CommonFileDialogResult.Ok)
            {
                int tmpID = 0;

                string[] _tmpFolders = Directory.GetDirectories(screen.FileName); //get all folders in directory
                foreach (var newFolder in _tmpFolders)  //add to data list
                {
                    var newItem = new FileName()
                    {
                        id = ++tmpID,
                        filePath = newFolder,
                        oldName = System.IO.Path.GetFileName(newFolder),
                        newName = "",
                        err = ""
                    };

                    _fileNames.Add(newItem);
                }

>>>>>>> almost-finish
            }
        }

<<<<<<< HEAD
        private void clearAllOperationsButton(object sender, RoutedEventArgs e)
        {
            while (readyLoadOper.Count != 0)
                readyLoadOper.RemoveAt(0);
            MessageBox.Show("Remove All Operations from ListBox");
        }

        private void resetAllList()
        {
            while (_fileNames.Count != 0)//reset listview and list of data
                _fileNames.RemoveAt(0);
            while (readyLoadOper.Count != 0) //clear all operations in listbox
                readyLoadOper.RemoveAt(0);
        }

        private void loadFilesButton(object sender, RoutedEventArgs e)
        {
            resetAllList();

            var screen = new CommonOpenFileDialog(); // show dialog to chose directory
            screen.IsFolderPicker = true; //directory pick option

            if (screen.ShowDialog() == CommonFileDialogResult.Ok)
            {
                int tmpID = 0;

                string[] _tmpFiles= Directory.GetFiles(screen.FileName); //get all folders in directory
                foreach (var newFile in _tmpFiles)  //add to data list
                {
                    var newItem = new FileName()
                    {
                        id = ++tmpID,
                        filePath = newFile,
                        oldName = System.IO.Path.GetFileName(newFile),
                        newName = "",
                        err = ""
                    };

                    _fileNames.Add(newItem);
                }

            }

            listviewOfFiles.ItemsSource = _fileNames; //update to UI of listview
        }

        private void loadFoldersButton(object sender, RoutedEventArgs e)
        {
            resetAllList(); 

            var screen = new CommonOpenFileDialog(); // show dialog to chose directory
            screen.IsFolderPicker = true; //directory pick option

            if (screen.ShowDialog() == CommonFileDialogResult.Ok)
            {
                int tmpID = 0;

                string[] _tmpFolders = Directory.GetDirectories(screen.FileName); //get all folders in directory
                foreach (var newFolder in _tmpFolders)  //add to data list
                {
                    var newItem = new FileName()
                    {
                        id = ++tmpID,
                        filePath = newFolder,
                        oldName = System.IO.Path.GetFileName(newFolder),
                        newName = "",
                        err = ""
                    };

                    _fileNames.Add(newItem);
                }

=======
            listviewOfFiles.ItemsSource = _fileNames; //update to UI of listview
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button buttonClose && buttonClose.Tag is ListBoxItem lsBoxItem)
            {
                var operToRemove = lsBoxItem.Content as StringOperation;
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
>>>>>>> almost-finish
            }

            listviewOfFiles.ItemsSource = _fileNames; //update to UI of listview
        }

        //operate operations to data
        private void previewOperations()
        {

            foreach (var datum in _fileNames)
            {

                foreach (var oper in readyLoadOper)
                {
                    if (datum.newName == "")
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
            }
        }

        private void refreshUI()
        {
            foreach (var tmp in readyLoadOper)
                tmp.refreshChange();
        }
        private void refreshUI(object sender, TextChangedEventArgs e)
        {
            foreach (var tmp in readyLoadOper)
                tmp.refreshChange();
        }

<<<<<<< HEAD
=======
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
<<<<<<< Updated upstream
>>>>>>> Stashed changes
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

>>>>>>> almost-finish2
        private void HelpAbout_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(this, "Nguyễn Khánh Hoàng - 1712457\n      Trần Trung Thọ      - 1712798", "About Us");
        }
<<<<<<< HEAD
=======

        private void refreshButton_Click(object sender, RoutedEventArgs e)
        {
            previewOperations();
        }
>>>>>>> Stashed changes
>>>>>>> almost-finish2
    }
}
