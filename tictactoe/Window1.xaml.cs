using System.Windows;

namespace tictactoe
{
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
        }

        private void Image_X_Click(object sender, RoutedEventArgs e)
        {
            OpenMainWindow(Player.X);
        }

        private void Image_O_Click(object sender, RoutedEventArgs e)
        {
            OpenMainWindow(Player.O);
        }

        private void OpenMainWindow(Player player)
        {
            MainWindow mainWindow = new MainWindow(player);
            mainWindow.Show();
            this.Close();
        }
    }
}
