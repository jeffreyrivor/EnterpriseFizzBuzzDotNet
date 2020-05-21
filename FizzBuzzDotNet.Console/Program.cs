using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace FizzBuzzDotNet
{
    class Program
    {
        static string Fizz(int _) => "Fizz";

        static string Buzz(int _) => "Buzz";

        static string FizzBuzz(int _) => "FizzBuzz";

        static string Default(int i) => i.ToString();

        static Func<int, string>[] Generators = new Func<int, string>[]
        {
            FizzBuzz,
            Default,
            Default,
            Fizz,
            Default,
            Buzz,
            Fizz,
            Default,
            Default,
            Fizz,
            Buzz,
            Default,
            Fizz,
            Default,
            Default
        };

        static void Main(string[] args)
        {
            var results = new List<string>(10000000);

            Stopwatch stopwatch = Stopwatch.StartNew();

            foreach (var i in Enumerable.Range(1, 10000000))
            {
                results.Add(Generators[i % Generators.Length](i));
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
