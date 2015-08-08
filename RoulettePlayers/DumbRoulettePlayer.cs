using System.Collections.Generic;
using ConsoleApplication9.RouletteWheels;

namespace ConsoleApplication9.RoulettePlayers
{
    /// <summary>
    /// One baseline for dumb gamblers... the house will win because they just choose the same thing every time
    /// </summary>
    public class DumbRoulettePlayer : IRoulettePlayer
    {
        /// <summary>
        /// If you need to specify a name and starting amount, here's a constructor for YOU
        /// </summary>
        /// <param name="name"></param>
        /// <param name="startingAmount"></param>
        public DumbRoulettePlayer(string name, decimal startingAmount)
        {
            MoneyTotal = startingAmount;
            Name = name;
        }

        /// <summary>
        /// Display name on the output
        /// </summary>
        public string Name { get; private set; }

        public decimal MoneyTotal { get; private set; }

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
            MoneyTotal -= 1;
            // always bet 1 on slot 1
            return new List<IRouletteBet> { new RouletteBet { Amount = 1, ResultBetOn = RouletteResult.Slot1 } };
        }

        /// <summary>
        /// The Amount that the player receives at the end of a round
        /// </summary>
        /// <param name="result">Called to detail the result of the most recent roll from the wheel.</param>
        public void NotifyTheResult(RouletteResult result)
        {
            //do nothing... I to dum
        }
    }
}