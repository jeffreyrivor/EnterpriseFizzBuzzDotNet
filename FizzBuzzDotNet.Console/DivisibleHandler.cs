using FizzBuzzDotNet.Abstractions.Interfaces;

namespace FizzBuzzDotNet
{
    class DivisibleHandler : IIterationHandler
    {
        private readonly IIterationHandler _handler;
        private readonly int _divisor;

        public DivisibleHandler(IIterationHandler handler, int divisor)
        {
            _handler = handler;
            _divisor = divisor;
        }

        public void Handle(int value)
        {
            if (value % _divisor == 0)
            {
                _handler.Handle(value);
            }
        }
    }
}