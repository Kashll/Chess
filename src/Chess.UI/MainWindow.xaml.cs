using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Chess.Player.Board;
using Chess.UI.ViewModel;

namespace Chess.UI
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
		}

		private void MoveEntry_OnKeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Enter)
			{
				TextBox textBox = sender as TextBox;
				if (textBox != null)
				{
					MainWindowViewModel viewModel = textBox.DataContext as MainWindowViewModel;
					if (viewModel != null)
					{
						if (viewModel.AttemptUserMoveFromEntryHelper())
							viewModel.MoveEntry = String.Empty;
					}
				}
			}
		}

		private void Board_OnDragEnter(object sender, DragEventArgs e)
		{
			e.Effects = DragDropEffects.Move;
			e.Handled = true;
		}

		private void Board_OnDrop(object sender, DragEventArgs e)
		{
			MainWindowViewModel viewModel = DataContext as MainWindowViewModel;
			Square destination = ((FrameworkElement) sender).DataContext as Square;
			object data = e.Data.GetData(typeof(Square));
			if (viewModel != null && destination != null && data != null)
			{
				Coordinate from = ((Square) data).Coordinate;
				Coordinate to = destination.Coordinate;		
				viewModel.AttemptUserMove(from, to);
			}
		}

		private void Square_OnMouseDown(object sender, MouseButtonEventArgs e)
		{
			if (Mouse.LeftButton == MouseButtonState.Pressed)
			{
				Grid senderGrid = (Grid) sender;
				Square source = senderGrid.DataContext as Square;
				if (source != null && source.HasPiece)
					DragDrop.DoDragDrop(senderGrid, source, DragDropEffects.Move);
			}
		}
	}
}
