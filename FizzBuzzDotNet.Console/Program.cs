using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.Extensions.Primitives;

namespace FizzBuzzDotNet
{
    class Program
    {
        static void Main(string[] args)
        {
            var fizzBuzz = new StringSegment("FizzBuzz");

            var results = new List<string>(10000000);

            Stopwatch stopwatch = Stopwatch.StartNew();

            foreach (var i in Enumerable.Range(1, 10000000))
            {
                var idx = 4; // 'B' at beginning of 'Buzz' in char array
                var count = 0; // no letters to print from char array

                if (i % 3 == 0)
                {
                    idx = 0; // 'F' at beginning of 'Fizz' in char array
                    count += 4; // letters to print = 'Fizz'
                }

                if (i % 5 == 0)
                {
                    count += 4; // add letters to print = 'Buzz'; when both i % 3 and i % 5, letters to print = 'FizzBuzz'
                }

                if (count > 0)
                {
                    results.Add(fizzBuzz.Substring(idx, count));
                }
                else
                {
                    results.Add(i.ToString());
                }
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