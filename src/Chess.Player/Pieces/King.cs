﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using Chess.Player.Board;

namespace Chess.Player.Pieces
{
	public sealed class King : Piece
	{
		public King(Color color)
			: base(color)
        {
		}

        public override PieceType Type { get { return PieceType.King; } }

        public override ReadOnlyCollection<Move> GenerateMoves(int row, int column, Square[,] board)
		{
			List<Move> moves = new List<Move>();

			// horizontal/vertical
			moves.AddRange(Scan(0, 1, row, column, board, onlyOnce: true));
			moves.AddRange(Scan(1, 0, row, column, board, onlyOnce: true));
			moves.AddRange(Scan(0, -1, row, column, board, onlyOnce: true));
			moves.AddRange(Scan(-1, 0, row, column, board, onlyOnce: true));

			// diagonal
			moves.AddRange(Scan(-1, 1, row, column, board, onlyOnce: true));
			moves.AddRange(Scan(1, 1, row, column, board, onlyOnce: true));
			moves.AddRange(Scan(1, -1, row, column, board, onlyOnce: true));
			moves.AddRange(Scan(-1, -1, row, column, board, onlyOnce: true));

			return moves.AsReadOnly();
		}

	    public override string ToString()
	    {
	        return "K";
	    }
	}
}
