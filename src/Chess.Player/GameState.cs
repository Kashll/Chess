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

		Color m_playerTurn;
		Square[,] m_board;
	}
}
