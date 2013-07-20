using System.Collections.Generic;
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
			List<Move> moves = new List<Move>();
			moves.AddRange(Scan(-1, 1, row, column, board));
			moves.AddRange(Scan(1, 1, row, column, board));
			moves.AddRange(Scan(1,- 1, row, column, board));
			moves.AddRange(Scan(-1, -1, row, column, board));

			return moves.AsReadOnly();
		}
	}
}
