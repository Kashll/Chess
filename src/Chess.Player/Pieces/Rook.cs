using System.Collections.ObjectModel;
using Chess.Player.Board;

namespace Chess.Player.Pieces
{
	public sealed class Rook : Piece
	{
		public Rook(Color color)
			: base(color, PieceType.Rook)
		{
		}

		public override ReadOnlyCollection<Move> GenerateMoves(int row, int column, Square[,] board)
		{
			throw new System.NotImplementedException();
		}
	}
}
