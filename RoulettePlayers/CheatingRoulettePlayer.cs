using System;
using System.Collections.Generic;
using System.Threading;
using ConsoleApplication9.RouletteWheels;

namespace ConsoleApplication9.RoulettePlayers
{
    /// <summary>
    /// One baseline for dumb gamblers... the house will win because they just choose the same thing every time
    /// </summary>
    public class CheatingRoulettePlayer : IRoulettePlayer
    {
        private readonly IPayoutCalculator _payoutCalculator;
        private readonly Random _random;
        private readonly int _enumLength;
        private readonly Dictionary<RouletteResult, int> _rouletteResultProbabilities;
        private const int _randomIndex = 1000;

        /// <summary>
        /// If you need to specify a name and starting amount, here's a constructor for YOU
        /// </summary>
        /// <param name="name"></param>
        /// <param name="startingAmount"></param>
        /// <param name="payoutCalculator"></param>
        public CheatingRoulettePlayer(string name, decimal startingAmount, IPayoutCalculator payoutCalculator)
        {
            _payoutCalculator = payoutCalculator;
            MoneyTotal = startingAmount;
            Name = name;

            Thread.Sleep(1); // wait to make sure seed is unique in his program
            _random = new Random((int)DateTime.Now.Ticks);
            _enumLength = Enum.GetNames(typeof(RouletteResult)).Length;

            // build up probability array
            _rouletteResultProbabilities = new Dictionary<RouletteResult, int>();
            foreach (var idx in Enum.GetValues(typeof(RouletteResult)))
            {
                var rIdx = (RouletteResult)idx;
                _rouletteResultProbabilities[rIdx] = _randomIndex;
            }
        }

        /// <summary>
        /// Display name on the output
        /// </summary>
        public string Name { get; private set; }

        public decimal MoneyTotal { get; private set; }

        public Random Random
        {
            get { return _random; }
        }

        /// <summary>
        /// The Amount that the player receives at the end of a round
        /// </summary>
        /// <param name="amount">the winnings to add the player's <see cref="IRoulettePlayer.MoneyTotal"/> (cannot be negative)</param>
        public void PaymentToPlayer(decimal amount)
        {
            MoneyTotal += amount;
        }

        /// <summary>
        /// This function requests money from the player, which may be zero (but not negative).
        /// The total received by the player is implicitly deducted from his or her <see cref="IRoulettePlayer.MoneyTotal"/>.
        /// </summary>
        /// <returns>The bets that the player is making for the round</returns>
        public IReadOnlyCollection<IRouletteBet> ReceiveBet()
        {
            const int betAmount = 1;
            var bet = new List<IRouletteBet>();
            foreach (var idx in Enum.GetValues(typeof(RouletteResult)))
            {
                var rIdx = (RouletteResult)idx;
                var cutoff = (_enumLength / _payoutCalculator.PayoutAmount(1, true)) * _randomIndex;
                if (_rouletteResultProbabilities[rIdx] <= cutoff) continue;

                if (MoneyTotal - betAmount >= 0)
                {
                    MoneyTotal -= betAmount;
                    bet.Add(new RouletteBet {Amount = betAmount, ResultBetOn = rIdx});
                    continue;
                }

                break;
            }

            return bet;
        }

        /// <summary>
        /// The Amount that the player receives at the end of a round
        /// </summary>
        /// <param name="result">Called to detail the result of the most recent roll from the wheel.</param>
        public void NotifyTheResult(RouletteResult result)
        {
            _rouletteResultProbabilities[result] -= _enumLength;
            foreach (var idx in Enum.GetValues(typeof(RouletteResult)))
            {
                var rIdx = (RouletteResult)idx;
                _rouletteResultProbabilities[rIdx]++;
            }
        }
    }
}