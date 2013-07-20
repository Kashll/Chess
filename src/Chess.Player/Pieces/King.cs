using System.Collections.ObjectModel;
using Chess.Player.Board;

namespace Chess.Player.Pieces
{
	public sealed class King : Piece
	{
		public King(Color color)
			: base(color, PieceType.King)
		{
		}

		public override ReadOnlyCollection<Move> GenerateMoves(int row, int column, Square[,] board)
		{
			throw new System.NotImplementedException();
		}
	}
}
