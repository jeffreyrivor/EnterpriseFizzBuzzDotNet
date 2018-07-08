using System;
using FizzBuzzDotNet.Abstractions.Interfaces;

namespace FizzBuzzDotNet
{
    class ConsoleLogger<T> : ILogger<T>
    {
        public void Log(T value)
        {
            Console.Write(value);
        }
    }
}