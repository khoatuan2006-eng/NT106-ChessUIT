using System.Threading.Tasks;

namespace ChessClient
{
    public static class ClientManager
    {
        private static NetworkClient _instance;

        public static NetworkClient Instance
        {
            get
            {
                // Nếu instance chưa có hoặc đã bị ngắt kết nối thì tạo mới
                if (_instance == null || !_instance.IsConnected)
                {
                    _instance = new NetworkClient();
                }
                return _instance;
            }
        }

        public static async Task ConnectToServerAsync(string ip, int port)
        {
            if (!Instance.IsConnected)
            {
                await Instance.ConnectAsync(ip, port);
            }
        }

        // --- BỔ SUNG HÀM NÀY ---
        public static void Disconnect()
        {
            if (_instance != null)
            {
                _instance.CloseConnection();
                _instance = null; // Reset để lần sau gọi Instance sẽ tạo mới
            }
        }
    }
}