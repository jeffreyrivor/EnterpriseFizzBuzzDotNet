using System;
using System.Collections.Generic;
using System.Linq;
using FizzBuzzDotNet.Abstractions.Interfaces;

namespace FizzBuzzDotNet
{
    class Program
    {
        static void Main(string[] args)
        {
            var consoleLogger = new ConsoleLogger<int>();
            var stringConsoleLogger = new StringConsoleLogger();

            var orderedHandler = new OrderedIterationHandler(new List<IIterationHandler>
            {
                new DivisibleHandler(new ArbitraryStringLoggingHandler("Fizz", stringConsoleLogger), 3),
                new DivisibleHandler(new ArbitraryStringLoggingHandler("Buzz", stringConsoleLogger), 5),
                new IndivisibleHandler(new IndivisibleHandler(new ValueLoggingHandler(consoleLogger), 5), 3),
                new ArbitraryStringLoggingHandler(Environment.NewLine, stringConsoleLogger)
            });

            foreach (var i in Enumerable.Range(1, 100))
            {
                orderedHandler.Handle(i);
            }

            Console.ReadLine();
        }
    }
}