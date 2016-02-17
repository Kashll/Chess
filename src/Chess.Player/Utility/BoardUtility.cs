using Chess.Player.Board;
using Chess.Player.Pieces;

namespace Chess.Player.Utility
{
	public static class BoardUtility
	{
		public static Square[,] StartingBoard()
		{
			Square[,] board = new Square[8, 8];

			// rank 1
			int row = 0;
			board[row, 0] = new Square(new Coordinate(0, row), Color.Black, new Rook(Color.White));
			board[row, 1] = new Square(new Coordinate(1, row), Color.White, new Knight(Color.White));
			board[row, 2] = new Square(new Coordinate(2, row), Color.Black, new Bishop(Color.White));
			board[row, 3] = new Square(new Coordinate(3, row), Color.White, new Queen(Color.White));
			board[row, 4] = new Square(new Coordinate(4, row), Color.Black, new King(Color.White));
			board[row, 5] = new Square(new Coordinate(5, row), Color.White, new Bishop(Color.White));
			board[row, 6] = new Square(new Coordinate(6, row), Color.Black, new Knight(Color.White));
			board[row, 7] = new Square(new Coordinate(7, row), Color.White, new Rook(Color.White));

			// rank 2
			row = 1;
			for (int i = 0; i < 8; i++)
				board[row, i] = new Square(new Coordinate(i, row), i % 2 == 0 ? Color.White : Color.Black, new Pawn(Color.White));

			// ranks 3 - 6
			for (row = 2; row < 6; row++)
			{
				for (int j = 0; j < 8; j++)
					board[row, j] = new Square(new Coordinate(j, row), (row + j) % 2 == 0 ? Color.Black : Color.White);
			}

			// rank 7
			row = 6;
			for (int i = 0; i < 8; i++)
				board[row, i] = new Square(new Coordinate(i, row), i % 2 == 0 ? Color.Black : Color.White, new Pawn(Color.Black));

			// rank 8
			row = 7;
			board[row, 0] = new Square(new Coordinate(0, row), Color.White, new Rook(Color.Black));
			board[row, 1] = new Square(new Coordinate(1, row), Color.Black, new Knight(Color.Black));
			board[row, 2] = new Square(new Coordinate(2, row), Color.White, new Bishop(Color.Black));
			board[row, 3] = new Square(new Coordinate(3, row), Color.Black, new Queen(Color.Black));
			board[row, 4] = new Square(new Coordinate(4, row), Color.White, new King(Color.Black));
			board[row, 5] = new Square(new Coordinate(5, row), Color.Black, new Bishop(Color.Black));
			board[row, 6] = new Square(new Coordinate(6, row), Color.White, new Knight(Color.Black));
			board[row, 7] = new Square(new Coordinate(7, row), Color.Black, new Rook(Color.Black));

			return board;
		}

		public static Square[,] EmptyBoard()
		{
			Square[,] board = new Square[8, 8];

			for (int i = 0; i < 8; i++)
			{
				for (int j = 0; j < 8; j++)
					board[i, j] = new Square(new Coordinate(j, i), (i + j) % 2 == 0 ? Color.Black : Color.White);
			}

			return board;
		}

		public static int ToRank(this int row)
		{
			return row + 1;
		}

		public static File ToFile(this int column)
		{
			return (File) column;
		}

		public static int ToRow(this int rank)
		{
			return rank - 1;
		}

		public static int ToColumn(this File file)
		{
			return (int) file;
		}
	}
}
