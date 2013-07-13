using System;
using System.Globalization;
using System.Windows.Data;
using Chess.Player.Board;
using Chess.Player.Pieces;
using Utility;
using Color = Chess.Player.Color;

namespace Chess.UI.Converters
{
	[ValueConversion(typeof(Square), typeof(Uri))]
	public class PieceImageConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			Square square = (Square) value;

			if (!square.HasPiece)
				return null;

			const string packUriBase = @"pack://application:,,,/Chess.UI;component/Images/{0}{1}.png";
			return new Uri(packUriBase.FormatInvariant(square.Piece.Color, square.Piece.Type));
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			Uri uri = (Uri) value;

			return new Pawn(Color.White);
		}
	}
}
