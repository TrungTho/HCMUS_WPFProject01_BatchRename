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
        class FileName
        {
            public int id { get; set; }
            public string oldName { get; set; }
            public string filePath { get; set; }
            public string newName { get; set; }
            public string err { get; set; }
        }

        BindingList<FileName> _fileNames = new BindingList<FileName>(); //list of file loaded to listview

        //operate operations to data
        private void previewOperations()
        {
            foreach (var datum in _fileNames)
            {
                foreach (var oper in readyLoadOper)
                {
                    
                }
            }
        }

        List<StringOperation> _prototypes = new List<StringOperation>(); //list of operations can perform
        BindingList<StringOperation> readyLoadOper = new BindingList<StringOperation>(); //list of operation that was chosen
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //load operations to prototype to show in combobox
            var prototype1 = new Replace()
            {
                Arguments = new ReplaceArguments()
                {
                    oldPattern = "",
                    newPattern = ""
                }
            };

            var prototype2 = new NewCase()
            {
                Arguments = new NewCaseArguments()
                {
                    typeOfNewCase = 1
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
                    typeOfMove = 1
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
            comboBoxToChooseOperations.SelectedIndex = 2;
            listBoxOperations.ItemsSource = readyLoadOper;
        }

        public MainWindow()
        {
            InitializeComponent();
        }

        private void addOperation_Click(object sender, RoutedEventArgs e)
        {
            StringOperation newOper = comboBoxToChooseOperations.SelectedItem as StringOperation;
            if (newOper != null)
                readyLoadOper.Add(newOper.Clone());
            else
                MessageBox.Show("Please choose Operation to add!");
            listBoxOperations.SelectedIndex = listBoxOperations.Items.Count - 1;
            listBoxOperations.ScrollIntoView(listBoxOperations.SelectedItem);
        }

        private void downButton(object sender, RoutedEventArgs e)
        {
            if (listBoxOperations.SelectedIndex < readyLoadOper.Count - 1)
            {
                var _tmp = listBoxOperations.SelectedItem as StringOperation;
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
                var _tmp = listBoxOperations.SelectedItem as StringOperation;
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

        }
        //save preset to an simple txt file
        private void savePresetButton(object sender, RoutedEventArgs e)
        {

        }

        //load preset(template of a chain of operations) in a simple txt file
        private void loadPresetButton(object sender, RoutedEventArgs e)
        {

        }

        private void clearAllOperationsButton(object sender, RoutedEventArgs e)
        {
            while (readyLoadOper.Count != 0)
                readyLoadOper.RemoveAt(0);
            MessageBox.Show("Removed All Operations from ListBox.", "Alert");
        }

        private void resetAllList()
        {
            while (_fileNames.Count != 0)//reset listview and list of data
                _fileNames.RemoveAt(0);
            while (readyLoadOper.Count != 0) //clear all operations in listbox
                readyLoadOper.RemoveAt(0);
        }

        //private string getShortName(string path, string fullname)
        //{
        //    return fullname.Substring(fullname.LastIndexOf(@"\") + 1);
        //}

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
            }
        }

        private void HelpAbout_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(this, "Nguyễn Khánh Hoàng - 1712457\n      Trần Trung Thọ      - 1712798", "About Us");
        }
    }
}
