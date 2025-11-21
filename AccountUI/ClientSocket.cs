using System;
using System.Net.Sockets;
using System.Text;

namespace AccountUI
{
    public static class ClientSocket
    {
        private static TcpClient? client;
        private static NetworkStream? stream;

        public static bool Connect(string ipAddress, int port)
        {
            try
            {
                client = new TcpClient();
                client.Connect(ipAddress, port);
                stream = client.GetStream();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static string SendAndReceive(string message)
        {
            try
            {
                byte[] dataToSend = Encoding.UTF8.GetBytes(message + "\n");
                stream?.Write(dataToSend, 0, dataToSend.Length);

                byte[] buffer = new byte[2048];
                int bytesRead = stream!.Read(buffer, 0, buffer.Length);
                string response = Encoding.UTF8.GetString(buffer, 0, bytesRead).Trim();
                return response;
            }
            catch (Exception ex)
            {
                return "ERROR|Mất kết nối đến server: " + ex.Message;
            }
        }
        public static void Disconnect()
        {
            if (client != null && client.Connected)
            {
                stream?.Close();
                client.Close();
            }
        }
    }
}