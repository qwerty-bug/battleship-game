using Battleship.Models;

namespace Battleship.Abstraction
{
    public interface IUIEngine
    {
        bool PrintBattlefield(Gameplay gameplay);
    }
}
