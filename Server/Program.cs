using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using ChessData; // <-- QUAN TRỌNG: Sử dụng thư viện xử lý DB mới
using MyTcpServer;

namespace MyTcpServer
{
    class Program
    {
        private static IConfiguration _config;
        private static UserRepository _userRepo; // <-- Sử dụng lớp từ project ChessData

        static async Task Main(string[] args)
        {
            // 1. Load cấu hình từ appsettings.json
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            _config = builder.Build();

            // 2. Khởi tạo kết nối Database (Thông qua ChessData)
            string connString = _config.GetConnectionString("DefaultConnection");
            try
            {
                _userRepo = new UserRepository(connString);
                Console.WriteLine("Đã khởi tạo dịch vụ Database.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi cấu hình DB: {ex.Message}");
                return;
            }

            // 3. Khởi động Server TCP
            int port = 8888;
            TcpListener server = new TcpListener(IPAddress.Any, port);
            server.Start();
            Console.WriteLine($"Server TCP đã khởi động trên cổng {port}...");
            Console.WriteLine("Đang chờ kết nối từ client...");

            while (true)
            {
                TcpClient client = await server.AcceptTcpClientAsync();
                Console.WriteLine("Client đã kết nối!");
                // Xử lý mỗi client trên một luồng riêng biệt
                _ = HandleClientAsync(client);
            }
        }

        static async Task HandleClientAsync(TcpClient client)
        {
            // Bọc TcpClient trong ConnectedClient để dễ quản lý gửi/nhận
            ConnectedClient connectedClient = new ConnectedClient(client);

            try
            {
                // Báo cho GameManager biết có người mới (tùy chọn logic)
                GameManager.HandleClientConnect(connectedClient);

                while (true)
                {
                    // Đọc tin nhắn từ Client (sử dụng Reader của ConnectedClient)
                    string requestMessage = await connectedClient.Reader.ReadLineAsync();
                    if (requestMessage == null) break; // Client ngắt kết nối

                    Console.WriteLine($"[RECV] {requestMessage}");

                    // Xử lý yêu cầu
                    string responseMessage = await ProcessRequest(connectedClient, requestMessage);

                    // Nếu có phản hồi thì gửi lại (LOGIN/REGISTER thường có phản hồi ngay)
                    if (responseMessage != null)
                    {
                        await connectedClient.SendMessageAsync(responseMessage);
                        Console.WriteLine($"[SENT] {responseMessage}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi kết nối client: {ex.Message}");
            }
            finally
            {
                // Dọn dẹp khi ngắt kết nối
                GameManager.HandleClientDisconnect(connectedClient);

                // Đóng stream an toàn
                try { connectedClient.Reader.Close(); } catch { }
                try { connectedClient.Writer.Close(); } catch { }
                try { connectedClient.Client.Close(); } catch { }
            }
        }

        static async Task<string> ProcessRequest(ConnectedClient client, string requestMessage)
        {
            string[] parts = requestMessage.Split('|');
            string command = parts[0];

            switch (command)
            {
                // --- NHÓM LỆNH DATABASE (Chuyển sang ChessData xử lý) ---
                case "REGISTER":
                    // Format: REGISTER|User|Pass|Email|Fullname|Birthday
                    if (parts.Length == 6)
                    {
                        return await _userRepo.RegisterUserAsync(parts[1], parts[2], parts[3], parts[4], parts[5]);
                    }
                    return "ERROR|Giao thức REGISTER không đúng.";

                case "LOGIN":
                    // Format: LOGIN|User|Pass
                    if (parts.Length == 3)
                    {
                        return await _userRepo.LoginUserAsync(parts[1], parts[2]);
                    }
                    return "ERROR|Giao thức LOGIN không đúng.";

                // --- NHÓM LỆNH GAME (Chuyển sang GameManager xử lý) ---
                case "FIND_GAME":
                case "MOVE":
                case "CHAT":
                    // GameManager sẽ tự gửi phản hồi (Broadcast) nên ta trả về null ở đây
                    await GameManager.ProcessGameCommand(client, requestMessage);
                    return null;

                default:
                    return "ERROR|Lệnh không xác định.";
            }
        }
    }
}