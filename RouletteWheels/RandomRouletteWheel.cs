using System;
using System.Threading;

namespace ConsoleApplication9.RouletteWheels
{
    public class RandomRouletteWheel : IRouletteWheel
    {
        private readonly Random _random;

        public RandomRouletteWheel()
        {
            // or use some other clever seeding technique
            Thread.Sleep(1); // wait to make sure seed is unique in his program
            _random = new Random((int)DateTime.Now.Ticks);
        }

        public RouletteResult Spin()
        {
            return (RouletteResult)_random.Next(Enum.GetNames(typeof(RouletteResult)).Length);
        }
    }
}