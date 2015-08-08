namespace ConsoleApplication9
{

    public interface IPayoutCalculator
    {
        /// <summary>
        /// This is the method to call to calculate the amount the player gets back after a roll has been made.
        /// </summary>
        /// <param name="bet">The amount that the player bet before the roll</param>
        /// <param name="correct">Whether the player was correct</param>
        /// <returns>The amount the player received as a result of the bet and the roll result, which includes the total of their bet</returns>
        decimal PayoutAmount(decimal bet, bool correct);
    }

    public class PayoutCalculator : IPayoutCalculator
    {
        private double _odds;

        public PayoutCalculator()
        {
            _odds = 35; // default payout according to wikipedia
        }

        public PayoutCalculator(double odds)
        {
            _odds = odds;
        }

        public void SetPayoutRatio(double odds)
        {
            _odds = odds;
        }

        /// <summary>
        /// This is the method to call to calculate the amount the player gets back after a roll has been made.
        /// </summary>
        /// <param name="bet">The amount that the player bet before the roll</param>
        /// <param name="correct">Whether the player was correct</param>
        /// <returns>The amount the player received as a result of the bet and the roll result, which includes the total of their bet</returns>
        public decimal PayoutAmount(decimal bet, bool correct)
        {
            if (!correct) return 0;
            return bet * (decimal)_odds;
        }
    }
}