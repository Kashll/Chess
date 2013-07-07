using Chess.Player.Board;
using Chess.Player.Utility;

namespace Chess.Player
{
    public sealed class GameState
    {
        public static GameState NewGame()
        {
            GameState gameState = new GameState
            {
                PlayerTurn = Color.White,
                Board = BoardUtility.StartingBoard()
            };

            return gameState;
        }

        public Color PlayerTurn
        {
            get { return m_playerTurn; }
            set { m_playerTurn = value; }
        }

        public Square[,] Board
        {
            get { return m_board; }
            set { m_board = value; }
        }

        Color m_playerTurn;
        Square[,] m_board;
    }
}
