using ConsoleApplication9.RouletteWheels;

namespace ConsoleApplication9
{
    public interface IRouletteBet
    {
        /// <summary>
        /// Non-negative amount that the player is betting for this bet on the roulette wheel
        /// </summary>
        decimal Amount { get; }

        /// <summary>
        /// The result that the player put this <see cref="Amount"/> on
        /// </summary>
        RouletteResult ResultBetOn { get; }
    }

    /// <summary>
    /// Only used by players, because the runner doesn't need access to the setters.  Not that it matters all that much
    /// </summary>
    public class RouletteBet : IRouletteBet
    {
        /// <summary>
        /// Non-negative amount that the player is betting for this bet on the roulette wheel
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// The result that the player put this <see cref="Amount"/> on
        /// </summary>
        public RouletteResult ResultBetOn { get; set; }
    }
}