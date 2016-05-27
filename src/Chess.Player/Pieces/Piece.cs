using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Chess.Player.Board;

namespace Chess.Player.Pieces
{
	public abstract class Piece
	{
		protected Piece(Color color)
        {
			m_color = color;
		}

		public Color Color
		{
			get { return m_color; }
		}

		public abstract PieceType Type { get; }

		public abstract ReadOnlyCollection<Move> GenerateMoves(int row, int column, Square[,] board);

	    public abstract override string ToString();

		protected ReadOnlyCollection<Move> Scan(int xDirection, int yDirection, int row, int column, Square[,] board)
		{
			List<Move> moves = new List<Move>();
			Coordinate from = new Coordinate(column, row);

			int newRow = row;
			int newColumn = column;
			bool finished = false;

			while (!finished)
			{
				newRow += yDirection;
				newColumn += xDirection;

				if (newRow >= 0 && newRow <= 7 && newColumn >= 0 && newColumn <= 7)
				{
					Coordinate to = new Coordinate(newColumn, newRow);
					Move move = new Move(MoveType.Standard, from, to);

					if (!board[newRow, newColumn].HasPiece)
					{
						// empty square
						moves.Add(move);
					}
					else
					{
						// occupied square
						if (board[newRow, newColumn].Piece.Color != m_color)
							moves.Add(move);

						finished = true;
					}
				}
				else
				{
					finished = true;
				}

				// kings and knights can only move once in a given direction
				finished |= Type == PieceType.King || Type == PieceType.Knight;
			}

			return moves.AsReadOnly();
		}

		readonly Color m_color;
	}
}
