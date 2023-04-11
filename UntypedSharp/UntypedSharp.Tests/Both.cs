using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using UntypedSharp;

namespace UntypedSharp.Tests
{
    public class BothTests
    {

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        [InlineData(10)]
        [InlineData("Hello Converter")]

        public void CanConvertBackAndForth<T>(T value)
        {
            Any any = new Any(value);
            Any<T> anyt = value;

            //equal values
            Assert.Equal(value, any.Value);
            Assert.Equal(value, anyt.Value);

            //forward conveersions
            Any<T> anyt2 = any;
            Assert.Equal(any.Value, anyt2.Value);

            //backwardConversions
            Any any2 = anyt;
            Assert.Equal(anyt.Value, any2.Value);
        }

        //someone ought to ask Xunit why they can't do decimals, datetimes, or guids, in their InlineData()
    }
}
