using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessLogic
{
    public class King : Pieces
    {
        public override PieceType Type => PieceType.King;
        public override Player Color { get; }

        private static readonly Directions[] dirs = new Directions[]
        {
            Directions.North,
            Directions.South,
            Directions.East,
            Directions.West,
            Directions.NorthWest,
            Directions.NorthEast,
            Directions.SouthWest,
            Directions.SouthEast
        };
        public King(Player color)
        {
            Color = color;
        }
        public override Pieces Copy()
        {
            King copy = new King(Color);
            copy.HasMoved = HasMoved;
            return copy;
        }

        private IEnumerable<Position> MovePositions(Position from, Board board)
        {
            foreach (Directions dir in dirs)
            {
                Position to = from + dir;

                if (!Board.IsInside(to))
                {
                    continue;
                }

                if (board.IsEmty(to) || board[to].Color != Color)
                {
                    yield return to;
                }
            }

        }

        public override IEnumerable<Move> GetMoves(Position from, Board board)
        {
            foreach (Position to in MovePositions(from, board))
            {
                yield return new NormalMove(from, to);
            }
        }

        public override bool CanCaptureOpponentKing(Position from, Board board)
        {
            return MovePositions(from, board).Any(to =>
            {
                Pieces piece = board[to];
                return piece != null && piece.Type == PieceType.King;
            });
        }
    }
}
