using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;

using ChessLogic;          // Dùng cho Board, Move, ChessTimer
using ChessClient;         // Dùng cho NetworkClient
using ChessUI.Services;    // Dùng cho ServerResponseHandler

namespace ChessUI
{
    public partial class MainWindow : Window
    {
        // --- UI COMPONENTS ---
        private readonly Image[,] pieceImages = new Image[8, 8];
        private readonly Rectangle[,] highlights = new Rectangle[8, 8];
        private readonly Dictionary<Position, Move> moveCache = new Dictionary<Position, Move>();

        // --- GAME STATE ---
        private GameState _localGameState;
        private Position selectedPos = null;
        private Player _myColor;

        // --- SERVICES ---
        private NetworkClient _networkClient;
        private ServerResponseHandler _responseHandler;

        // SỬ DỤNG CHESSTIMER TỪ CHESSLOGIC (Thay vì GameTimerService)
        private ChessTimer _gameTimer;

        public MainWindow(string gameStartMessage)
        {
            InitializeComponent();
            InitializedBoard();

            // 1. Khởi tạo Services
            _networkClient = ClientManager.Instance;
            _responseHandler = new ServerResponseHandler();

            // 2. Khởi tạo Timer (Logic chung)
            // Số phút ban đầu không quan trọng vì sẽ được Sync ngay
            _gameTimer = new ChessTimer(10);

            // 3. Đăng ký sự kiện
            RegisterEvents();

            // 4. Bắt đầu game
            try
            {
                if (!_networkClient.IsConnected) throw new Exception("Mất kết nối.");

                // Xử lý tin nhắn GAME_START ngay lập tức
                _responseHandler.ProcessMessage(gameStartMessage);

                // Bắt đầu vòng lặp lắng nghe
                StartServerListener();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khởi động: {ex.Message}");
                Close();
            }
        }

        private void RegisterEvents()
        {
            // --- SỰ KIỆN TỪ SERVER ---

            _responseHandler.GameStarted += (s, e) =>
            {
                _myColor = e.MyColor;
                _localGameState = new GameState(Player.White, e.Board);

                DrawBoard(_localGameState.Board);
                SetCursor(_localGameState.CurrentPlayer);
                this.Title = $"Game Cờ Vua - Bạn là quân {e.MyColor}";

                // Đồng bộ Timer và bắt đầu chạy
                _gameTimer.Sync(e.WhiteTime, e.BlackTime);
                _gameTimer.Start(Player.White); // Luôn là Trắng đi trước
                UpdateTimerColor();
            };

            _responseHandler.GameUpdated += (s, e) =>
            {
                _localGameState = new GameState(e.CurrentPlayer, e.Board);

                DrawBoard(_localGameState.Board);
                SetCursor(_localGameState.CurrentPlayer);

                // Đồng bộ lại thời gian từ Server (Sửa sai số)
                _gameTimer.Sync(e.WhiteTime, e.BlackTime);

                // Chuyển lượt Timer (trên Client chỉ để hiển thị)
                _gameTimer.Start(e.CurrentPlayer);
                UpdateTimerColor();
            };

            _responseHandler.ChatReceived += (s, e) => AppendChatMessage(e.Sender, e.Content);
            _responseHandler.ErrorReceived += msg => MessageBox.Show(msg, "Lỗi Server");
            _responseHandler.WaitingReceived += () => MessageBox.Show("Đang tìm đối thủ...", "Thông báo");

            _responseHandler.GameOverReceived += msg =>
            {
                _gameTimer.Stop();
                MessageBox.Show(msg, "Kết thúc");
                var menu = FindName("GameOverMenu") as UserControl;
                if (menu != null) menu.Visibility = Visibility.Visible;
            };

            // --- SỰ KIỆN TỪ TIMER (Để vẽ lên UI) ---

            _gameTimer.Tick += (wTime, bTime) =>
            {
                // Timer chạy thread riêng nên phải Invoke về UI
                Dispatcher.Invoke(() =>
                {
                    lblWhiteTime.Text = FormatTime(wTime);
                    lblBlackTime.Text = FormatTime(bTime);
                });
            };
        }

        private void StartServerListener()
        {
            Task.Run(() =>
            {
                try
                {
                    while (true)
                    {
                        string msg = _networkClient.WaitForMessage();
                        if (msg == null)
                        {
                            Dispatcher.Invoke(() => { MessageBox.Show("Mất kết nối!"); Close(); });
                            break;
                        }
                        Dispatcher.Invoke(() => _responseHandler.ProcessMessage(msg));
                    }
                }
                catch { }
            });
        }

        // ===================== UI LOGIC (Vẽ bàn cờ) =====================

        private void InitializedBoard()
        {
            for (int r = 0; r < 8; r++)
            {
                for (int c = 0; c < 8; c++)
                {
                    Image image = new Image();
                    pieceImages[r, c] = image;
                    PieceGrid.Children.Add(image);

                    Rectangle highlight = new Rectangle();
                    highlights[r, c] = highlight;
                    HighlightGrid.Children.Add(highlight);
                }
            }
        }

