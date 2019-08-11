using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using FizzBuzzDotNet.Abstractions;

namespace FizzBuzzDotNet
{
    class Program
    {
        private static string Aggregate(IEnumerable<string> inputs) =>
            inputs.Aggregate((curr, next) => curr + next);

        static void Main(string[] args)
        {
            var executor = new CachedIntDivisibleValueGenerator<string>(
                new ValuesAggregatorDelegate<string, string>(Aggregate),
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

            foreach (var line in results.Take(30))
            {
                Console.WriteLine(line);
            }

            Console.WriteLine(stopwatch.Elapsed);

            Console.ReadLine();
        }
    }
}
