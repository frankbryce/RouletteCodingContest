namespace ConsoleApplication9.RouletteWheels
{
    /// <summary>
    /// Interface that all wheels in my simulator must abide by
    /// </summary>
    public interface IRouletteWheel
    {
        /// <summary>
        /// Rolling the Roulette Wheel according to some algorithm which is the meat and potatoes of the
        /// entire thing we've been hired for.
        /// </summary>
        /// <returns>The Result of the Roll</returns>
        RouletteResult Spin();
    }
}