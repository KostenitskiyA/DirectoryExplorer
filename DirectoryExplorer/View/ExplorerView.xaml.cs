using System;
using System.Windows;
using System.IO;
using DirectoryExplorer.ViewModel;

namespace DirectoryExplorer.View
{
    public partial class ExplorerView : Window
    {
        public ExplorerView()
        {
            InitializeComponent();
            ExplorerViewModel viewModel = new ExplorerViewModel();
            this.DataContext = viewModel;

            if (viewModel.Display == null)
            {
                viewModel.Display = new Action(Display);
            }
        }

        public void Display() 
        {
            webBrowser.Navigate(Directory.GetCurrentDirectory() + @"\view.html");
        }
    }
}
