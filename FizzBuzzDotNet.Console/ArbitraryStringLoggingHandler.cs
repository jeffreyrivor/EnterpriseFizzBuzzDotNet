using System;
using FizzBuzzDotNet.Abstractions.Interfaces;

namespace FizzBuzzDotNet
{
    class ArbitraryStringLoggingHandler : IIterationHandler
    {
        private readonly string _str;

        public ArbitraryStringLoggingHandler(string str)
        {
            _str = str;
        }

        public void Handle(int value)
        {
            Console.Write(_str);
        }
    }
}