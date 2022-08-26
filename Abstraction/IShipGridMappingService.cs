using Battleship.Models;

namespace Battleship.Abstraction
{
    public interface IShipGridMappingService
    {
        bool SaveShip(Ship ship);
    }
}
