using Battleship.Abstraction;
using Battleship.Models;
using System.Text;

namespace Battleship.Services
{
    public class UIEngine : IUIEngine
    {
        private const char SuccessfullHit = 'X';
        private const char MissedHit = '-';

        private static char[,] grid = new char[10,10];

        public UIEngine()
        {
            for (int x = 0; x < 10; x++)
            {
                for (int y = 0; y < 10; y++)
                {
                    grid[x,y] = ' ';
                }
            }
        }

        public bool PrintBattlefield(Gameplay gameplay)
        {
            Console.Clear();

            foreach (var hit in gameplay.UserHits)
            {
                grid[hit.XCoord, hit.YCoord] = hit.IsHit ? SuccessfullHit : MissedHit;
            }

            var output = new StringBuilder()
                .AppendLine("    1   2   3   4   5   6   7   8   9   10")
                .AppendLine("  -----------------------------------------");

            foreach (var row in Enumerable.Range(0,10))
            {

                output.AppendLine($"{(LetterRow)row} | {grid[0, row]} | {grid[1, row]} | {grid[2, row]} | {grid[3, row]} | {grid[4, row]} | {grid[5, row]} | {grid[6, row]} | {grid[7, row]} | {grid[8, row]} | {grid[9, row]} |");
                output.AppendLine("                                           ");
            }
//@"A |   |   |   |   |   |   |   |   | O | x |
                                           
//B |   |   |   |   |   |   |   |   | O | x |
                                           
//C |   |   |   |   |   |   |   | x |   |   |
                                           
//D |   |   |   |   |   |   |   |   |   |   |
                                           
//E |   |   |   |   |   |   |   |   |   |   |
                                           
//F |   |   |   |   |   |   |   |   |   |   |
                                           
//G |   |   |   |   |   |   |   |   |   |   |
                                           
//H |   |   |   |   |   |   |   |   |   |   |
                                           
//I |   |   |   |   |   |   |   |   |   |   |
                                           
//J |   |   |   |   |   |   |   |   |   |   |
            output.AppendLine("  -----------------------------------------");

            Console.Write(output);
            return true;
        }
    }

    public enum LetterRow
    {
        A = 0,
        B = 1,
        C = 2,
        D = 3,
        E = 4,
        F = 5,
        G = 6,
        H = 7,
        I = 8,
        J = 9,
    }
}
