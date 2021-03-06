using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using Chess.Player;
using Chess.Player.Board;
using Chess.Player.Utility;
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
				SetPropertyAndField(BoardViewProperty, value, ref m_boardView);
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
				SetPropertyAndField(GameStateProperty, value, ref m_gameState);
			}
		}

		public static readonly string MoveEntryProperty = PropertyNameUtility.GetPropertyName((MainWindowViewModel x) => x.MoveEntry);
		public string MoveEntry
		{
			get
			{
				return m_moveEntry;
			}
			set
			{
				SetPropertyAndField(MoveEntryProperty, value, ref m_moveEntry);
			}
		}

		public bool AttemptUserMoveFromEntryHelper()
		{
			Move move;
			bool isValidMoveNotation = TryGetUserMove(m_moveEntry, out move);

			return isValidMoveNotation && m_gameState.MakeMove(move);
		}

		public bool AttemptUserMove(Coordinate from, Coordinate to)
		{
			Move move;
			bool isValidMove = TryGetUserMove(from, to, out move);
			return isValidMove && m_gameState.MakeMove(move);
		}

		public void Dispose()
		{
			m_gameState.PropertyChanged -= GameState_PropertyChanged;
		}

		private bool TryGetUserMove(string moveNotation, out Move move)
		{
			move = null;
			if (moveNotation.IsNullOrEmpty())
				return false;

			// castle kingside
			if (moveNotation.ToLowerInvariant() == "0-0")
			{
				move = Move.CastleKingside(m_gameState.PlayerTurn);
				return true;
			}

			// castle queenside
			if (moveNotation.ToLowerInvariant() == "0-0-0")
			{
				move = Move.CastleQueenside(m_gameState.PlayerTurn);
				return true;
			}

			if (moveNotation.Length != 5)
				return false;

			Coordinate from;
			if (!AlgebraicNotationUtility.TryConvertToCoordinate(moveNotation.Substring(0, 2), out from))
				return false;

			Coordinate to;
			if (!AlgebraicNotationUtility.TryConvertToCoordinate(moveNotation.Substring(3, 2), out to))
				return false;

			TryGetUserMove(from, to, out move);
			return move != null;
		}

		private bool TryGetUserMove(Coordinate from, Coordinate to, out Move move)
		{
			ReadOnlyCollection<Move> possibleMoves = m_gameState.GenerateMoves();
			move = possibleMoves.SingleOrDefault(x => x.From.File == from.File && x.From.Rank == from.Rank && x.To.File == to.File && x.To.Rank == to.Rank);

			return move != null;
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

		private void GameState_PropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			if (e.HasChanged(GameState.BoardProperty))
				BoardView = GetDisplayBoard(m_gameState.Board);
		}

		GameState m_gameState;
		ObservableCollection<Square> m_boardView;
		string m_moveEntry;
	}
}
