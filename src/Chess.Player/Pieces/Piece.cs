using System.Collections.ObjectModel;
using Chess.Player.Board;

namespace Chess.Player.Pieces
{
	public abstract class Piece
	{
		protected Piece(Color color, PieceType type)
		{
			m_color = color;
			m_type = type;
		}

		public Color Color
		{
			get { return m_color; }
		}

		public PieceType Type
		{
			get { return m_type; }
		}

		public abstract ReadOnlyCollection<Move> GenerateMoves(int row, int column, Square[,] board);

		readonly Color m_color;
		readonly PieceType m_type;
	}
}
