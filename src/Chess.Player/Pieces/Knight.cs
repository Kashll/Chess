using System.Collections.Generic;
using System.Collections.ObjectModel;
using Chess.Player.Board;

namespace Chess.Player.Pieces
{
	public sealed class Knight : Piece
	{
		public Knight(Color color)
			: base(color)
        {
		}

        public override PieceType Type { get { return PieceType.Knight; } }

        public override ReadOnlyCollection<Move> GenerateMoves(int row, int column, Square[,] board)
		{
			List<Move> moves = new List<Move>();
			moves.AddRange(Scan(1, 2, row, column, board, onlyOnce: true));
			moves.AddRange(Scan(2, 1, row, column, board, onlyOnce: true));
			moves.AddRange(Scan(2, -1, row, column, board, onlyOnce: true));
			moves.AddRange(Scan(1, -2, row, column, board, onlyOnce: true));
			moves.AddRange(Scan(-1, -2, row, column, board, onlyOnce: true));
			moves.AddRange(Scan(-2, -1, row, column, board, onlyOnce: true));
			moves.AddRange(Scan(-2, 1, row, column, board, onlyOnce: true));
			moves.AddRange(Scan(-1, 2, row, column, board, onlyOnce: true));

			return moves.AsReadOnly();
		}

	    public override string ToString()
	    {
	        return "N";
	    }
	}
}
