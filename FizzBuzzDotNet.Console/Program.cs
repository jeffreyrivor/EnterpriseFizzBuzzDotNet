using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace FizzBuzzDotNet
{
    class OrderedPredicateValueAggregator<TInput, TOutput>
    {
        private readonly Func<TOutput, TOutput, TOutput> _aggregateFunc;
        private readonly Func<TInput, TOutput> _fallbackFunc;
        private readonly IEnumerable<(Predicate<TInput> Predicate, TOutput Value)> _predicateValuePairs;

        public OrderedPredicateValueAggregator(
            Func<TOutput, TOutput, TOutput> aggregateFunc,
            Func<TInput, TOutput> fallbackFunc,
            params (Predicate<TInput>, TOutput)[] predicateValuePairs)
        {
            _aggregateFunc = aggregateFunc;
            _fallbackFunc = fallbackFunc;
            _predicateValuePairs = predicateValuePairs;
        }

        public virtual TOutput Execute(TInput value)
        {
            TOutput output;

            using (IEnumerator<TOutput> e = ExecutePredicates(value).GetEnumerator())
            {
                if (e.MoveNext())
                {
                    output = e.Current;

                    while (e.MoveNext())
                    {
                        output = _aggregateFunc(output, e.Current);
                    }
                }
                else
                {
                    output = _fallbackFunc(value);
                }
            }

            return output;
        }

        protected internal IEnumerable<TOutput> ExecutePredicates(TInput input) =>
            _predicateValuePairs.Where(i => i.Predicate(input)).Select(i => i.Value);
    }

    class OrderedIntDivisiblePredicateValueAggregator<TOutput>
        : OrderedPredicateValueAggregator<int, TOutput>
    {
        private readonly Func<int, TOutput>[] _cache;

        public OrderedIntDivisiblePredicateValueAggregator(
            Func<TOutput, TOutput, TOutput> aggregateFunc,
            Func<int, TOutput> fallbackFunc,
            params (int Divisor, TOutput Value)[] divisorValuePairs) :
            base(
                aggregateFunc,
                fallbackFunc,
                divisorValuePairs.Select(pair => new ValueTuple<Predicate<int>, TOutput>(value => value % pair.Divisor == 0, pair.Value)).ToArray())
        {
            var divisors = divisorValuePairs.Select(pair => pair.Divisor).ToArray();

            _cache = Enumerable.Range(0, GetLeastCommonMultiple(divisors))
                .Select(i => divisors.Any(d => i % d == 0) ? CreateCachedOutputFunction(i) : fallbackFunc)
                .ToArray();
        }

        public override TOutput Execute(int value) => _cache[value % _cache.Length](value);

        private Func<int, TOutput> CreateCachedOutputFunction(int input)
        {
            var lazy = new Lazy<TOutput>(() => base.Execute(input));

            return _ => lazy.Value;
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

    class Program
    {
        static void Main(string[] args)
        {
            var executor = new OrderedIntDivisiblePredicateValueAggregator<string>(
                (prev, next) => prev + next,
                value => value.ToString(),
                (3, "Fizz"),
                (5, "Buzz"));

            var results = new List<string>(10000000);

            Stopwatch stopwatch = Stopwatch.StartNew();

            foreach (var i in Enumerable.Range(1, 10000000))
            {
                results.Add(executor.Execute(i));
            }

            stopwatch.Stop();

            //foreach (var line in results)
            //{
            //    Console.WriteLine(line);
            //}

            Console.WriteLine(stopwatch.Elapsed);

            Console.ReadLine();
        }
    }
}