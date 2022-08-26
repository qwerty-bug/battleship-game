using Battleship.Abstraction;
using Battleship.Models;

namespace Battleship.Services
{
    public class GameScheduler : IGameScheduler
    {
        private bool[,] shipGridPositions = new bool[10,10];
        
        private readonly IShipGridMappingService shipGridMappingService;

        public GameScheduler(IShipGridMappingService shipGridMappingService)
        {
            this.shipGridMappingService = shipGridMappingService;
        }

        public Gameplay PrepareGame()
        {
            var game = new Gameplay();
            game.AddShip(BuildShip(5));
            game.AddShip(BuildShip(4));
            game.AddShip(BuildShip(4));

            return game;
        }

        private Ship BuildShip(int shipSize)
        {
            List<int> usedInitialSeeds = new List<int>();
            bool isShipBuilt = false;
            while (!isShipBuilt)
            {
                int maxGridPosition = 10 - shipSize;

                int initialSeedX = new Random().Next(0, maxGridPosition);
                int initialSeedY = new Random().Next(0, maxGridPosition);
                bool isVertical = new Random().Next(2) == 0;

                var seedId = (isVertical ? 1 : 0) * 100 + initialSeedX * 10 + initialSeedY;
                if (usedInitialSeeds.Any(x => x == seedId))
                    continue;

                usedInitialSeeds.Add(seedId);

                var ship = new Ship(CalculateShipCoordinates(shipSize, initialSeedX, initialSeedY, isVertical));
                if (shipGridMappingService.SaveShip(ship))
                {
                    return ship;
                }
            }

            throw new Exception("Cannot prepare match!");
        }

        private static GridSquare[] CalculateShipCoordinates(int shipSize, int initialSeedX, int initialSeedY, bool isVertical)
        {
            var coordinates = new GridSquare[shipSize];
            coordinates[0] = new GridSquare
            {
                XCoord = initialSeedX,
                YCoord = initialSeedY
            };
            for (int i = 1; i < shipSize; i++)
            {
                coordinates[i] = new GridSquare
                {
                    XCoord = isVertical ? initialSeedX : initialSeedX + i,
                    YCoord = isVertical ? initialSeedY + i : initialSeedY
                };
            }

            return coordinates;
        }

        private void MapShipWithBordersToGrid(Ship ship)
        {
            foreach (var square in ship.ShipCoordinates)
            {
                int minX = square.XCoord - 1;
                minX = minX < 0 ? 0 : minX;
                int maxX = square.XCoord + 1;
                maxX = maxX > 9 ? 9 : maxX;

                int minY = square.YCoord - 1;
                minY = minY < 0 ? 0 : minY;
                int maxY = square.YCoord + 1;
                maxY = maxY > 9 ? 9 : maxY;

                for (int x = minX; x < maxX; x++)
                {
                    for (int y = minY; y < maxY; y++)
                    {
                        shipGridPositions[x,y] = true;
                    }
                }
            }
        }
    }
}
