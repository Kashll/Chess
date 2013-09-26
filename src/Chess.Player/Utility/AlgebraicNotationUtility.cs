using System;
using System.Globalization;
using Chess.Player.Board;

namespace Chess.Player.Utility
{
	public static class AlgebraicNotationUtility
	{
		public static bool TryConvertToCoordinate(string coordinateNotation, out Coordinate coordinate)
		{
			coordinate = null;

			if (coordinateNotation.Length != 2)
				return false;

			File file;
			if (!TryConvertToFile(coordinateNotation[0], out file))
				return false;

			int rank;
			if (!TryConvertToRank(coordinateNotation[1], out rank))
				return false;

			coordinate = new Coordinate(file, rank);
			return true;
		}

		public static bool TryConvertToFile(char fileCharacter, out File file)
		{			
			switch (char.ToLowerInvariant(fileCharacter))
			{
			case 'a':
				file = File.A;
				return true;
			case 'b':
				file = File.B;
				return true;
			case 'c':
				file = File.C;
				return true;
			case 'd':
				file = File.D;
				return true;
			case 'e':
				file = File.E;
				return true;
			case 'f':
				file = File.F;
				return true;
			case 'g':
				file = File.G;
				return true;
			case 'h':
				file = File.H;
				return true;
			default:
				file = File.A;
				return false;
			}
		}

		public static bool TryConvertToRank(char rankCharacter, out int rank)
		{
			if (!Int32.TryParse(rankCharacter.ToString(CultureInfo.InvariantCulture), out rank))
				return false;

			return rank >= 1 && rank <= 8;
		}
	}
}
