using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;
using Color = Chess.Player.Color;

namespace Chess.UI.Converters
{
	[ValueConversion(typeof(Color), typeof(Brush))]
	public class ColorConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			Color color = (Color) value;

			return color == Color.Black ? Brushes.SaddleBrown : Brushes.SandyBrown;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			Brush brush = (Brush) value;

			return brush.Equals(Brushes.SaddleBrown) ? Color.Black : Color.White;
		}
	}
}
