using System;
using System.Collections.Generic;
using System.Linq;
using FizzBuzzDotNet.Abstractions;
using FizzBuzzDotNet.Abstractions.Interfaces;

namespace FizzBuzzDotNet
{
    class CachedIntDivisibleValueGenerator<TOutput>
        : AggregatedValueGenerator<int, TOutput, TOutput>
    {
        private readonly Func<int, TOutput>[] _executeCache;

        public CachedIntDivisibleValueGenerator(
            IValuesAggregator<TOutput, TOutput> valuesAggregator,
            Func<int, TOutput> fallbackOutputGenerator,
            params (int Divisor, TOutput)[] divisorOutputPairs)
            : base(CreateValueGeneratorDelegate(divisorOutputPairs), valuesAggregator)
        {
            var divisors = divisorOutputPairs.Select(pair => pair.Divisor).ToArray();

            _executeCache = Enumerable.Range(0, GetLeastCommonMultiple(divisors))
                .Select(i => GetGenerator(i, divisors, fallbackOutputGenerator))
                .ToArray();
        }

        public override TOutput Execute(int input) => _executeCache[input % _executeCache.Length](input);

        private static ValueGeneratorDelegate<int, IEnumerable<TOutput>> CreateValueGeneratorDelegate(
            params (int Divisor, TOutput Value)[] divisorOutputPairs)
        {
            return new ValueGeneratorDelegate<int, IEnumerable<TOutput>>(input =>
                divisorOutputPairs.Where(pair => input % pair.Divisor == 0).Select(pair => pair.Value));
        }

        private Func<int, TOutput> GetGenerator(
            int input,
            int[] divisors,
            Func<int, TOutput> fallbackOutputGenerator)
        {
            if (divisors.Any(d => input % d == 0))
            {
                var lazy = new Lazy<TOutput>(() => base.Execute(input));

                return _ => lazy.Value;
            }

            return fallbackOutputGenerator;
        }

        private static int GetLeastCommonMultiple(params int[] divisors)
        {
            var temp = divisors.ToArray();

            while (true)
            {
                var min = int.MaxValue;
                var max = int.MinValue;

                for (var i = 0; i < temp.Length; i++)
                {
                    if (temp[i] < min)
                    {
                        min = temp[i];
                    }

                    if (temp[i] > max)
                    {
                        max = temp[i];
                    }
                }

                if (min != max)
                {
                    for (var i = 0; i < temp.Length; i++)
                    {
                        if (temp[i] == min)
                        {
                            temp[i] += divisors[i];
                        }
                    }

                    continue;
                }

                return min;
            }
        }
    }
}
