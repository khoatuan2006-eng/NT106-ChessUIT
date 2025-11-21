using System;
using System.Threading.Tasks;

namespace MyTcpServer
{
    public class ChatRoom
    {
        private readonly ConnectedClient _playerWhite;
        private readonly ConnectedClient _playerBlack;

        public ChatRoom(ConnectedClient white, ConnectedClient black)
        {
            _playerWhite = white;
            _playerBlack = black;
        }

        public async Task SendMessage(ConnectedClient sender, string messageContent)
        {
            // 1. Xác định tên người gửi
            string senderName = (sender == _playerWhite) ? "Trắng" : "Đen";
            string senderColorCode = (sender == _playerWhite) ? "WHITE" : "BLACK"; // Dùng cho sau này nếu cần

            // 2. Tạo lệnh gửi đi
            // Format: CHAT_RECEIVE | Tên hiển thị | Nội dung
            string command = $"CHAT_RECEIVE|{senderName}|{messageContent}";

            // 3. Gửi cho cả hai (Broadcast)
            await SafeSend(_playerWhite, command);
            await SafeSend(_playerBlack, command);
        }

        // Hàm gửi an toàn riêng cho Chat (không làm ảnh hưởng luồng game chính)
        private async Task SafeSend(ConnectedClient client, string message)
        {
            try
            {
                await client.SendMessageAsync(message);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Chat Error] Không thể gửi tin cho client: {ex.Message}");
            }
        }
    }
}