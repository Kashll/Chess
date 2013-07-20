using System;
using Chess.Player.Board;
using Chess.Player.Pieces;
using Utility;

namespace Chess.Player
{
	public sealed class Move
	{
		public Move(MoveType type, Coordinate from, Coordinate to)
		{
			if (MoveType != MoveType.Standard && MoveType != MoveType.EnPassant)
				throw new ArgumentException("type");

			m_moveType = type;
			m_from = from;
			m_to = to;
		}

		public Move(PieceType promoteTo, Coordinate from, Coordinate to)
		{
			m_moveType = MoveType.Promotion;
			m_promoteTo = promoteTo;
			m_from = from;
			m_to = to;
		}

		public Move(MoveType type)
		{
			if (MoveType != MoveType.CastleKingside && MoveType != MoveType.CastleQueenside)
				throw new ArgumentException("type");

			m_moveType = type;
		}

		public MoveType MoveType
		{
			get { return m_moveType; }
		}

		public Coordinate From
		{
			get { return m_from; }
		}

		public Coordinate To
		{
			get { return m_to; }
		}

		public PieceType PromoteTo
		{
			get { return m_promoteTo; }
		}

		public string ToString(Square[,] board)
		{
			if (m_moveType == MoveType.CastleKingside)
				return "0-0";

			if (m_moveType == MoveType.CastleQueenside)
				return "0-0-0";

			Piece piece = board[m_from.BoardRow(), m_from.BoardColumn()].Piece;
			bool isCapture = board[m_to.BoardRow(), m_to.BoardColumn()].HasPiece;

			string promotionType = m_moveType == MoveType.Promotion ? m_promoteTo.ToString() : "";
			string pieceContext = "";
			if (piece.Type == PieceType.Knight || piece.Type == PieceType.Rook)
			{
				// may need to append file or rank info
				int count = 0;
				for (int i = 0; i < 8; i++)
				{
					if (board[m_from.BoardRow(), i].HasPiece && board[m_from.BoardRow(), i].Piece.Type == piece.Type && board[m_from.BoardRow(), i].Piece.Color == piece.Color)
						count++;
				}

				pieceContext = count <= 2 ? m_from.File.ToString() : m_from.Rank.ToString();
			}

			return "{0}{1}{2}{3}{4}{5}".FormatInvariant(piece, pieceContext, isCapture ? "x" : "", m_to.File.ToString().ToLowerInvariant(), m_to.Rank, promotionType);
		}

		readonly MoveType m_moveType;
		readonly Coordinate m_from;
		readonly Coordinate m_to;
		readonly PieceType m_promoteTo;
	}
}
