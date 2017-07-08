using System.Collections.Generic;
using FizzBuzzDotNet.Abstractions.Interfaces;

namespace FizzBuzzDotNet
{
    class OrderedIterationHandler : IIterationHandler
    {
        private readonly IEnumerable<IIterationHandler> _handlers;

        public OrderedIterationHandler(IEnumerable<IIterationHandler> handlers)
        {
            _handlers = handlers;
        }

        public void Handle(int value)
        {
            foreach (var handler in _handlers)
            {
                handler.Handle(value);
            }
        }
    }
}