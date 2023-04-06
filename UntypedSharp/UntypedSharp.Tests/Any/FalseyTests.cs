using Xunit;

namespace UntypedSharp.Tests
{
    public class FalseyTests
    {

        [Theory]
        [InlineData(false)]
        [InlineData("false")]
        public void FalseIsFalsey<T>(T value)
        {
            Any any = new Any(value);

            Assert.False(any);
        }

        [Theory]
        [InlineData(0)]
        [InlineData("0")]
        public void ZeroIsFalsey<T>(T value)
        {
            Any any = new Any(value);

            Assert.False(any);
        }

        [Theory]
        [InlineData("")]
        public void EmptyStringIsFalsey<T>(T value)
        {
            Any any = new Any(value);

            Assert.False(any);
        }

        [Theory]
        [InlineData(null)]
        public void NullIsFalsey<T>(T value)
        {
            Any any = new Any(value);

            Assert.False(any);

            any = null;

            Assert.False(any);
        }

    }
}