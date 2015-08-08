using System.Collections.Generic;
using ConsoleApplication9.RouletteWheels;

namespace ConsoleApplication9.RoulettePlayers
{
    /// <summary>
    /// sort of a baseline... not really interesting but it's nice to see where this guy lands as compared to non-cheaters.
    /// I don't gamble so this is where I land ALL the time!
    /// </summary>
    public class DoNothingRoulettePlayer : IRoulettePlayer
    {
        /// <summary>
        /// If you need to specify a name and starting amount, here's a constructor for YOU
        /// </summary>
        /// <param name="name"></param>
        /// <param name="startingAmount"></param>
        public DoNothingRoulettePlayer(string name, decimal startingAmount)
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
            // I mean... I'll TAKE if if you really want me to have something...
            MoneyTotal += amount;
        }

        /// <summary>
        /// This function requests money from the player, which may be zero (but not negative).
        /// The total received by the player is implicitly deducted from his or her <see cref="IRoulettePlayer.MoneyTotal"/>.
        /// </summary>
        /// <returns>The bets that the player is making for the round</returns>
        public IReadOnlyCollection<IRouletteBet> ReceiveBet()
        {
            // but I ain't bettin NOTHING
            return new List<IRouletteBet>();
        }

        /// <summary>
        /// The Amount that the player receives at the end of a round
        /// </summary>
        /// <param name="result">Called to detail the result of the most recent roll from the wheel.</param>
        public void NotifyTheResult(RouletteResult result)
        {
            //do nothing... just keep swimming
        }
    }
}