using tictactoe;

namespace TicTacToe
{
    public class Board
    {
        public Player[,] BoardArray { get; private set; }

        public Board()
        {
            BoardArray = new Player[3, 3];
        }

        public bool Select(int row, int col, Player currentPlayer)
        {
            if (BoardArray[row, col] == Player.None)
            {
                BoardArray[row, col] = currentPlayer;
                return true;
            }
            return false;
        }

        public bool CheckWinner(Player player)
        {
            for (int i = 0; i < 3; i++)
            {
                if (BoardArray[i, 0] == player && BoardArray[i, 1] == player && BoardArray[i, 2] == player)
                    return true;

                if (BoardArray[0, i] == player && BoardArray[1, i] == player && BoardArray[2, i] == player)
                    return true;
            }

            if (BoardArray[0, 0] == player && BoardArray[1, 1] == player && BoardArray[2, 2] == player)
                return true;

            if (BoardArray[0, 2] == player && BoardArray[1, 1] == player && BoardArray[2, 0] == player)
                return true;

            return false;
        }

        public bool IsBoardFull()
        {
            foreach (var cell in BoardArray)
            {
                if (cell == Player.None)
                    return false;
            }
            return true;
        }
    }
}
