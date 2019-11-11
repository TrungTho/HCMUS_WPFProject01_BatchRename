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
        }

        BindingList<FileName> _fileNames = new BindingList<FileName>(); //list of file loaded to listview

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
        }

        public MainWindow()
        {
            InitializeComponent();
        }

        private void addOperation_Click(object sender, RoutedEventArgs e)
        {
            StringOperations newOper = comboBoxToChooseOperations.SelectedItem as StringOperations;
            if (newOper != null)
            {
                readyLoadOper.Add(newOper.Clone());
                refreshUI();
                previewOperations();
            }
            else
                MessageBox.Show("Please choose Operation to add!");
            listBoxOperations.SelectedIndex = listBoxOperations.Items.Count - 1;
            listBoxOperations.ScrollIntoView(listBoxOperations.SelectedItem);
        }

        private void downButton(object sender, RoutedEventArgs e)
        {
            if (listBoxOperations.SelectedIndex < readyLoadOper.Count - 1)
            {
                var _tmp = listBoxOperations.SelectedItem as StringOperations;
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
                var _tmp = listBoxOperations.SelectedItem as StringOperations;
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
            }
        }

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
    }
}
