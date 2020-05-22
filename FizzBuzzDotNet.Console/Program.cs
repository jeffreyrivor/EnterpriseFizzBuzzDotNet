using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace FizzBuzzDotNet
{
    class Program
    {
        static readonly Func<int, string>[] FunctionCache = GenerateFunctionCache(15, i => i.ToString(),
            (i, j) => i + j, (i => i % 3 == 0, "Fizz"), (i => i % 5 == 0, "Buzz")).ToArray();

        static string Generate(int i) => FunctionCache[i % FunctionCache.Length](i);

        static void Main(string[] args)
        {
            var results = new List<string>(10000000);

            Stopwatch stopwatch = Stopwatch.StartNew();

            foreach (var i in Enumerable.Range(1, 10000000))
            {
                results.Add(Generate(i));
            }

            stopwatch.Stop();

            foreach (var line in results.Take(30))
            {
                Console.WriteLine(line);
            }

            Console.WriteLine(stopwatch.Elapsed);

            Console.ReadLine();
        }

        static IEnumerable<Func<int, TOutput>> GenerateFunctionCache<TOutput>(int range, Func<int, TOutput> defaultFunc,
            Func<TOutput, TOutput, TOutput> aggregator, params (Func<int, bool> predicate, TOutput output)[] staticOutputs) =>
            Enumerable.Range(0, range).Select(i =>
            {
                using var outputEnumerator = staticOutputs.Where(p => p.predicate(i)).Select(p => p.output).GetEnumerator();

                if (!outputEnumerator.MoveNext())
                {
                    return defaultFunc;
                }

                var output = outputEnumerator.Current;
                while (outputEnumerator.MoveNext())
                {
                    output = aggregator(output, outputEnumerator.Current);
                }

                return _ => output;
            });
    }
}
