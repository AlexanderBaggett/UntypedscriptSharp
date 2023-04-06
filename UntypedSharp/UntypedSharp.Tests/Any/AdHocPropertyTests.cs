using Xunit;
using System;

namespace UntypedSharp.Tests
{
    public class AdHocPropertyTests
    {
        [Theory]
        [InlineData(default(int), default(int))]
        [InlineData(default(string), default(int))]
        [InlineData(default(int), default(string))]
        [InlineData(default(double), default(string))]
        [InlineData(default(bool), default(string))]
        [InlineData(default(int), default(double))]
        [InlineData(default(double), default(int))]
        [InlineData(default(bool), default(int))]
        [InlineData(default(int), default(bool))]
        [InlineData(default(double), default(bool))]
        [InlineData(default(bool), default(bool))]
        public void CanAddPropertyViaIndexer<T1,T2>(T1 propertyValue, T2 anyValue)
        {
            Any any = new Any(anyValue);
            
            var propertyName = "new Property";

            any[propertyName] = propertyValue;

            Assert.Equal(propertyValue, any[propertyName]);
        }
    }
}
