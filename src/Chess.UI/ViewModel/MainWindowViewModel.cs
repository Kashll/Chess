using Chess.Player;
using Utility;

namespace Chess.UI.ViewModel
{
	public sealed class MainWindowViewModel : NotifyPropertyChangedImpl
	{
		public MainWindowViewModel()
		{
			m_gameState = GameState.NewGame();
		}

		public static readonly string GameStateProperty = PropertyNameUtility.GetPropertyName((MainWindowViewModel x) => x.GameState);
		public GameState GameState
		{
			get
			{
				return m_gameState;
			}
			set
			{
				SetPropertyField(GameStateProperty, value, ref m_gameState);
			}
		}

		GameState m_gameState;
	}
}
