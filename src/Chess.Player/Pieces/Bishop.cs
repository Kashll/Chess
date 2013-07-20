using System.Collections.ObjectModel;
using Chess.Player.Board;

namespace Chess.Player.Pieces
{
	public sealed class Bishop : Piece
	{
		public Bishop(Color color)
			: base(color, PieceType.Bishop)
		{
		}

		public override ReadOnlyCollection<Move> GenerateMoves(int row, int column, Square[,] board)
		{
			throw new System.NotImplementedException();
		}
	}
}
