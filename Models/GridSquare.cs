namespace Battleship.Models
{
    public class GridSquare
    {
        public GridSquare()
        {
        }

        public GridSquare(int xCoords, int yCoords)
        {
            XCoord = xCoords;
            YCoord = yCoords;
        }

        public int XCoord { get; set; }
        public int YCoord { get; set; }
        public bool IsHit { get; set; } = false;
    }
}
