using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace MyTcpServer
{
    public class ConnectedClient
    {
        public TcpClient Client { get; }
        public StreamReader Reader { get; }
        public StreamWriter Writer { get; }

        public ConnectedClient(TcpClient client)
        {
            Client = client;
            var stream = client.GetStream();
            Reader = new StreamReader(stream, Encoding.UTF8);
            Writer = new StreamWriter(stream, Encoding.UTF8) { AutoFlush = false };
        }

        public async Task SendMessageAsync(string message)
        {
            // Bọc trong try-catch để an toàn
            try
            {
                await Writer.WriteLineAsync(message);
                await Writer.FlushAsync(); // <-- GIẢI PHÁP "DỨT ĐIỂM"
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi gửi cho client: {ex.Message}");
                // (Thêm logic xử lý ngắt kết nối nếu cần)
            }
        }
    }
}