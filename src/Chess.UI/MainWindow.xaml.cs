using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Chess.UI.ViewModel;

namespace Chess.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

	    private void MoveEntry_OnKeyDown(object sender, KeyEventArgs e)
	    {
		    if (e.Key == Key.Enter)
		    {
			    TextBox textBox = sender as TextBox;
			    if (textBox != null)
			    {
				    MainWindowViewModel viewModel = textBox.DataContext as MainWindowViewModel;
				    if (viewModel != null)
				    {
					    if (viewModel.AttemptUserMove())
						    viewModel.MoveEntry = String.Empty;
				    }
			    }
		    }
	    }
    }
}
