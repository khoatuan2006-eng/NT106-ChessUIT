using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessLogic
{
    public class Queen : Pieces
    {
        public override PieceType Type => PieceType.Queen;
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
        public Queen(Player color)
        {
            Color = color;
        }
        public override Pieces Copy()
        {
            Queen copy = new Queen(Color);
            copy.HasMoved = HasMoved;
            return copy;
        }

        public override IEnumerable<Move> GetMoves(Position from, Board board)
        {
            return MovePositionsInDirs(from, board, dirs).Select(to => new NormalMove(from, to));
        }
    }
}
