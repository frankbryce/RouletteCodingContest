using System;
using System.Collections.Generic;
using System.Threading;

namespace ConsoleApplication9.RouletteWheels
{
    public class ShadyCasinoRouletteWheel : IRouletteWheel
    {
        private readonly Random _random;
        private readonly Dictionary<RouletteResult, int> _rouletteResultProbabilities;
        private readonly int _enumLength;
        private const int _randomIndex = 1000;

        public ShadyCasinoRouletteWheel()
        {
            // or use some other clever seeding technique
            Thread.Sleep(1); // wait to make sure seed is unique in his program
            _random = new Random((int)DateTime.Now.Ticks);
            _enumLength = Enum.GetNames(typeof (RouletteResult)).Length;

            // build up probability array
            _rouletteResultProbabilities = new Dictionary<RouletteResult, int>();
            foreach (var idx in Enum.GetValues(typeof(RouletteResult)))
            {
                var rIdx = (RouletteResult)idx;
                _rouletteResultProbabilities[rIdx] = _randomIndex;
            }
        }

        public RouletteResult Spin()
        {
            var randomValue = _random.Next(_enumLength * _randomIndex);
            var accum = 0;
            foreach (var idx in Enum.GetValues(typeof(RouletteResult)))
            {
                var rIdx = (RouletteResult)idx;
                accum += _rouletteResultProbabilities[rIdx];
                if (accum > randomValue)
                {
                    foreach (var jdx in Enum.GetValues(typeof (RouletteResult)))
                    {
                        var rJdx = (RouletteResult)jdx;
                        if (rJdx == rIdx) _rouletteResultProbabilities[rJdx] -= _enumLength;
                        _rouletteResultProbabilities[rJdx]++;
                    }

                    return rIdx;
                }
            }
            return default(RouletteResult);
        }
    }
}