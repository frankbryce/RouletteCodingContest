using System.Collections.Generic;
using ConsoleApplication9.RouletteWheels;

namespace ConsoleApplication9.RoulettePlayers
{
    public interface IRoulettePlayer
    {
        /// <summary>
        /// Display name on the output
        /// </summary>
        string Name { get; }

        decimal MoneyTotal { get; }

        /// <summary>
        /// This function requests money from the player, which may be zero (but not negative).
        /// The total received by the player is implicitly deducted from his or her <see cref="MoneyTotal"/>.
        /// </summary>
        /// <returns>The bets that the player is making for the round</returns>
        IReadOnlyCollection<IRouletteBet> ReceiveBet();

        /// <summary>
        /// The Amount that the player receives at the end of a round
        /// </summary>
        /// <param name="amount">the winnings to add the player's <see cref="MoneyTotal"/> (cannot be negative)</param>
        void PaymentToPlayer(decimal amount);

        /// <summary>
        /// The Amount that the player receives at the end of a round
        /// </summary>
        /// <param name="result">Called to detail the result of the most recent roll from the wheel.</param>
        void NotifyTheResult(RouletteResult result);
    }
}