namespace ChessLogic
{
    public class Directions
    {
        public readonly static Directions North = new Directions(-1,0);
        public readonly static Directions South =new Directions(1,0);
        public readonly static Directions East =new Directions(0,1);
        public readonly static Directions West =new Directions(0,-1);
        public readonly static Directions NorthEast = North+ East;
        public readonly static Directions NorthWest = North+West;
        public readonly static Directions SouthEast = South+East;
        public readonly static Directions SouthWest = South+West;
        public int RowDelta { get; }
        public int ColumnDelta { get; }
        public Directions(int rowDelta, int columnDelta)
        {
            RowDelta = rowDelta; 
            ColumnDelta = columnDelta;
        }

        public static Directions operator +(Directions dir1, Directions dir2)
        {
            return new Directions(dir1.RowDelta+dir2.RowDelta, dir1.ColumnDelta+dir2.ColumnDelta);
        }
        public static Directions operator *(int scalar, Directions dir)
        {
            return new Directions(scalar* dir.RowDelta, scalar* dir.ColumnDelta);
        }
    }
}
