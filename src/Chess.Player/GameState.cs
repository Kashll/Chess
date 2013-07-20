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

			// standard moves
			for (int i = 0; i < 8; i++)
			{
				for (int j = 0; j < 8; j++)
					moves.AddRange(m_board[i, j].GenerateMoves(m_playerTurn, i, j, m_board));
			}

			// castling
			if (m_playerTurn == Color.White && !m_whiteKingMoved)
			{
				if (!m_whiteKingRookMoved && !m_board[0,5].HasPiece && !m_board[0,6].HasPiece)
					moves.Add(new Move(MoveType.CastleKingside));

				if (!m_whiteQueenRookMoved && !m_board[0,3].HasPiece && !m_board[0,2].HasPiece && !m_board[0,1].HasPiece)
					moves.Add(new Move(MoveType.CastleQueenside));
			}

			if (m_playerTurn == Color.Black && !m_blackKingMoved)
			{
				if (!m_blackKingRookMoved && !m_board[7, 5].HasPiece && !m_board[7, 6].HasPiece)
					moves.Add(new Move(MoveType.CastleKingside));

				if (!m_blackQueenRookMoved && !m_board[7, 3].HasPiece && !m_board[7, 2].HasPiece && !m_board[7, 1].HasPiece)
					moves.Add(new Move(MoveType.CastleQueenside));
			}

			// en passant
			if (m_enPassantColumn != null)
			{
				if (m_playerTurn == Color.White)
				{
					const int row = 4;

					// left
					int leftColumn = m_enPassantColumn.Value - 1;
					if (leftColumn > 0 && m_board[row, leftColumn].HasPiece && m_board[row, leftColumn].Piece.Color == Color.White && m_board[row, leftColumn].Piece.Type == PieceType.Pawn)
						moves.Add(new Move(MoveType.EnPassant, new Coordinate(leftColumn, row), new Coordinate(m_enPassantColumn.Value, row + 1)));

					// right
					int rightColumn = m_enPassantColumn.Value + 1;
					if (rightColumn > 0 && m_board[row, rightColumn].HasPiece && m_board[row, rightColumn].Piece.Color == Color.White && m_board[row, rightColumn].Piece.Type == PieceType.Pawn)
						moves.Add(new Move(MoveType.EnPassant, new Coordinate(rightColumn, row), new Coordinate(m_enPassantColumn.Value, row + 1)));
				}
				else if (m_playerTurn == Color.Black)
				{
					const int row = 3;

					// left
					int leftColumn = m_enPassantColumn.Value - 1;
					if (leftColumn > 0 && m_board[row, leftColumn].HasPiece && m_board[row, leftColumn].Piece.Color == Color.Black && m_board[row, leftColumn].Piece.Type == PieceType.Pawn)
						moves.Add(new Move(MoveType.EnPassant, new Coordinate(leftColumn, row), new Coordinate(m_enPassantColumn.Value, row - 1)));

					// right
					int rightColumn = m_enPassantColumn.Value + 1;
					if (rightColumn > 0 && m_board[row, rightColumn].HasPiece && m_board[row, rightColumn].Piece.Color == Color.Black && m_board[row, rightColumn].Piece.Type == PieceType.Pawn)
						moves.Add(new Move(MoveType.EnPassant, new Coordinate(rightColumn, row), new Coordinate(m_enPassantColumn.Value, row - 1)));
				}
			}

			return moves.AsReadOnly();
		}

		Color m_playerTurn;
		Square[,] m_board;
		bool m_whiteKingMoved;
		bool m_blackKingMoved;
		bool m_whiteKingRookMoved;
		bool m_blackKingRookMoved;
		bool m_whiteQueenRookMoved;
		bool m_blackQueenRookMoved;
		int? m_enPassantColumn;
	}
}
