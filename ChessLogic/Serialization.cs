using System;
using System.Text;

namespace ChessLogic
{
    // Lớp static để chuyển đổi Board thành String và ngược lại
    public static class Serialization
    {
        // --- Chuyển Board thành chuỗi 64 ký tự ---
        public static string BoardToString(Board board)
        {
            StringBuilder sb = new StringBuilder(64);
            for (int r = 0; r < 8; r++)
            {
                for (int c = 0; c < 8; c++)
                {
                    Pieces piece = board[r, c];
                    if (piece == null)
                    {
                        sb.Append('.'); // Ô trống
                    }
                    else
                    {
                        sb.Append(GetPieceChar(piece));
                    }
                }
            }
            return sb.ToString();
        }

        // --- Chuyển chuỗi 64 ký tự thành Board ---
        public static Board ParseBoardString(string boardString)
        {
            if (boardString.Length != 64)
            {
                throw new ArgumentException("Chuỗi trạng thái bàn cờ phải có 64 ký tự.");
            }

            Board board = new Board(); // Giả sử Board() tạo ra một bàn cờ trống
            for (int i = 0; i < 64; i++)
            {
                int r = i / 8;
                int c = i % 8;
                char pieceChar = boardString[i];

                if (pieceChar != '.')
                {
                    board[r, c] = GetPieceFromChar(pieceChar);
                }
            }
            return board;
        }

        // --- Hàm hỗ trợ: Lấy ký tự từ quân cờ ---
        // (Chữ hoa = Trắng, Chữ thường = Đen)
        private static char GetPieceChar(Pieces piece)
        {
            char c = piece.Type switch
            {
                PieceType.Pawn => 'P',
                PieceType.Rook => 'R',
                PieceType.Knight => 'N',
                PieceType.Bishop => 'B',
                PieceType.Queen => 'Q',
                PieceType.King => 'K',
                _ => '?'
            };

            return (piece.Color == Player.White) ? c : char.ToLower(c);
        }

        // --- Hàm hỗ trợ: Lấy quân cờ từ ký tự ---
        private static Pieces GetPieceFromChar(char c)
        {
            Player color = char.IsUpper(c) ? Player.White : Player.Black;
            PieceType type = char.ToUpper(c) switch
            {
                'P' => PieceType.Pawn,
                'R' => PieceType.Rook,
                'N' => PieceType.Knight,
                'B' => PieceType.Bishop,
                'Q' => PieceType.Queen,
                'K' => PieceType.King,
                _ => throw new ArgumentException($"Ký tự quân cờ không xác định: {c}")
            };

            // Chúng ta cần một cách để tạo quân cờ.
            // Giả sử các lớp quân cờ của bạn (Pawn, Rook, ...) đều kế thừa từ 'Pieces'
            // và có constructor (Player color)
            return type switch
            {
                PieceType.Pawn => new Pawn(color),
                PieceType.Rook => new Rook(color),
                PieceType.Knight => new Knight(color),
                PieceType.Bishop => new Bishop(color),
                PieceType.Queen => new Queen(color),
                PieceType.King => new King(color),
                _ => null // Sẽ không xảy ra
            };
        }
    }
}

// GHI CHÚ: Đoạn mã 'GetPieceFromChar' ở trên giả định bạn có các lớp
// new Pawn(color), new Rook(color),... 
// Nếu cấu trúc của bạn khác, bạn sẽ cần điều chỉnh phần đó.