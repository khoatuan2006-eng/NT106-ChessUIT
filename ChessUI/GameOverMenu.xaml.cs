using System.Windows;
using System.Windows.Controls;

namespace ChessUI
{
    public partial class GameOverMenu : UserControl
    {
        public GameOverMenu()
        {
            InitializeComponent();
        }

        private void Restart_Click(object sender, RoutedEventArgs e)
        {
            // Logic chơi lại: Ẩn menu đi hoặc gửi lệnh xin chơi lại
            this.Visibility = Visibility.Collapsed;

            // (Tùy chọn) Có thể thêm logic gửi lệnh "REMATCH" lên server ở đây
            MessageBox.Show("Chức năng Chơi lại đang phát triển!");
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            // Thoát game (Đóng cửa sổ chính)
            Application.Current.Shutdown();
        }
    }
}