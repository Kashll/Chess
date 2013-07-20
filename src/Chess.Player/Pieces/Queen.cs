using System.Collections.ObjectModel;
using Chess.Player.Board;

namespace Chess.Player.Pieces
{
	public sealed class Queen : Piece
	{
		public Queen(Color color)
			: base(color, PieceType.Queen)
		{
		}

		public override ReadOnlyCollection<Move> GenerateMoves(int row, int column, Square[,] board)
		{
			throw new System.NotImplementedException();
		}
	}
}
