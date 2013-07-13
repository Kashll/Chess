using Chess.Player.Pieces;

namespace Chess.Player.Board
{
	public sealed class Square
	{
		public Square(Color color)
		{
			m_color = color;
		}

		public Square(Color color, Piece piece)
		{
			m_color = color;
			m_piece = piece;
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

		readonly Color m_color;

		Piece m_piece;
	}
}
