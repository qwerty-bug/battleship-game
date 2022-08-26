using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship.Models
{
    public class Ship
    {
        public Ship(GridSquare[] shipCoordinates)
        {
            ShipCoordinates = shipCoordinates;
        }

        public bool IsDestroyed {
            get
            {
                return ShipCoordinates.All( x => x.IsHit);
            } 
        }

        public GridSquare[] ShipCoordinates { get; private set; }
    }


}
