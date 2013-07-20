using System;
using Chess.Player.Utility;

namespace Chess.Player.Board
{
	public sealed class Coordinate
	{
		public Coordinate(File file, int rank)
		{
			if (rank < 1 || rank > 8)
				throw new ArgumentException("rank");

			m_file = file;
			m_rank = rank;
		}

		public Coordinate(int column, int row)
		{
			if (row < 0 || row > 7 || column < 0 || column > 7)
				throw new ArgumentException("coordinate out of board range");

			m_file = column.ToFile();
			m_rank = row.ToRank();
		}

		public File File
		{
			get { return m_file; }
		}

		public int Rank
		{
			get { return m_rank; }
		}

		public int BoardRow()
		{
			return m_rank.ToRow();
		}

		public int BoardColumn()
		{
			return m_file.ToColumn();
		}

		readonly File m_file;
		readonly int m_rank;
	}
}
