using System.Collections.ObjectModel;
using Chess.Player.Board;

namespace Chess.Player.Pieces
{
	public sealed class Knight : Piece
	{
		public Knight(Color color)
			: base(color, PieceType.Knight)
		{
		}

		public override ReadOnlyCollection<Move> GenerateMoves(int row, int column, Square[,] board)
		{
			throw new System.NotImplementedException();
		}
	}
}
