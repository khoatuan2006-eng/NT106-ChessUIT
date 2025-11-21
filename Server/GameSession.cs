using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using ChessLogic; // Dùng chung logic và Timer

namespace MyTcpServer
{
    public class GameSession
    {
        public string SessionId { get; }
        public ConnectedClient PlayerWhite { get; }
        public ConnectedClient PlayerBlack { get; }

        private readonly GameState _gameState;

        // SỬ DỤNG CHESSTIMER TỪ CHESSLOGIC
        private readonly ChessTimer _gameTimer;

        private readonly ChatRoom _chatRoom;

        public GameSession(ConnectedClient player1, ConnectedClient player2)
        {
            SessionId = Guid.NewGuid().ToString();

            // 1. Random màu
            if (new Random().Next(2) == 0)
            {
                PlayerWhite = player1;
                PlayerBlack = player2;
            }
            else
            {
                PlayerWhite = player2;
                PlayerBlack = player1;
            }

            // 2. Khởi tạo GameState
            _gameState = new GameState(Player.White, Board.Initial());

            // 3. Khởi tạo Timer (10 phút)
            _gameTimer = new ChessTimer(10);
            _gameTimer.TimeExpired += HandleTimeExpired; // Đăng ký sự kiện hết giờ

            // 4. Khởi tạo Chat
            _chatRoom = new ChatRoom(PlayerWhite, PlayerBlack);
        }

        // --- XỬ LÝ GAME ---

        public async Task StartGame()
        {
            // Bắt đầu tính giờ cho Trắng
            _gameTimer.Start(Player.White);

            string boardString = Serialization.BoardToString(_gameState.Board);
            int wTime = _gameTimer.WhiteRemaining;
            int bTime = _gameTimer.BlackRemaining;

            // Gửi tin nhắn bắt đầu
            await PlayerWhite.SendMessageAsync($"GAME_START|WHITE|{boardString}|{wTime}|{bTime}");
            await PlayerBlack.SendMessageAsync($"GAME_START|BLACK|{boardString}|{wTime}|{bTime}");
        }

        public async Task HandleMove(ConnectedClient client, string moveString)
        {
            Console.WriteLine($"[GameSession] Nhận nước đi: {moveString}");
            try
            {
                Player player = (client == PlayerWhite) ? Player.White : Player.Black;

                // 1. Kiểm tra lượt đi
                if (player != _gameState.CurrentPlayer) return;

                // 2. Parse tọa độ
                var parts = moveString.Split('|');
                if (parts.Length != 5 ||
                    !int.TryParse(parts[1], out int r1) || !int.TryParse(parts[2], out int c1) ||
                    !int.TryParse(parts[3], out int r2) || !int.TryParse(parts[4], out int c2))
                {
                    return;
                }

                Position from = new Position(r1, c1);
                Position to = new Position(r2, c2);

                // 3. Lấy quân cờ
                Pieces piece = _gameState.Board[from];
                if (piece == null || piece.Color != player) return;

                // 4. Tìm nước đi hợp lệ (sử dụng ChessLogic)
                IEnumerable<Move> moves = piece.GetMoves(from, _gameState.Board);
                Move move = moves.FirstOrDefault(m => m.ToPos.Equals(to));

                if (move == null)
                {
                    await client.SendMessageAsync("ERROR|Nước đi không hợp lệ.");
                    return;
                }

                // 5. Thực hiện nước đi
                _gameState.MakeMove(move);

                // 6. ĐẢO LƯỢT ĐỒNG HỒ
                _gameTimer.SwitchTurn();

                // 7. Gửi UPDATE
                string boardString = Serialization.BoardToString(_gameState.Board);
                string currentPlayerStr = _gameState.CurrentPlayer.ToString().ToUpper();

                // Lấy thời gian hiện tại từ Timer
                int wTime = _gameTimer.WhiteRemaining;
                int bTime = _gameTimer.BlackRemaining;

                string updateMsg = $"UPDATE|{boardString}|{currentPlayerStr}|{wTime}|{bTime}";
                await Broadcast(updateMsg);

                // 8. Kiểm tra Hết cờ (Checkmate/Draw)
                if (_gameState.IsGameOver())
                {
                    _gameTimer.Stop();
                    string winnerMsg = _gameState.Result.Winner == null ? "HÒA cờ!" :
                                      (_gameState.Result.Winner == Player.White ? "TRẮNG thắng!" : "ĐEN thắng!");
                    await Broadcast($"GAME_OVER|{winnerMsg}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Lỗi GameSession] {ex.Message}");
            }
        }

        // --- XỬ LÝ SỰ KIỆN TIMER ---

        private void HandleTimeExpired(Player loser)
        {
            string winner = (loser == Player.White) ? "Đen" : "Trắng";
            // Chạy task background để không block timer thread
            _ = Broadcast($"GAME_OVER|{winner} thắng do đối thủ hết giờ!");
        }

        // --- XỬ LÝ CHAT ---

        public async Task BroadcastChat(ConnectedClient sender, string messageContent)
        {
            await _chatRoom.SendMessage(sender, messageContent);
        }

        // --- HÀM HỖ TRỢ ---

        private async Task Broadcast(string message)
        {
            try { await PlayerWhite.SendMessageAsync(message); } catch { }
            try { await PlayerBlack.SendMessageAsync(message); } catch { }
        }
    }
}