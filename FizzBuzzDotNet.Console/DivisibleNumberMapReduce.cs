using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using FizzBuzzDotNet.Abstractions;

namespace FizzBuzzDotNet
{
    public sealed class DivisibleNumberMapReduce<TNumber, TMap, TOutput> : MapReduce<TNumber, TMap, TOutput>
        where TNumber : IBinaryInteger<TNumber>
    {
        private TNumber LeastCommonMultiple { get; }

        private IReadOnlyDictionary<TNumber, TOutput> DivisorMapReduceOutputCache { get; }

        public DivisibleNumberMapReduce(
            Func<TNumber, TOutput> unmappedDefaultOutputFunction,
            Func<IEnumerable<TMap>, TOutput> pureDivisorMapValueReducerFunction,
            IEnumerable<(TNumber Divisor, TMap Value)> divisorMapValues) :
            this(unmappedDefaultOutputFunction, pureDivisorMapValueReducerFunction, divisorMapValues.ToArray())
        { }

        public DivisibleNumberMapReduce(
            Func<TNumber, TOutput> unmappedDefaultOutputFunction,
            Func<IEnumerable<TMap>, TOutput> pureDivisorMapValueReducerFunction,
            params (TNumber Divisor, TMap Value)[] divisorMapValues) :
            base(
                unmappedDefaultOutputFunction,
                pureDivisorMapValueReducerFunction,
                divisorMapValues.Select<(TNumber Divisor, TMap Value), (Func<TNumber, bool> Predicate, TMap Value)>(
                    x => ((TNumber n) => n % x.Divisor == TNumber.Zero, x.Value)))
        {
            var distinctDivisors = divisorMapValues.Select(x => x.Divisor).Distinct().ToArray();

            LeastCommonMultiple = GetLeastCommonMultiple(distinctDivisors);

            var resultCache = new Dictionary<TNumber, TOutput>();

            for (var i = TNumber.Zero; i < LeastCommonMultiple; i++)
            {
                if (distinctDivisors.Any(d => i % d == TNumber.Zero))
                {
                    resultCache[i] = base.Execute(i);
                }
            }

            DivisorMapReduceOutputCache = resultCache;
        }

        public override TOutput Execute(TNumber input) =>
            DivisorMapReduceOutputCache.TryGetValue(input % LeastCommonMultiple, out var value)
            ? value
            : UnmappedDefaultOutputFunction(input);

        private static TNumber GetLeastCommonMultiple(params TNumber[] distinctDivisors)
        {
            if (distinctDivisors.Length == 1)
            {
                return distinctDivisors[0];
            }

            var temp = distinctDivisors.ToArray();

            while (true)
            {
                var min = temp[0];
                var max = temp[0];

                for (var i = 1; i < temp.Length; i++)
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
                            temp[i] += distinctDivisors[i];
                        }
                    }

                    continue;
                }

                return min;
            }
        }
    }
}