        private void DrawBoard(Board board)
        {
            for (int r = 0; r < 8; r++)
            {
                for (int c = 0; c < 8; c++)
                {
                    Position boardPos = (_myColor == Player.Black)
                        ? new Position(7 - r, 7 - c)
                        : new Position(r, c);

                    Pieces piece = board[boardPos];
                    pieceImages[r, c].Source = Images.GetImage(piece);
                }
            }
        }

        private void BoardGrid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (_localGameState == null) return;
            Point point = e.GetPosition(BoardGrid);
            Position pos = ToSquarePosition(point);

            if (selectedPos == null) OnFromPositionSelected(pos);
            else OnToPositionSelected(pos);
        }

        private void OnFromPositionSelected(Position pos)
        {
            if (_localGameState.CurrentPlayer != _myColor) return;
            var moves = _localGameState.MovesForPiece(pos);
            if (moves.Any())
            {
                selectedPos = pos;
                CacheMoves(moves);
                ShowHighlights();
            }
        }

        private void OnToPositionSelected(Position pos)
        {
            selectedPos = null;
            HideHighlights();
            if (moveCache.TryGetValue(pos, out Move move)) HandleMove(move);
        }

        private void HandleMove(Move move)
        {
            if (_localGameState.CurrentPlayer != _myColor) return;

            Task.Run(async () =>
            {
                if (!move.IsLegal(_localGameState.Board))
                {
                    Dispatcher.Invoke(() => MessageBox.Show("Nước đi không hợp lệ!"));
                    return;
                }
                await _networkClient.SendAsync($"MOVE|{move.FromPos.Row}|{move.FromPos.Column}|{move.ToPos.Row}|{move.ToPos.Column}");
            });
        }

        private Position ToSquarePosition(Point point)
        {
            double squareSize = BoardGrid.ActualWidth / 8;
            int row = (int)(point.Y / squareSize);
            int col = (int)(point.X / squareSize);

            if (_myColor == Player.Black) { row = 7 - row; col = 7 - col; }
            return new Position(row, col);
        }

        private void CacheMoves(IEnumerable<Move> moves)
        {
            moveCache.Clear();
            foreach (Move move in moves) moveCache[move.ToPos] = move;
        }

        private void ShowHighlights()
        {
            Color color = Color.FromArgb(159, 125, 255, 125);
            foreach (Position to in moveCache.Keys)
            {
                int r = to.Row; int c = to.Column;
                if (_myColor == Player.Black) { r = 7 - r; c = 7 - c; }
                highlights[r, c].Fill = new SolidColorBrush(color);
            }
        }

        private void HideHighlights()
        {
            foreach (Position to in moveCache.Keys)
            {
                int r = to.Row; int c = to.Column;
                if (_myColor == Player.Black) { r = 7 - r; c = 7 - c; }
                highlights[r, c].Fill = Brushes.Transparent;
            }
        }

        private void SetCursor(Player player)
        {
            if (player == Player.White) Cursor = ChessCursors.WhiteCursor;
            else Cursor = ChessCursors.BlackCursor;
        }

        // ===================== CHAT & TIMER UI =====================

        private async void btnSendChat_Click(object sender, RoutedEventArgs e)
        {
            string content = txtChatInput.Text.Trim();
            if (!string.IsNullOrEmpty(content))
            {
                await _networkClient.SendAsync($"CHAT|{content}");
                txtChatInput.Text = "";
            }
        }

        private void txtChatInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) btnSendChat_Click(sender, e);
        }

        private void AppendChatMessage(string sender, string message)
        {
            Paragraph paragraph = new Paragraph();
            Run senderRun = new Run(sender + ": ") { FontWeight = FontWeights.Bold };

            senderRun.Foreground = (sender == "Trắng" || sender == "You")
                ? new SolidColorBrush(Color.FromRgb(100, 149, 237))
                : new SolidColorBrush(Color.FromRgb(255, 165, 0));

            Run messageRun = new Run(message) { Foreground = Brushes.White };
            paragraph.Inlines.Add(senderRun);
            paragraph.Inlines.Add(messageRun);

            txtChatHistory.Document.Blocks.Add(paragraph);
            txtChatHistory.ScrollToEnd();
        }

        private string FormatTime(int s) => TimeSpan.FromSeconds(s).ToString(@"mm\:ss");

        private void UpdateTimerColor()
        {
            if (_localGameState == null) return;
            if (_localGameState.CurrentPlayer == Player.White)
            {
                lblWhiteTime.Foreground = Brushes.Red; lblBlackTime.Foreground = Brushes.White;
            }
            else
            {
                lblWhiteTime.Foreground = Brushes.White; lblBlackTime.Foreground = Brushes.Red;
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            _networkClient.CloseConnection();
        }
    }
}