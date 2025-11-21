using System.Collections.Concurrent;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System;

namespace MyTcpServer
{
    public static class GameManager
    {
        private static readonly ConcurrentQueue<ConnectedClient> _waitingLobby = new ConcurrentQueue<ConnectedClient>();
        private static readonly ConcurrentDictionary<ConnectedClient, GameSession> _activeGames = new ConcurrentDictionary<ConnectedClient, GameSession>();

        public static void HandleClientConnect(ConnectedClient client)
        {
            Console.WriteLine("GameManager đã ghi nhận client mới.");
        }

        public static void HandleClientDisconnect(ConnectedClient client)
        {
            if (_activeGames.TryRemove(client, out GameSession session))
            {
                // --- SỬA LỖI Ở ĐÂY: Đổi GameSession -> ConnectedClient ---
                ConnectedClient otherPlayer = (session.PlayerWhite == client) ? session.PlayerBlack : session.PlayerWhite;

                // Gửi tin nhắn cho người còn lại
                _ = otherPlayer.SendMessageAsync("GAME_OVER|Đối thủ đã rời game!");

                // Xóa đối thủ ra khỏi dictionary activeGames để tránh lỗi logic sau này
                _activeGames.TryRemove(otherPlayer, out GameSession dummy);

                Console.WriteLine($"GameSession {session.SessionId} kết thúc do người chơi thoát.");
            }

            // (Có thể thêm logic xóa khỏi _waitingLobby nếu cần, nhưng ConcurrentQueue khó xóa phần tử ở giữa)
            Console.WriteLine("Client đã ngắt kết nối và được dọn dẹp khỏi GameManager.");
        }

        public static async Task ProcessGameCommand(ConnectedClient client, string command)
        {
            var parts = command.Split('|');
            string action = parts[0];

            if (_activeGames.TryGetValue(client, out GameSession session))
            {
                switch (action)
                {
                    case "MOVE":
                        await session.HandleMove(client, command);
                        break;
                    case "CHAT":
                        if (parts.Length > 1)
                        {
                            string messageContent = parts[1];
                            await session.BroadcastChat(client, messageContent);
                        }
                        break;
                }
            }
            else if (action == "FIND_GAME")
            {
                await AddToLobby(client);
            }
        }

        private static async Task AddToLobby(ConnectedClient client)
        {
            _waitingLobby.Enqueue(client);

            if (_waitingLobby.Count >= 2)
            {
                if (_waitingLobby.TryDequeue(out ConnectedClient player1) &&
                    _waitingLobby.TryDequeue(out ConnectedClient player2))
                {
                    GameSession newSession = new GameSession(player1, player2);

                    _activeGames[player1] = newSession;
                    _activeGames[player2] = newSession;

                    Console.WriteLine($"Đã ghép cặp! Bắt đầu GameSession: {newSession.SessionId}");

                    await newSession.StartGame();
                }
            }
            else
            {
                await client.SendMessageAsync("WAITING");
                Console.WriteLine("Client đã vào hàng đợi.");
            }
        }
    }
}