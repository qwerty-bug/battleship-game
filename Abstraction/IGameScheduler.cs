using Battleship.Models;

namespace Battleship.Abstraction
{
    public interface IGameScheduler
    {
        Gameplay PrepareGame();
    }
}
