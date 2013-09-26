using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Chess.Player.Board;
using Chess.Player.Pieces;
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
				GameResult = GameResult.InProgress,
				Board = BoardUtility.StartingBoard()
			};

			return gameState;
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

		public static readonly string GameResultProperty = PropertyNameUtility.GetPropertyName((GameState x) => x.GameResult);
		public GameResult GameResult
		{
			get
			{
				return m_gameResult;
			}
			set
			{
				SetPropertyField(GameResultProperty, value, ref m_gameResult);
			}
		}

		public bool MakeMove(Move move)
		{
			bool isLegalMove = GenerateMoves().Any(x => 
				x.MoveType == move.MoveType && 
				x.PromoteTo == move.PromoteTo &&
				((x.From == null && move.From == null) || (x.From != null && move.From != null && x.From.File == move.From.File && x.From.Rank == move.From.Rank)) &&
				((x.To == null && move.To == null) || (x.To != null && move.To != null && x.To.File == move.To.File && x.To.Rank == move.To.Rank)));

			if (!isLegalMove)
				return false;

			switch (move.MoveType)
			{
			case MoveType.Standard:
				Piece pieceToMove = Board[move.From.BoardRow(), move.From.BoardColumn()].Piece;
				Board[move.To.BoardRow(), move.To.BoardColumn()].Piece = Board[move.From.BoardRow(), move.From.BoardColumn()].Piece;
				Board[move.From.BoardRow(), move.From.BoardColumn()].Piece = null;

				if (pieceToMove.Type == PieceType.King)
				{
					m_whiteKingMoved |= pieceToMove.Color == Color.White;
					m_blackKingMoved |= pieceToMove.Color == Color.Black;
				}
				else if (pieceToMove.Type == PieceType.Rook)
				{
					m_whiteQueenRookMoved |= pieceToMove.Color == Color.White && move.From.BoardRow() == 0 && move.From.BoardColumn() == 0;
					m_whiteKingRookMoved |= pieceToMove.Color == Color.White && move.From.BoardRow() == 0 && move.From.BoardColumn() == 7;
					m_blackQueenRookMoved |= pieceToMove.Color == Color.Black && move.From.BoardRow() == 7 && move.From.BoardColumn() == 0;
					m_blackKingRookMoved |= pieceToMove.Color == Color.Black && move.From.BoardRow() == 7 && move.From.BoardColumn() == 7;
				}

				break;
			case MoveType.PawnStart:
				m_enPassantColumn = move.From.BoardColumn();
				Board[move.To.BoardRow(), move.To.BoardColumn()].Piece = Board[move.From.BoardRow(), move.From.BoardColumn()].Piece;
				Board[move.From.BoardRow(), move.From.BoardColumn()].Piece = null;
				break;
			case MoveType.EnPassant:
				int rowOffset = m_playerTurn == Color.White ? -1 : 1;
				Board[move.To.BoardRow(), move.To.BoardColumn()].Piece = Board[move.From.BoardRow(), move.From.BoardColumn()].Piece;
				Board[move.From.BoardRow(), move.From.BoardColumn()].Piece = null;
				Board[move.To.BoardRow() + rowOffset, move.To.BoardColumn()].Piece = null;
				break;
			case MoveType.Promotion:
				Board[move.To.BoardRow(), move.To.BoardColumn()].Piece = move.PromoteTo;
				Board[move.From.BoardRow(), move.From.BoardColumn()].Piece = null;
				break;
			case MoveType.CastleKingside:
				int kRow = m_playerTurn == Color.White ? 0 : 7;
				Board[kRow, 5].Piece = Board[kRow, 7].Piece;
				Board[kRow, 6].Piece = Board[kRow, 4].Piece;
				Board[kRow, 4].Piece = null;
				Board[kRow, 7].Piece = null;

				if (m_playerTurn == Color.White)
				{
					m_whiteKingMoved = true;
					m_whiteKingRookMoved = true;
				}
				else
				{
					m_blackKingMoved = true;
					m_blackKingRookMoved = true;
				}
				break;
			case MoveType.CastleQueenside:
				int qRow = m_playerTurn == Color.White ? 0 : 7;
				Board[qRow, 2].Piece = Board[qRow, 4].Piece;
				Board[qRow, 3].Piece = Board[qRow, 0].Piece;
				Board[qRow, 0].Piece = null;
				Board[qRow, 4].Piece = null;

				if (m_playerTurn == Color.White)
				{
					m_whiteKingMoved = true;
					m_whiteQueenRookMoved = true;
				}
				else
				{
					m_blackKingMoved = true;
					m_blackQueenRookMoved = true;
				}

				break;
			default:
				throw new ArgumentOutOfRangeException("move");
			}

			// reset enPassant state
			if (move.MoveType != MoveType.PawnStart)
				m_enPassantColumn = null;

			// check to see if the game is over
			bool hasWhiteKing = false;
			bool hasBlackKing = false;
			for (int i = 0; i < Board.GetLength(0); i++)
			{
				for (int j = 0; j < Board.GetLength(0); j++)
				{
					if (Board[i, j].Piece != null && Board[i, j].Piece.Type == PieceType.King)
					{
						hasWhiteKing |= Board[i, j].Piece.Color == Color.White;
						hasBlackKing |= Board[i, j].Piece.Color == Color.Black;
					}
				}
			}
			if (!hasWhiteKing)
				GameResult = GameResult.BlackWin;
			else if (!hasBlackKing)
				GameResult = GameResult.WhiteWin;

			PlayerTurn = m_playerTurn == Color.White ? Color.Black : Color.White;
			RaisePropertyChanged(BoardProperty);
			return true;
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
			bool checkforCastling = (m_playerTurn == Color.White && !m_whiteKingMoved && (!m_whiteKingRookMoved || !m_whiteQueenRookMoved)) ||
												m_playerTurn == Color.Black && !m_blackKingMoved && (!m_blackKingRookMoved || !m_blackQueenRookMoved);

			if (checkforCastling)
			{
				List<Move> opponentBasicMoves = new List<Move>();
				for (int i = 0; i < 8; i++)
				{
					for (int j = 0; j < 8; j++)
						opponentBasicMoves.AddRange(m_board[i, j].GenerateMoves(m_playerTurn == Color.White ? Color.Black : Color.White, i, j, m_board));
				}

				int homeRow = m_playerTurn == Color.White ? 0 : 7;
				bool isCastleQueensideThroughCheck = opponentBasicMoves.Any(x => x.To.BoardRow() == homeRow && x.To.BoardColumn() >= 1 && x.To.BoardColumn() <= 4);
				bool isCastleKingsideThroughCheck = opponentBasicMoves.Any(x => x.To.BoardRow() == homeRow && x.To.BoardColumn() >= 4 && x.To.BoardColumn() <= 6);

				if (m_playerTurn == Color.White && !m_whiteKingMoved)
				{
					if (!isCastleKingsideThroughCheck && !m_whiteKingRookMoved && !m_board[0, 5].HasPiece && !m_board[0, 6].HasPiece)
						moves.Add(new Move(MoveType.CastleKingside));

					if (!isCastleQueensideThroughCheck && !m_whiteQueenRookMoved && !m_board[0, 3].HasPiece && !m_board[0, 2].HasPiece && !m_board[0, 1].HasPiece)
						moves.Add(new Move(MoveType.CastleQueenside));
				}

				if (m_playerTurn == Color.Black && !m_blackKingMoved)
				{
					if (!isCastleKingsideThroughCheck && !m_blackKingRookMoved && !m_board[7, 5].HasPiece && !m_board[7, 6].HasPiece)
						moves.Add(new Move(MoveType.CastleKingside));

					if (!isCastleQueensideThroughCheck && !m_blackQueenRookMoved && !m_board[7, 3].HasPiece && !m_board[7, 2].HasPiece && !m_board[7, 1].HasPiece)
						moves.Add(new Move(MoveType.CastleQueenside));
				}
			}

			// en passant
			if (m_enPassantColumn != null)
			{
				if (m_playerTurn == Color.White)
				{
					const int row = 4;

					// left
					int leftColumn = m_enPassantColumn.Value - 1;
					if (leftColumn >= 0 && m_board[row, leftColumn].HasPiece && m_board[row, leftColumn].Piece.Color == Color.White && m_board[row, leftColumn].Piece.Type == PieceType.Pawn)
						moves.Add(new Move(MoveType.EnPassant, new Coordinate(leftColumn, row), new Coordinate(m_enPassantColumn.Value, row + 1)));

					// right
					int rightColumn = m_enPassantColumn.Value + 1;
					if (rightColumn <= 7 && m_board[row, rightColumn].HasPiece && m_board[row, rightColumn].Piece.Color == Color.White && m_board[row, rightColumn].Piece.Type == PieceType.Pawn)
						moves.Add(new Move(MoveType.EnPassant, new Coordinate(rightColumn, row), new Coordinate(m_enPassantColumn.Value, row + 1)));
				}
				else if (m_playerTurn == Color.Black)
				{
					const int row = 3;

					// left
					int leftColumn = m_enPassantColumn.Value - 1;
					if (leftColumn >= 0 && m_board[row, leftColumn].HasPiece && m_board[row, leftColumn].Piece.Color == Color.Black && m_board[row, leftColumn].Piece.Type == PieceType.Pawn)
						moves.Add(new Move(MoveType.EnPassant, new Coordinate(leftColumn, row), new Coordinate(m_enPassantColumn.Value, row - 1)));

					// right
					int rightColumn = m_enPassantColumn.Value + 1;
					if (rightColumn <= 7 && m_board[row, rightColumn].HasPiece && m_board[row, rightColumn].Piece.Color == Color.Black && m_board[row, rightColumn].Piece.Type == PieceType.Pawn)
						moves.Add(new Move(MoveType.EnPassant, new Coordinate(rightColumn, row), new Coordinate(m_enPassantColumn.Value, row - 1)));
				}
			}

			return moves.AsReadOnly();
		}

		Square[,] m_board;
		Color m_playerTurn;
		GameResult m_gameResult;
		bool m_whiteKingMoved;
		bool m_blackKingMoved;
		bool m_whiteKingRookMoved;
		bool m_blackKingRookMoved;
		bool m_whiteQueenRookMoved;
		bool m_blackQueenRookMoved;
		int? m_enPassantColumn;
	}
}
