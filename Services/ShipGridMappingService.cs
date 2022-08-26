using Battleship.Abstraction;
using Battleship.Models;

namespace Battleship.Services
{
    public class ShipGridMappingService : IShipGridMappingService
    {
        private bool[,] shipGridPositions = new bool[10, 10];

        public bool SaveShip(Ship ship)
        {
            var canBeSave = CanSqueresBeReserved(ship.ShipCoordinates);
            if(!canBeSave)
                return false;

            foreach (var coords in ship.ShipCoordinates)
            {
                if (shipGridPositions[coords.XCoord, coords.YCoord])
                    throw new InvalidOperationException($"Cannot save ship on position X:{coords.XCoord} Y:{coords.YCoord}, because it is already reserved!");

                shipGridPositions[coords.XCoord, coords.YCoord] = true;

            }
            return true;
        }
        private void MapShipWithBordersToGrid(Ship ship)
        {
        }
        private bool CanSqueresBeReserved(GridSquare[] squares)
        {
            foreach (var square in squares)
            {
                int minX = square.XCoord - 1;
                minX = minX < 0 ? 0 : minX;
                int maxX = square.XCoord + 1;
                maxX = maxX > 9 ? 9 : maxX;

                int minY = square.YCoord - 1;
                minY = minY < 0 ? 0 : minY;
                int maxY = square.YCoord + 1;
                maxY = maxY > 9 ? 9 : maxY;

                bool canSquereBeReserved = true;
                for (int x = minX; x <= maxX; x++)
                {
                    for (int y = minY; y <= maxY; y++)
                    {
                        canSquereBeReserved &= shipGridPositions[x, y] == false;
                        if (!canSquereBeReserved)
                            return false;
                    }
                }
            }
            return true;
        }
    }
}
