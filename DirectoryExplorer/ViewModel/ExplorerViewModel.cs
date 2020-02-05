using System;
using System.Windows.Forms;
using System.Windows.Input;
using DirectoryExplorer.Infrastructure;
using DirectoryExplorer.Model;

namespace DirectoryExplorer.ViewModel
{
    class ExplorerViewModel : ViewModel
    {
        Explorer explorer;

        private string _directoryPath;
        public string DirectoryPath
        {
            get
            {
                return _directoryPath;
            }
            set
            {
                _directoryPath = value;
                OnPropertyChanged("DirectoryPath");
            }
        }
        public Action Display { get; set; }

        // Команда выбора директории
        #region SelectCommand 
        RelayCommand _selectCommand;
        public ICommand SelectCommand
        {
            get
            {
                if (_selectCommand == null) { _selectCommand = new RelayCommand(ExecuteSelectCommand, CanExecuteSelectCommand); }
                return _selectCommand;
            }
        }
        public bool CanExecuteSelectCommand(object parameter)
        {
            return true;
        }
        public void ExecuteSelectCommand(object parameter)
        {
            using (FolderBrowserDialog browserDialog = new FolderBrowserDialog())
            {
                if (browserDialog.ShowDialog() == DialogResult.OK)
                {
                    DirectoryPath = browserDialog.SelectedPath;
                }
            }

            explorer = new Explorer(DirectoryPath);
            explorer.Find();
        }
        #endregion

        // Команда открытия директории
        #region OpenCommand 
        RelayCommand _openCommand;
        public ICommand OpenCommand
        {
            get
            {
                if (_openCommand == null) { _openCommand = new RelayCommand(ExecuteOpenCommand, CanExecuteOpenCommand); }
                return _openCommand;
            }
        }
        public bool CanExecuteOpenCommand(object parameter)
        {
            if (DirectoryPath != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public void ExecuteOpenCommand(object parameter)
        {
            explorer.Open();
            Display();
        }        
        #endregion
    }
}
