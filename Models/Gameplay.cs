namespace Battleship.Models
{
    public class Gameplay
    {
        public List<GridSquare> UserHits { get; } = new List<GridSquare>();

        public List<Ship> Ships { get; } = new List<Ship>();

        public bool IsGameOver
        { 
            get
            {
                return Ships.All( x => x.IsDestroyed);
            }
        }
        
        public void AddShip(Ship ship)
            => Ships.Add(ship);

        public void AddUserShot(int xCoord, int yCoord)
        {
            var hitShip = Ships.FirstOrDefault( s => 
                s.ShipCoordinates.Any(c => c.XCoord == xCoord && c.YCoord == yCoord));

            if(hitShip != null)
                hitShip.ShipCoordinates.First( x => x.XCoord == xCoord && x.YCoord == yCoord).IsHit = true;

            var isHitSuccessfully = Ships.Any(x => x.ShipCoordinates.Any(square => square.YCoord == yCoord && square.XCoord == xCoord));
            var gridSquare = new GridSquare
            {
                XCoord = xCoord,
                YCoord = yCoord,
                IsHit = isHitSuccessfully
            };
            UserHits.Add(gridSquare);

            if(isHitSuccessfully)
                TryMarkDestroyedShip(xCoord, yCoord);
        }

        private void TryMarkDestroyedShip(int xCoord, int yCoord)
        {
            var catchedShip = Ships.First( s => s.ShipCoordinates.Where(c => c.XCoord == xCoord && c.YCoord == yCoord).Any());
            if(!catchedShip.IsDestroyed)
                return;

            var minX = catchedShip.ShipCoordinates.Min( x => x.XCoord) - 1;
            var maxX = catchedShip.ShipCoordinates.Max( x => x.XCoord) + 1;
            var minY = catchedShip.ShipCoordinates.Min( x => x.YCoord) - 1;
            var maxY = catchedShip.ShipCoordinates.Max( x => x.YCoord) + 1;

            foreach(var x in Enumerable.Range(minX, maxX - minX + 1))
            {
                if(x < 0 || x > 9)
                    continue;

                foreach (var y in Enumerable.Range(minY, maxY - minY + 1))
                {
                    if(y < 0 || y > 9)
                        continue;

                    if(!catchedShip.ShipCoordinates.Any( c => c.XCoord == x && c.YCoord == y))
                        UserHits.Add(new GridSquare
                        {
                            IsHit = false,
                            XCoord = x,
                            YCoord = y,
                        });
                }
            }
        }
    }
}
