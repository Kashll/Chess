using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Chess.Player;
using Chess.Player.Board;
using Utility;

namespace Chess.UI.ViewModel
{
	public sealed class MainWindowViewModel : NotifyPropertyChangedImpl, IDisposable
	{
		public MainWindowViewModel()
		{
			m_gameState = GameState.NewGame();
			m_boardView = GetDisplayBoard(m_gameState.Board);
			m_gameState.PropertyChanged += GameState_PropertyChanged;
		}

		public static readonly string BoardViewProperty = PropertyNameUtility.GetPropertyName((MainWindowViewModel x) => x.BoardView);
		public ObservableCollection<Square> BoardView
		{
			get
			{
				return m_boardView;
			}
			set
			{
				SetPropertyField(BoardViewProperty, value, ref m_boardView);
			}
		}

		public static readonly string GameStateProperty = PropertyNameUtility.GetPropertyName((MainWindowViewModel x) => x.GameState);
		public GameState GameState
		{
			get
			{
				return m_gameState;
			}
			set
			{
				SetPropertyField(GameStateProperty, value, ref m_gameState);
			}
		}

		public void Dispose()
		{
			m_gameState.PropertyChanged -= GameState_PropertyChanged;
		}

		private void GameState_PropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			if (e.HasChanged(GameState.BoardProperty))
				BoardView = GetDisplayBoard(m_gameState.Board);
		}

		private static ObservableCollection<Square> GetDisplayBoard(Square[,] logicalBoard)
		{
			List<Square> squares = new List<Square>();
			for (int i = 7; i >= 0; i--)
			{
				for (int j = 0; j < 8; j++)
					squares.Add(logicalBoard[i, j]);
			}

			return new ObservableCollection<Square>(squares);
		}

		GameState m_gameState;
		ObservableCollection<Square> m_boardView;
	}
}
