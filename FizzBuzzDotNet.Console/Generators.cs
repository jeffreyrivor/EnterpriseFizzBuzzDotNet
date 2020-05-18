using Microsoft.Extensions.Primitives;

namespace FizzBuzzDotNet
{
    static class Generators
    {
        private static readonly StringSegment _fizzBuzz = new StringSegment("FizzBuzz");

        private static readonly StringSegment _fizz = _fizzBuzz.Subsegment(0, 4);

        private static readonly StringSegment _buzz = _fizzBuzz.Subsegment(4);

        public static StringSegment Default(int i) => i.ToString();

        public static StringSegment Fizz(int _) => _fizz;

        public static StringSegment Buzz(int _) => _buzz;

        public static StringSegment FizzBuzz(int _) => _fizzBuzz;
    }
}
