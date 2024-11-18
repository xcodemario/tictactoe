using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace tictactoe
{
    public partial class MainWindow : Window
    {
        private Board gameBoard;

        public MainWindow(Player startingPlayer)
        {
            InitializeComponent();
            InitializeGame(startingPlayer);
        }

        private void InitializeGame(Player startingPlayer)
        {
            gameBoard = new Board();
            gameBoard.SetCurrentPlayer(startingPlayer);
            UpdateNotification($"Turn: Player {gameBoard.CurrentPlayer}");

            // Reset grid buttons
            foreach (UIElement element in GameGrid.Children)
            {
                if (element is Button button)
                {
                    button.Content = null;
                    button.IsEnabled = true;
                }
            }
        }

        private void Square_Click(object sender, RoutedEventArgs e)
        {
            Button clickedButton = sender as Button;
            int row = Grid.GetRow(clickedButton);
            int col = Grid.GetColumn(clickedButton);

            if (gameBoard.Select(row, col))
            {
                clickedButton.Content = gameBoard.CurrentPlayer == Player.X ? "O" : "X";
                clickedButton.IsEnabled = false;

                Player winner = gameBoard.CheckWin();
                if (winner != Player.NONE)
                {
                    EndGame(winner);
                }
                else if (gameBoard.IsBoardFull())
                {
                    EndGame(Player.NONE); // Draw
                }
                else
                {
                    UpdateNotification($"Turn: Player {gameBoard.CurrentPlayer}");
                }
            }
        }

        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            gameBoard.Reset();
            foreach (UIElement element in GameGrid.Children)
            {
                if (element is Button button)
                {
                    button.Content = null;
                    button.IsEnabled = true;
                }
            }
            UpdateNotification($"Turn: Player {gameBoard.CurrentPlayer}");
        }

        private void UpdateNotification(string message)
        {
            NotificationArea.Text = message;
        }

        private void EndGame(Player winner)
        {
            foreach (UIElement element in GameGrid.Children)
            {
                if (element is Button button)
                {
                    button.IsEnabled = false;
                }
            }

            if (winner == Player.NONE)
            {
                UpdateNotification("It's a draw!");
            }
            else
            {
                UpdateNotification($"Player {winner} wins!");
            }
        }
    }
}
