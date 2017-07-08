using System;
using FizzBuzzDotNet.Abstractions.Interfaces;

namespace FizzBuzzDotNet
{
    class ValueLoggingHandler : IIterationHandler
    {
        public void Handle(int value)
        {
            Console.Write(value);
        }
    }
}