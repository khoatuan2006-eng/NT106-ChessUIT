using System;
using System.Collections.Concurrent;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ChessClient
{
    public class NetworkClient
    {
        private TcpClient _client;
        private StreamReader _reader;
        private StreamWriter _writer;
        private bool _isConnected = false;

        // Hàng đợi tin nhắn an toàn luồng
        private readonly BlockingCollection<string> _messageQueue = new BlockingCollection<string>();

        public bool IsConnected => _isConnected;

        public NetworkClient()
        {
            _client = new TcpClient();
        }

        public async Task ConnectAsync(string ipAddress, int port)
        {
            if (_isConnected) return;
            try
            {
                _client = new TcpClient(); // Tạo mới mỗi lần connect lại
                await _client.ConnectAsync(ipAddress, port);
                var stream = _client.GetStream();
                _reader = new StreamReader(stream, Encoding.UTF8);
                _writer = new StreamWriter(stream, Encoding.UTF8) { AutoFlush = false };
                _isConnected = true;
                StartListening();
            }
            catch
            {
                _isConnected = false;
                throw;
            }
        }

        private void StartListening()
        {
            Task.Run(async () =>
            {
                try
                {
                    while (_isConnected)
                    {
                        string message = await _reader.ReadLineAsync();
                        if (message == null) break;
                        _messageQueue.Add(message);
                    }
                }
                catch { }
                finally
                {
                    _isConnected = false;
                    _messageQueue.CompleteAdding();
                }
            });
        }

        public string WaitForMessage()
        {
            try
            {
                return _messageQueue.Take();
            }
            catch (InvalidOperationException) { return null; }
        }

        public async Task SendAsync(string message)
        {
            if (!_isConnected) return;
            try
            {
                await _writer.WriteLineAsync(message);
                await _writer.FlushAsync();
            }
            catch { _isConnected = false; }
        }

        public void CloseConnection()
        {
            _isConnected = false;
            try { _client?.Close(); } catch { }
        }
    }
}