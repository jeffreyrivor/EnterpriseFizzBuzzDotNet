using System;
using System.Collections.Generic;
using System.Linq;
using FizzBuzzDotNet.Abstractions;
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
                new ConditionalIterationHandler(new DivisibleChecker(3), new ArbitraryStringLoggingHandler("Fizz", stringConsoleLogger)),
                new ConditionalIterationHandler(new DivisibleChecker(5), new ArbitraryStringLoggingHandler("Buzz", stringConsoleLogger)),
                new ConditionalIterationHandler(new IndivisibleChecker(3, 5), new ValueLoggingHandler(consoleLogger)),
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