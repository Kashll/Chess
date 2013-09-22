using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Threading;
using Chess.Player;
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

			m_moveTimer = new DispatcherTimer();
			m_moveTimer.Tick += new EventHandler(MoveTimer_Tick);
			m_moveTimer.Interval = new TimeSpan(0, 0, 1);
			m_moveTimer.Start();
		}

		private void MoveTimer_Tick(object sender, EventArgs e)
		{
			if (m_mainWindowViewModel.GameState.GameResult == GameResult.InProgress)
			{
				ReadOnlyCollection<Move> moves = m_mainWindowViewModel.GameState.GenerateMoves();

				Random random = new Random();
				Move randomMove = moves.ElementAt(random.Next(moves.Count));

				m_mainWindowViewModel.GameState.MakeMove(randomMove);
			}
		}

		MainWindowViewModel m_mainWindowViewModel;
		DispatcherTimer m_moveTimer;
	}
}
