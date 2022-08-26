using Battleship.Abstraction;
using Microsoft.Extensions.Hosting;
using System.Diagnostics;

namespace Battleship
{
    public class GameHostedService : IHostedService
    {
        private readonly IUIEngine uiEngine;
        private readonly IGameScheduler gameScheduler;
        private readonly IInputService inputService;

        public GameHostedService(IUIEngine uiEngine,
            IGameScheduler newGameProcessor,
            IInputService inputService)
        {
            this.uiEngine = uiEngine;
            this.gameScheduler = newGameProcessor;
            this.inputService = inputService;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("Starting new game!");
            var gameplay = gameScheduler.PrepareGame();
            var gameTimer = Stopwatch.StartNew();

            while (!gameplay.IsGameOver)
            {
                uiEngine.PrintBattlefield(gameplay);
                var (X, Y) = inputService.ReadInput();
                gameplay.AddUserShot(X, Y);
            }
            gameTimer.Stop();
            var score = Convert.ToInt32(1000000 /(gameTimer.Elapsed.TotalSeconds * gameplay.UserHits.Count));
            Console.WriteLine($"\nGame finished, you have sink all the ships!\nYour score is: {score} points");
            Console.WriteLine($"Number of user shots: {gameplay.UserHits.Count}");
            Console.WriteLine($"Total game time: {gameTimer.Elapsed.Minutes} minutes {gameTimer.Elapsed.Seconds} seconds");

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("Closing game!");
            return Task.CompletedTask;
        }
    }
}
