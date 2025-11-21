using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ChessLogic
{
    public class Pawn : Pieces
    {
        public override PieceType Type => PieceType.Pawn;
        public override Player Color { get; }

        public readonly Directions forward;

        public Pawn(Player color)
        {
            Color = color;
            if (color == Player.White)
            {
                forward = Directions.North;
            }
            else if (color == Player.Black)
            {
                forward = Directions.South;
            }
        }
        public override Pieces Copy()
        {
            Pawn copy = new Pawn(Color);
            copy.HasMoved = HasMoved;
            return copy;
        }

        private static bool CanMoveTo(Position pos, Board board)
        {
            return Board.IsInside(pos) && board.IsEmty(pos);
        }

        private bool CanCaptureAt(Position pos, Board board)
        {
            if (!Board.IsInside(pos) || board.IsEmty(pos))
            {
                return false;
            }

            return board[pos].Color != Color;
        }

        private IEnumerable<Move> ForwardMoves(Position from, Board board)
        {
            // Tính vị trí 1 bước tiến
            Position oneMovePos = from + forward;

            // Kiểm tra xem có thể đi 1 bước không
            if (CanMoveTo(oneMovePos, board))
            {
                // Nếu được, thêm nước đi 1 bước
                yield return new NormalMove(from, oneMovePos);

                // Tính vị trí 2 bước tiến
                Position twoMovePos = oneMovePos + forward; // (hoặc from + 2 * forward)

                // Nếu quân Tốt chưa di chuyển VÀ ô 2 bước cũng trống
                // (lưu ý: chúng ta đã biết ô 1 bước là trống từ khối if bên ngoài)
                if (!HasMoved && CanMoveTo(twoMovePos, board))
                {
                    // Thêm nước đi 2 bước
                    yield return new NormalMove(from, twoMovePos);
                }
            }
        }

        private IEnumerable<Move> DiagonalMoves(Position from , Board board)
        {
            foreach (Directions dir in new Directions[] { Directions.West, Directions.East })
            {
                Position to = from + forward + dir;

                if(CanCaptureAt(to, board))
                {
                    yield return new NormalMove(from, to);
                }
            }
        }


        public override IEnumerable<Move> GetMoves(Position from, Board board)
        {
            return ForwardMoves(from, board).Concat(DiagonalMoves(from, board));
        }

        public override bool CanCaptureOpponentKing(Position from, Board board)
        {
            return DiagonalMoves(from, board).Any(move =>
            {
                Pieces piece = board[move.ToPos];
                return piece != null && piece.Type == PieceType.King;
            });
        }

    }
}