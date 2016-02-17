using System.Collections.Generic;
using System.Collections.ObjectModel;
using Chess.Player.Pieces;
using Chess.Player.Utility;

namespace Chess.Player.Board
{
	public sealed class Square
	{
		public Square(Coordinate coordinate, Color color)
		{
			m_coordinate = coordinate;
			m_color = color;
		}

		public Square(Coordinate coordinate, Color color, Piece piece)
		{
			m_coordinate = coordinate;
			m_color = color;
			m_piece = piece;
		}

		public Coordinate Coordinate
		{
			get { return m_coordinate; }
		}

		public Color Color
		{
			get { return m_color; }
		}

		public bool HasPiece
		{
			get { return m_piece != null; }
		}

		public Piece Piece
		{
			get { return m_piece; }
			set { m_piece = value; }
		}

		public ReadOnlyCollection<Move> GenerateMoves(Color onMove, Square[,] board)
		{
			List<Move> moves = new List<Move>();

			if (HasPiece && onMove == Piece.Color)
				moves.AddRange(Piece.GenerateMoves(m_coordinate.Rank.ToRow(), m_coordinate.File.ToColumn(), board));

			return moves.AsReadOnly();
		}

		readonly Color m_color;
		readonly Coordinate m_coordinate;

		Piece m_piece;
	}
}
