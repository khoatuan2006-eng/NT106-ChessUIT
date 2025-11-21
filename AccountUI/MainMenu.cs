using ChessLogic;
using ChessUI;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Threading;
using ChessClient;

namespace AccountUI
{
    public partial class MainMenu : Form
    {
        // --- ĐÃ XÓA ---
        // Không cần luồng lắng nghe (_listenerTask) ở đây nữa.

        public MainMenu()
        {
            InitializeComponent();

            // --- ĐÃ XÓA ---
            // Không bắt đầu lắng nghe ở đây.
            // StartServerListener(); 
        }

        // --- ĐÃ XÓA ---
        // Toàn bộ hàm "StartServerListener()" đã bị xóa
        // vì nó gây ra lỗi "cạnh tranh luồng".

        // --- HÀM XỬ LÝ TIN NHẮN (Giữ nguyên) ---
        private void HandleServerMessage(string message)
        {
            var parts = message.Split('|');
            var command = parts[0];

            if (command == "GAME_START")
            {
                // Chuyền tin nhắn "GAME_START" đi
                LaunchWpfGameWindow(message);

                // Đóng Form WinForms (MainMenu) hiện tại
                this.Close();
            }
            else if (command == "WAITING")
            {
                // Cập nhật UI
                button1.Text = "Đang chờ đối thủ...";
            }
        }

        // --- HÀM KHỞI CHẠY WPF (Giữ nguyên) ---
        private void LaunchWpfGameWindow(string gameStartMessage)
        {
            Thread wpfThread = new Thread(() =>
            {
                // Truyền tin nhắn vào hàm khởi tạo (constructor) của MainWindow
                ChessUI.MainWindow gameWindow = new ChessUI.MainWindow(gameStartMessage);

                gameWindow.Show();
                Dispatcher.Run();
            });
            wpfThread.SetApartmentState(ApartmentState.STA);
            wpfThread.Start();
        }

        // --- THAY THẾ TOÀN BỘ HÀM NÀY ---
        // Hàm "button1_Click" cũ đã bị thay thế bằng hàm "async" mới
        private async void button1_Click(object sender, EventArgs e)
        {
            // 1. Cập nhật UI
            button1.Text = "Đang tìm trận...";
            button1.Enabled = false;
            button3.Enabled = false;

            try
            {
                // 2. Gửi lệnh FIND_GAME
                await ClientManager.Instance.SendAsync("FIND_GAME");

                // 3. CHỜ tin nhắn đầu tiên (phải là WAITING hoặc GAME_START)
                // (Chạy trên luồng nền để không treo UI)
                string response = await Task.Run(() => ClientManager.Instance.WaitForMessage());

                // Xử lý tin WAITING (hoặc GAME_START nếu ghép ngay)
                // (Lưu ý: HandleServerMessage đang chạy trên luồng UI vì .Invoke trong code gốc)
                // Cần đảm bảo nó được gọi trên luồng UI
                this.Invoke((MethodInvoker)delegate
                {
                    HandleServerMessage(response);
                });


                // 4. Nếu server báo "WAITING", chúng ta cần chờ tin thứ hai (là GAME_START)
                if (response.StartsWith("WAITING"))
                {
                    // Chờ tin GAME_START
                    response = await Task.Run(() => ClientManager.Instance.WaitForMessage());

                    this.Invoke((MethodInvoker)delegate
                    {
                        HandleServerMessage(response);
                    });
                }

                // Khi HandleServerMessage nhận GAME_START, nó sẽ tự đóng Form này
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tìm trận: {ex.Message}");
                // Reset lại nút
                button1.Text = "Tìm trận nhanh";
                button1.Enabled = true;
                button3.Enabled = true;
            }
        }

        // --- CÁC HÀM KHÁC GIỮ NGUYÊN ---
        private void button3_Click(object sender, EventArgs e)
        {
            // (Chức năng khác)
        }

        private void MainMenu_Load(object sender, EventArgs e)
        {

        }
    }
}