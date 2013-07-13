using System.Windows;
using Chess.UI.ViewModel;

namespace Chess.UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
	    public MainWindowViewModel MainWindowViewModel
	    {
			get { return m_mainWindowViewModel; }
	    }

        protected override void OnStartup(StartupEventArgs e)
        {
			MainWindow mainWindow = new MainWindow();
            m_mainWindowViewModel = new MainWindowViewModel();
	        m_mainWindowViewModel = (MainWindowViewModel) mainWindow.DataContext;

			mainWindow.Show();
        }

        MainWindowViewModel m_mainWindowViewModel;
    }
}
