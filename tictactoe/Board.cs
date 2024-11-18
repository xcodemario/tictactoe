﻿namespace tictactoe
{
    public class Board
    {
        private Player[,] board = new Player[3, 3];
        private Player currentPlayer;

        public Player CurrentPlayer
        {
            get { return currentPlayer; }
        }

        public Board()
        {
            Reset();
        }

        public void Reset()
        {
            for (int x = 0; x < 3; x++)
            {
                for (int y = 0; y < 3; y++)
                {
                    board[x, y] = Player.NONE;
                }
            }
            currentPlayer = Player.X; // Player X starts by default
        }

        public bool Select(int row, int col)
        {
            if (board[row, col] == Player.NONE)
            {
                board[row, col] = currentPlayer;
                currentPlayer = currentPlayer == Player.X ? Player.O : Player.X;
                return true;
            }
            return false;
        }

        public Player CheckWin()
        {
            // Check rows and columns
            for (int i = 0; i < 3; i++)
            {
                if (board[i, 0] == board[i, 1] && board[i, 1] == board[i, 2] && board[i, 0] != Player.NONE)
                    return board[i, 0];
                if (board[0, i] == board[1, i] && board[1, i] == board[2, i] && board[0, i] != Player.NONE)
                    return board[0, i];
            }

            // Check diagonals
            if (board[0, 0] == board[1, 1] && board[1, 1] == board[2, 2] && board[0, 0] != Player.NONE)
                return board[0, 0];
            if (board[0, 2] == board[1, 1] && board[1, 1] == board[2, 0] && board[0, 2] != Player.NONE)
                return board[0, 2];

            return Player.NONE;
        }

        public bool IsBoardFull()
        {
            for (int x = 0; x < 3; x++)
            {
                for (int y = 0; y < 3; y++)
                {
                    if (board[x, y] == Player.NONE)
                    {
                        return false; // Found an empty square
                    }
                }
            }
            return true; // No empty squares, board is full
        }

        public void SetCurrentPlayer(Player player)
        {
            currentPlayer = player;
        }
    }
}