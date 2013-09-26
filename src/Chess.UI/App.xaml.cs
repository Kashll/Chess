using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Threading;
using Chess.Player;
using Chess.UI.ViewModel;
using Utility;

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

			m_mainWindowViewModel.GameState.PropertyChanged += GameState_PropertyChanged;
		}

		protected override void OnExit(ExitEventArgs e)
		{
			m_mainWindowViewModel.GameState.PropertyChanged -= GameState_PropertyChanged;

			base.OnExit(e);
		}

		private void GameState_PropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			if (e.HasChanged(GameState.PlayerTurnProperty))
			{
				if (m_mainWindowViewModel.GameState.GameResult == GameResult.InProgress && m_mainWindowViewModel.GameState.PlayerTurn == c_computerPlayerColor)
				{
					ReadOnlyCollection<Move> moves = m_mainWindowViewModel.GameState.GenerateMoves();

					Random random = new Random();
					Move randomMove = moves.ElementAt(random.Next(moves.Count));

					m_mainWindowViewModel.GameState.MakeMove(randomMove);
				}
			}
		}

		const Color c_computerPlayerColor = Color.Black;

		MainWindowViewModel m_mainWindowViewModel;
	}
}
