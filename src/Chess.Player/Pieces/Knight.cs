using System.Collections.Generic;
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
			List<Move> moves = new List<Move>();
			moves.AddRange(Scan(1, 2, row, column, board));
			moves.AddRange(Scan(2, 1, row, column, board));
			moves.AddRange(Scan(2, -1, row, column, board));
			moves.AddRange(Scan(1, -2, row, column, board));
			moves.AddRange(Scan(-1, -2, row, column, board));
			moves.AddRange(Scan(-2, -1, row, column, board));
			moves.AddRange(Scan(-2, 1, row, column, board));
			moves.AddRange(Scan(-1, 2, row, column, board));

			return moves.AsReadOnly();
		}
	}
}
