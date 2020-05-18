using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.Extensions.Primitives;

namespace FizzBuzzDotNet
{
    class Program
    {
        static Func<int, StringSegment>[] GeneratorMap = new Func<int, StringSegment>[]
        {
            Generators.FizzBuzz,
            Generators.Default,
            Generators.Default,
            Generators.Fizz,
            Generators.Default,
            Generators.Buzz,
            Generators.Fizz,
            Generators.Default,
            Generators.Default,
            Generators.Fizz,
            Generators.Buzz,
            Generators.Default,
            Generators.Fizz,
            Generators.Default,
            Generators.Fizz,
        };

        static void Main(string[] args)
        {
            var results = new List<StringSegment>(10000000);

            Stopwatch stopwatch = Stopwatch.StartNew();

            foreach (var i in Enumerable.Range(1, 10000000))
            {
                results.Add(GeneratorMap[i % GeneratorMap.Length](i));
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