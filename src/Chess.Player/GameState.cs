using System.Collections.Generic;
using System.Collections.ObjectModel;
using Chess.Player.Board;
using Chess.Player.Utility;
using Utility;

namespace Chess.Player
{
	public sealed class GameState : NotifyPropertyChangedImpl
	{
		public static GameState NewGame()
		{
			GameState gameState = new GameState
			{
				PlayerTurn = Color.White,
				Board = BoardUtility.StartingBoard()
			};

			return gameState;
		}

		public static readonly string PlayerTurnProperty = PropertyNameUtility.GetPropertyName((GameState x) => x.PlayerTurn);
		public Color PlayerTurn
		{
			get
			{
				return m_playerTurn;
			}
			set
			{
				SetPropertyField(PlayerTurnProperty, value, ref m_playerTurn);
			}
		}

		public static readonly string BoardProperty = PropertyNameUtility.GetPropertyName((GameState x) => x.Board);
		public Square[,] Board
		{
			get
			{
				return m_board;
			}
			set
			{
				SetPropertyField(BoardProperty, value, ref m_board);
			}
		}

		public ReadOnlyCollection<Move> GenerateMoves()
		{
			List<Move> moves = new List<Move>();

			for (int i = 0; i < 8; i++)
			{
				for (int j = 0; j < 8; j++)
					moves.AddRange(m_board[i, j].GenerateMoves(m_playerTurn, i, j, m_board));
			}

			return moves.AsReadOnly();
		}

		Color m_playerTurn;
		Square[,] m_board;
	}
}
