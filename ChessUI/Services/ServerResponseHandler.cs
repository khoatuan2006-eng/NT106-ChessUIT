using ChessLogic;
using System;
using System.Linq;

namespace ChessUI.Services
{
    public class GameStartEventArgs : EventArgs
    {
        public required Player MyColor { get; set; }
        public required Board Board { get; set; }
        public int WhiteTime { get; set; }
        public int BlackTime { get; set; }
    }

    public class GameUpdateEventArgs : EventArgs
    {
        public required Board Board { get; set; }
        public required Player CurrentPlayer { get; set; }
        public int WhiteTime { get; set; }
        public int BlackTime { get; set; }
    }

    public class ChatEventArgs : EventArgs
    {
        public required string Sender { get; set; }
        public required string Content { get; set; }
    }

    public class ServerResponseHandler
    {
        public event EventHandler<GameStartEventArgs>? GameStarted;
        public event EventHandler<GameUpdateEventArgs>? GameUpdated;
        public event EventHandler<ChatEventArgs>? ChatReceived;
        public event Action<string>? ErrorReceived;
        public event Action<string>? GameOverReceived;
        public event Action? WaitingReceived;

        public void ProcessMessage(string message)
        {
            if (string.IsNullOrEmpty(message)) return;
            var parts = message.Split('|');
            var command = parts[0];

            try
            {
                switch (command)
                {
                    case "GAME_START":
                        var argsStart = new GameStartEventArgs
                        {
                            MyColor = (parts[1] == "WHITE") ? Player.White : Player.Black,
                            Board = Serialization.ParseBoardString(parts[2]),
                            WhiteTime = (parts.Length >= 5) ? int.Parse(parts[3]) : 0,
                            BlackTime = (parts.Length >= 5) ? int.Parse(parts[4]) : 0
                        };
                        GameStarted?.Invoke(this, argsStart);
                        break;

                    case "UPDATE":
                        var argsUpdate = new GameUpdateEventArgs
                        {
                            Board = Serialization.ParseBoardString(parts[1]),
                            CurrentPlayer = (parts[2] == "WHITE") ? Player.White : Player.Black,
                            WhiteTime = (parts.Length >= 5) ? int.Parse(parts[3]) : 0,
                            BlackTime = (parts.Length >= 5) ? int.Parse(parts[4]) : 0
                        };
                        GameUpdated?.Invoke(this, argsUpdate);
                        break;

                    case "CHAT_RECEIVE":
                        if (parts.Length >= 3)
                        {
                            ChatReceived?.Invoke(this, new ChatEventArgs
                            {
                                Sender = parts[1],
                                Content = string.Join("|", parts.Skip(2))
                            });
                        }
                        break;

                    case "ERROR": ErrorReceived?.Invoke(parts[1]); break;
                    case "GAME_OVER": GameOverReceived?.Invoke(parts[1]); break;
                    case "WAITING": WaitingReceived?.Invoke(); break;
                }
            }
            catch { }
        }
    }
}