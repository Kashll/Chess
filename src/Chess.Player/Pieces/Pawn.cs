using System.Collections.Generic;
using System.Collections.ObjectModel;
using Chess.Player.Board;

namespace Chess.Player.Pieces
{
	public sealed class Pawn : Piece
	{
		public Pawn(Color color)
			: base(color, PieceType.Pawn)
		{
		}

		public override ReadOnlyCollection<Move> GenerateMoves(int row, int column, Square[,] board)
		{
			List<Move> moves = new List<Move>();
			Coordinate from = new Coordinate(column, row);

			if (Color == Color.White)
			{
				// moves forward
				if (row != 7 && !board[row + 1, column].HasPiece)
				{
					if (row == 1 && !board[row + 2, column].HasPiece)
					{
						// starting move of two squares
						Coordinate to = new Coordinate(column, row + 2);
						moves.Add(new Move(MoveType.PawnStart, from, to));
					}

					if (row == 6)
					{
						// promotions
						Coordinate to = new Coordinate(column, row + 1);
						moves.AddRange(GetPromotions(from, to));
					}
					else
					{
						// single square forward	
						Coordinate to = new Coordinate(column, row + 1);
						moves.Add(new Move(MoveType.Standard, from, to));
					}
				}

				// capture left
				if (column != 0 && row != 7 && board[row + 1, column - 1].HasPiece && board[row + 1, column - 1].Piece.Color == Color.Black)
				{
					Coordinate to = new Coordinate(column - 1, row + 1);
					if (row == 6)
					{
						// promotion captures
						moves.AddRange(GetPromotions(from, to));
					}
					else
					{
						// regular captures
						moves.Add(new Move(MoveType.Standard, from, to));
					}
				}

				// capture right
				if (column != 7 && row != 7 && board[row + 1, column + 1].HasPiece && board[row + 1, column + 1].Piece.Color == Color.Black)
				{
					Coordinate to = new Coordinate(column + 1, row + 1);
					if (row == 6)
					{
						// promotion captures
						moves.AddRange(GetPromotions(from, to));
					}
					else
					{
						// regular captures
						moves.Add(new Move(MoveType.Standard, from, to));
					}
				}
			}
			else if (Color == Color.Black)
			{
				// moves forward
				if (row != 0 && !board[row - 1, column].HasPiece)
				{
					if (row == 6 && !board[row - 2, column].HasPiece)
					{
						// starting move of two squares
						Coordinate to = new Coordinate(column, row - 2);
						moves.Add(new Move(MoveType.PawnStart, from, to));
					}

					if (row == 1)
					{
						// promotions
						Coordinate to = new Coordinate(column, row - 1);
						moves.AddRange(GetPromotions(from, to));
					}
					else
					{
						// single square forward	
						Coordinate to = new Coordinate(column, row - 1);
						moves.Add(new Move(MoveType.Standard, from, to));
					}
				}

				// capture left
				if (column != 0 && row != 0 && board[row - 1, column - 1].HasPiece && board[row - 1, column - 1].Piece.Color == Color.White)
				{
					Coordinate to = new Coordinate(column - 1, row - 1);
					if (row == 1)
					{
						// promotion captures
						moves.AddRange(GetPromotions(from, to));
					}
					else
					{
						// regular captures
						moves.Add(new Move(MoveType.Standard, from, to));
					}
				}

				// capture right
				if (column != 7 && row != 0 && board[row - 1, column + 1].HasPiece && board[row - 1, column + 1].Piece.Color == Color.White)
				{
					Coordinate to = new Coordinate(column + 1, row - 1);
					if (row == 6)
					{
						// promotion captures
						moves.AddRange(GetPromotions(from, to));
					}
					else
					{
						// regular captures
						moves.Add(new Move(MoveType.Standard, from, to));
					}
				}
			}

			return moves.AsReadOnly();
		}

		private IEnumerable<Move> GetPromotions(Coordinate from, Coordinate to)
		{
			return new List<Move>
			{
				new Move(new Queen(Color), from, to),
				new Move(new Rook(Color), from, to),
				new Move(new Bishop(Color), from, to),
				new Move(new Knight(Color), from, to)
			};
		}
	}
}
