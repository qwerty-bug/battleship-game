namespace Battleship.Abstraction
{
    public interface IInputService
    {
        (int X, int Y) ReadInput();
    }
}
