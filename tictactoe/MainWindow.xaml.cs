using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using tictactoe;

namespace TicTacToe
{
    public partial class MainWindow : Window
    {
        private Player[,] board;
        private Player currentPlayer;
        private bool gameEnded;
        private int gamesPlayed;
        private int gamesWonX;
        private int gamesWonO;

        public MainWindow(Player startingPlayer)
        {
            InitializeComponent();
            board = new Player[3, 3];
            currentPlayer = startingPlayer;
            gameEnded = false;
            gamesPlayed = 0;
            gamesWonX = 0;
            gamesWonO = 0;
            UpdateBoardUI();
            UpdateNotificationArea();
        }

        private void UpdateBoardUI()
        {
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    var button = (Button)GameBoardGrid.Children[row * 3 + col];
                    if (board[row, col] == Player.X)
                    {
                        button.Content = new Image
                        {
                            Source = new BitmapImage(new Uri("img/tic-tac-toe_x.png", UriKind.Relative))
                        };
                    }
                    else if (board[row, col] == Player.O)
                    {
                        button.Content = new Image
                        {
                            Source = new BitmapImage(new Uri("img/tic-tac-toe_o.png", UriKind.Relative))
                        };
                    }
                    else
                    {
                        button.Content = null;
                    }

                    button.IsEnabled = !gameEnded;
                }
            }
        }

        private void UpdateNotificationArea()
        {
            GamesPlayedLabel.Content = $"Games Played: {gamesPlayed}| Games Won: {gamesWonX + gamesWonO}";
            WinRatioLabel.Content = gamesPlayed > 0
                ? $"Win Ratio: {(float)(gamesWonX + gamesWonO) / gamesPlayed * 100:F2}%" //found this in stack overflow
                : "Win Ratio: 0%";
            TurnLabel.Content = $"Turn: Player {(currentPlayer == Player.X ? "X" : "O")}";
        }

        private void Cell_Click(object sender, RoutedEventArgs e)
        {
            if (gameEnded)
                return;

            var button = sender as Button;
            int row = Grid.GetRow(button);
            int col = Grid.GetColumn(button);

            if (board[row, col] == Player.NONE)
            {
                board[row, col] = currentPlayer;
                UpdateBoardUI();

                if (CheckForWinner())
                {
                    WinnerLabel.Content = $"{currentPlayer} wins!";
                    gameEnded = true;
                    gamesPlayed++;
                    if (currentPlayer == Player.X)
                        gamesWonX++;
                    else
                        gamesWonO++;
                    UpdateNotificationArea();
                    RematchButton.Visibility = Visibility.Visible;
                    return;
                }
                else if (CheckForDraw())
                {
                    WinnerLabel.Content = "It's a draw!";
                    gameEnded = true;
                    gamesPlayed++;
                    UpdateNotificationArea();
                    RematchButton.Visibility = Visibility.Visible;
                    return;
                }
                else
                {
                    if (currentPlayer == Player.X)
                    {
                        currentPlayer = Player.O;
                    }
                    else
                    {
                        currentPlayer = Player.X;
                    }
                }

                UpdateNotificationArea();
            }
        }


        private bool CheckForWinner()
        {
            for (int i = 0; i < 3; i++)
            {
                if (board[i, 0] == currentPlayer && board[i, 1] == currentPlayer && board[i, 2] == currentPlayer)
                    return true;
                if (board[0, i] == currentPlayer && board[1, i] == currentPlayer && board[2, i] == currentPlayer)
                    return true;
            }

            if (board[0, 0] == currentPlayer && board[1, 1] == currentPlayer && board[2, 2] == currentPlayer)
                return true;
            if (board[0, 2] == currentPlayer && board[1, 1] == currentPlayer && board[2, 0] == currentPlayer)
                return true;

            return false;
        }

        private bool CheckForDraw()
        {
            foreach (var cell in board)
            {
                if (cell == Player.NONE)
                    return false;
            }
            return true;
        }

        private void RematchButton_Click(object sender, RoutedEventArgs e)
        {
            board = new Player[3, 3];
            currentPlayer = Player.X; 
            gameEnded = false;
            WinnerLabel.Content = "";
            UpdateBoardUI();
            UpdateNotificationArea();
            RematchButton.Visibility = Visibility.Collapsed;
        }
    }

   
}
