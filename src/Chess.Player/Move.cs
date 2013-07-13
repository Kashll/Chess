using System;
using Chess.Player.Board;

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

		readonly MoveType m_moveType;
		readonly Coordinate m_from;
		readonly Coordinate m_to;
		readonly PieceType m_promoteTo;
	}
}
