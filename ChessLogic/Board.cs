using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessLogic
{
    public class Board
    {
        private readonly Pieces[,] pieces = new Pieces[8, 8];
        public Pieces this[int row, int col]
        {
            get {return pieces[row, col];}
            set { pieces[row, col] = value;}
        }
        public Pieces this [Position pos]
        {
            get { return this[pos.Row, pos.Column]; }
            set { this [pos.Row, pos.Column] = value;}
        }
        public static Board Initial()
        {
            Board board =new Board();
            board.AddStartPieces();
            return board;
        }
        private void AddStartPieces()
        {
            this[0, 0] = new Rook(Player.Black);
            this[0, 1] = new Knight(Player.Black);
            this[0, 2]=new Bishop(Player.Black);
            this[0, 3]=new Queen(Player.Black);
            this[0, 4]= new King(Player.Black);
            this[0, 5] = new Bishop(Player.Black);
            this[0, 6]=new Knight(Player.Black);
            this[0, 7] = new Rook(Player.Black);

            this[7, 0] = new Rook(Player.White);
            this[7, 1] = new Knight(Player.White);
            this[7, 2] = new Bishop(Player.White);
            this[7, 3] = new Queen(Player.White);
            this[7, 4] = new King(Player.White);
            this[7, 5] = new Bishop(Player.White);
            this[7, 6] = new Knight(Player.White);
            this[7, 7] = new Rook(Player.White);

            for(int i=0;i<8;i++)
            {
                this[1, i]=new Pawn(Player.Black);
                this[6, i]=new Pawn(Player.White);
            }
        }
        public static bool IsInside(Position pos)
        {
            return pos.Row >= 0 && pos.Row < 8 && pos.Column >= 0 && pos.Column < 8;
        }
        public bool IsEmty(Position pos)
        {
            return this[pos] == null;
        }

        public IEnumerable<Position> PiecePosition()
        {
            for (int r=0; r<8;r++)
            {
                for (int c= 0; c<8;c++)
                {
                    Position pos = new Position(r, c);

                    if(!IsEmty(pos))
                    {
                        yield return pos;
                    }
                }
            }
        }

        public IEnumerable<Position> PiecePositionsFor(Player player)
        {
            return PiecePosition().Where(pos => this[pos].Color == player);
        }

        public bool IsIncheck(Player player )
        {
            return PiecePositionsFor(player.Opponent()).Any(pos =>
            {
                Pieces pieces = this[pos];
                return pieces.CanCaptureOpponentKing(pos, this);
            });
        }

        public Board Copy()
        {
            Board copy = new Board();

            foreach (Position pos in PiecePosition())
            {
                copy[pos] = this[pos].Copy();
            }
            return copy;

        }
    }
}
