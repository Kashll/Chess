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
			board[0, 0] = new Square(Color.Black, new Rook(Color.White));
			board[0, 1] = new Square(Color.White, new Knight(Color.White));
			board[0, 2] = new Square(Color.Black, new Bishop(Color.White));
			board[0, 3] = new Square(Color.White, new Queen(Color.White));
			board[0, 4] = new Square(Color.Black, new King(Color.White));
			board[0, 5] = new Square(Color.White, new Bishop(Color.White));
			board[0, 6] = new Square(Color.Black, new Knight(Color.White));
			board[0, 7] = new Square(Color.White, new Rook(Color.White));

			// rank 2
			for (int i = 0; i < 8; i++)
				board[1, i] = new Square(i % 2 == 0 ? Color.White : Color.Black, new Pawn(Color.White));

			// ranks 3 - 6
			for (int i = 2; i < 6; i++)
			{
				for (int j = 0; j < 8; j++)
					board[i, j] = new Square((i + j) % 2 == 0 ? Color.Black : Color.White);
			}

			// rank 7
			for (int i = 0; i < 8; i++)
				board[6, i] = new Square(i % 2 == 0 ? Color.Black : Color.White, new Pawn(Color.Black));

			// rank 8
			board[7, 0] = new Square(Color.White, new Rook(Color.Black));
			board[7, 1] = new Square(Color.Black, new Knight(Color.Black));
			board[7, 2] = new Square(Color.White, new Bishop(Color.Black));
			board[7, 3] = new Square(Color.Black, new Queen(Color.Black));
			board[7, 4] = new Square(Color.White, new King(Color.Black));
			board[7, 5] = new Square(Color.Black, new Bishop(Color.Black));
			board[7, 6] = new Square(Color.White, new Knight(Color.Black));
			board[7, 7] = new Square(Color.Black, new Rook(Color.Black));

			return board;
		}

		public static Square[,] EmptyBoard()
		{
			Square[,] board = new Square[8, 8];

			for (int i = 0; i < 8; i++)
			{
				for (int j = 0; j < 8; j++)
					board[i, j] = new Square((i + j) % 2 == 0 ? Color.Black : Color.White);
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
