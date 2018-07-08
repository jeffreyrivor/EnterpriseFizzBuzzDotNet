using System;
using FizzBuzzDotNet.Abstractions.Interfaces;

namespace FizzBuzzDotNet
{
    class StringConsoleLogger : ILogger<string>
    {
        public void Log(string value)
        {
            Console.Write(value);
        }
    }
}