using System;

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
			return m_rank - 1;
		}

		public int BoardColumn()
		{
			return (int) m_file;
		}

		readonly File m_file;
		readonly int m_rank;
	}
}
