using Battleship.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship.Services
{
    public class InputService : IInputService
    {
        public (int X, int Y) ReadInput()
        {
            while (true)
            {
                Console.WriteLine("Type new coordinates (e.g. A5): ");
                var userInput = Console.ReadLine();
                if(userInput is null || userInput.Length < 2 || userInput.Length > 3)
                {
                    PrintError(userInput);
                    continue;
                }

                var inputColumn = userInput.ElementAt(0);
                inputColumn = char.ToUpper(inputColumn);
                if(!Enum.TryParse(inputColumn.ToString(), out LetterRow letter))
                {
                    PrintError(userInput);
                    continue;
                }

                var inputRow = userInput.Substring(1,userInput.Length-1);
                int.TryParse(inputRow?.ToString(), out var columnNumber);
                if (columnNumber < 1 || columnNumber > 10)
                {
                    PrintError(userInput);
                    continue;
                }

                columnNumber--;
                return (columnNumber, (int)letter);
            }
        }

        private static void PrintError(string? userInput)
        {
            Console.WriteLine($"Invlaid cooridnates: {userInput}!. Please put valid value.");
        }
    }
}
