using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessLogic
{
    public class Rook : Pieces
    {
        public override PieceType Type => PieceType.Rook;
        public override Player Color { get; }

        private static readonly Directions[] dirs = new Directions[]
        {
            Directions.North,
            Directions.South,
            Directions.East,
            Directions.West
        };
        public Rook(Player color)
        {
            Color = color;
        }
        public override Pieces Copy()
        {
            Rook copy = new Rook(Color);
            copy.HasMoved = HasMoved;
            return copy;
        }

        public override IEnumerable<Move> GetMoves(Position from, Board board)
        {
            return MovePositionsInDirs(from, board, dirs).Select(to => new NormalMove(from, to));
        }
    }
}
