using System;
using System.Collections.Generic;
using ConsoleApplication9.RoulettePlayers;
using ConsoleApplication9.RouletteWheels;

namespace ConsoleApplication9
{
    public class Program
    {
        static void Main()
        {
            var runners = new List<RouletteRunner>
            {
                // HEY CASINO, JUST USE THIS ONE!  It's RIG PROOF, +1 randomness
                //new RouletteRunner(new RandomRouletteWheel(), new PayoutCalculator()),
                // My rigged up roulette wheel
                new RouletteRunner(new ShadyCasinoRouletteWheel(), new PayoutCalculator())
            };

            // AAAAAAAAND here are our contestants!
            var players = new List<IRoulettePlayer>
            {
                // the boring one
                //new DoNothingRoulettePlayer("Me", 1000),
                // the Dumb one
                //new DumbRoulettePlayer("Joe Shmo", 1000),
                // aaaaaaaaand the cheeeeeeaterrrrrrrr
                new CheatingRoulettePlayer("Cheater", 1000, new PayoutCalculator())
            };

            // try the different wheels that I made
            for (var runnerIdx = 0; runnerIdx < runners.Count; runnerIdx++)
            {
                var runner = runners[runnerIdx];

                // apparently I like to be dramatic
                Console.WriteLine();
                Console.WriteLine("Aaaaaaaand now for Wheel #" + (runnerIdx + 1));

                foreach (var player in players)
                {
                    // print starting totals
                    Console.WriteLine(player.Name + " starts with $" + player.MoneyTotal);
                    runner.AddPlayer(player);
                }

                runner.Run(10000, false); // logging takes too long

                // PRINT RESULTS TO CONSOLE AND CSV
                foreach (var player in players)
                {
                    // print starting totals
                    Console.WriteLine(player.Name + " ends with $" + player.MoneyTotal);
                }
            }

            // wait until ENTER
            Console.ReadLine();
        }
    }
}
