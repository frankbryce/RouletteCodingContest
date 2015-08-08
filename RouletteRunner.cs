using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using ConsoleApplication9.RoulettePlayers;
using ConsoleApplication9.RouletteWheels;

namespace ConsoleApplication9
{
    /// <summary>
    /// The main entry into the simulation.  Once the runner starts, we can run it for a while, then run it some more or add players as we want.
    /// </summary>
    public class RouletteRunner
    {
        private readonly IRouletteWheel _wheel;
        private readonly IPayoutCalculator _payoutCalculator;
        private readonly ConcurrentBag<IRoulettePlayer> _players;
        private readonly Dictionary<IRoulettePlayer, IReadOnlyCollection<IRouletteBet>> _tableBets;

        /// <summary>
        /// Inject in dependencies, in the event we want to test out different wheel spinners or payout calculators
        /// </summary>
        /// <param name="wheel">The wheel to use during the simulation.  The only thing we need is to get a series of results from it</param>
        /// <param name="payoutCalculator">Calculate what the payout is for the roll.  I assume we can only do a straight bet, for simplicity</param>
        public RouletteRunner(
            IRouletteWheel wheel,
            IPayoutCalculator payoutCalculator)
        {
            _wheel = wheel;
            _payoutCalculator = payoutCalculator;
            _players = new ConcurrentBag<IRoulettePlayer>();
            _tableBets = new Dictionary<IRoulettePlayer, IReadOnlyCollection<IRouletteBet>>();
        }

        /// <summary>
        /// Add a player to the mix!  Why not...
        /// </summary>
        /// <param name="player"></param>
        public void AddPlayer(IRoulettePlayer player)
        {
            _players.Add(player);
        }

        // run for a given number of spins.
        public void Run(uint numberOfRounds, bool verbose)
        {
            for (var rIdx = 0; rIdx < numberOfRounds; rIdx++)
            {
                // clear all bets for the new round
                _tableBets.Clear();

                // display top of the round
                if (verbose)
                {
                    Console.WriteLine();
                    Console.WriteLine("---- TOP OF THE ROUND " + rIdx + " ----");
                    Console.WriteLine("PLAYERS");
                }

                // collect bet from player
                foreach (var player in _players)
                {
                    if (verbose) Console.Write(player.Name + ": $" + player.MoneyTotal);
                    if (player.MoneyTotal <= 0) continue;

                    var bet = player.ReceiveBet();
                    _tableBets.Add(player, bet);

                    if (verbose)
                    {
                        Console.Write(", betting $" + bet.Aggregate<IRouletteBet, decimal>(0, (total, next) => total + next.Amount));
                        Console.WriteLine();
                    }
                }

                // once we have the bets, spin that thang
                var result = _wheel.Spin();

                if (verbose)
                {
                    Console.WriteLine();
                    Console.WriteLine("SPIN: " + result);
                    Console.WriteLine();
                }

                // handle all of the players' bets
                foreach (var player in _players)
                {
                    // simulating the active watching of the wheel by the player
                    player.NotifyTheResult(result);

                    // see if the player's bet is correct
                    var matchingBet = _tableBets[player].FirstOrDefault(x => x.ResultBetOn == result);
                    if (matchingBet != null)
                    {
                        // pay the player if they won
                        var betAmount = matchingBet.Amount;
                        var paymentAmount = _payoutCalculator.PayoutAmount(betAmount, true);
                        player.PaymentToPlayer(paymentAmount);
                    }

                    if (verbose)
                    {
                        Console.WriteLine("--- BOTTOM OF THE ROUND " + rIdx + " --");
                        Console.WriteLine("PLAYERS");
                        Console.Write(player.Name + ": $" + player.MoneyTotal);
                        Console.WriteLine();
                    }
                }
            }
        }
    }
}