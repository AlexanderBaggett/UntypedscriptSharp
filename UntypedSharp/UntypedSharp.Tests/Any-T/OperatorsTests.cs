using Microsoft.VisualBasic;
using System;
using System.Linq;
using Xunit;
namespace UntypedSharp.Tests.Any_T
{
    public class OperatorsTests
    {
        public static bool IsNumber(object obj)
        {
            return !(obj is string) && Information.IsNumeric(obj);
        }


        [Theory]
        [InlineData(1, 2, 3,3)]
        [InlineData(1, "2", "12","21")]
        [InlineData("josh ", "doesn't get it","josh doesn't get it", "doesn't get itjosh ")]

        public void PlusWorks<T1,T2>(T1 first, T2 second, object expectedResult, object reversedExpected)
        {
            Any<T1> any = first;

            Any<T2> any2 = second;

            var test1 = any + any2;
            var test2 = any2 + any;
            var test3 = any + second;
            var test4 = second + any;

            //xUnit doesn't directly support decimal
            if(IsNumber(expectedResult))
            {
                var decimalResult = Convert.ToDecimal(expectedResult);

                Assert.Equal(decimalResult, test1);
                Assert.Equal(decimalResult, test2);
                Assert.Equal(decimalResult, test3);
                Assert.Equal(decimalResult, test4);
            }
            else
            {
                Assert.Equal(expectedResult, test1);
                Assert.Equal(reversedExpected, test2);
                Assert.Equal(expectedResult, test3);
                Assert.Equal(reversedExpected, test4);
            }
        }
        [Theory]
        [InlineData(1,2,2)]
        [InlineData(4,4,16)]
        [InlineData(4,"","NaN")]
        [InlineData("",4, "NaN")]
        public void MultiplyWorks<T1,T2>(T1 first, T2 second, object expectedResult)
        {
            Any<T1> any = first;
            Any<T2> any2 = second;

            var test1 = any * any2;
            var test2 = any2 * any;
            var test3 = any * second;
            var test4 = second * any;

            if (IsNumber(expectedResult))
            {
                var decimalResult = Convert.ToDecimal(expectedResult);

                Assert.Equal(decimalResult, test1);
                Assert.Equal(decimalResult, test2);
                Assert.Equal(decimalResult, test3);
                Assert.Equal(decimalResult, test4);
            }
            else
            {
                Assert.Equal(expectedResult, test1);
                Assert.Equal(expectedResult, test2);
                Assert.Equal(expectedResult, test3);
                Assert.Equal(expectedResult, test4);
            }
        }

        [Theory]
        [InlineData(2, 2, 1)]
        [InlineData(16, 16, 1)]
        [InlineData(4, "", "NaN")]
        [InlineData("", 4, "NaN")]
        public void DivideWorks<T1, T2>(T1 first, T2 second, object expectedResult)
        {
            Any<T1> any = first;
            Any<T2> any2 = second;

            var test1 = any / any2;
            var test2 = any2 / any;
            var test3 = any / second;
            var test4 = second / any;

            if (IsNumber(expectedResult))
            {
                var decimalResult = Convert.ToDecimal(expectedResult);

                Assert.Equal(decimalResult, test1);
                Assert.Equal(decimalResult, test2);
                Assert.Equal(decimalResult, test3);
                Assert.Equal(decimalResult, test4);
            }
            else
            {
                Assert.Equal(expectedResult, test1);
                Assert.Equal(expectedResult, test2);
                Assert.Equal(expectedResult, test3);
                Assert.Equal(expectedResult, test4);
            }
        }

        [Theory]
        [InlineData(5, 2, 3,-3)]
        [InlineData(1, 2, -1,1)]
        [InlineData(4, "", "NaN", "NaN")]
        [InlineData("", 4, "NaN", "NaN")]
        public void SubtractWorks<T1, T2>(T1 first, T2 second, object expectedResult, object reversed)
        {
            Any<T1> any = first;
            Any<T2> any2 = second;

            var test1 = any - any2;
            var test2 = any2 - any;
            var test3 = any - second;
            var test4 = second - any;

            if (IsNumber(expectedResult))
            {
                var decimalResult = Convert.ToDecimal(expectedResult);

                var reversedResult = Convert.ToDecimal(reversed);

                Assert.Equal(decimalResult, test1);
                Assert.Equal(reversedResult, test2);
                Assert.Equal(decimalResult, test3);
                Assert.Equal(reversedResult, test4);
            }
            else
            {
                Assert.Equal(expectedResult, test1);
                Assert.Equal(expectedResult, test2);
                Assert.Equal(expectedResult, test3);
                Assert.Equal(expectedResult, test4);
            }
        }
        [Theory]
        [InlineData(true, false, false)]
        [InlineData(false, true, true)]
        [InlineData(true,true,false)]
        [InlineData(false, false, false)]
        [InlineData(2,4,true)]
        [InlineData(4,2,false)]
        [InlineData(false,1,true)]
        [InlineData(null,1,true)]
        public void LessThanWorks<T1,T2>(T1 first, T2 second,  bool expectedResult)
        {
            Any<T1> any = first;
            Any<T2> any2 = second;

            Assert.Equal(expectedResult, any < any2);
            Assert.Equal(expectedResult, any < second);
            Assert.Equal(expectedResult, first < any2);

        }

        [Theory]
        [InlineData(true, false, true)]
        [InlineData(false, true, false)]
        [InlineData(true, true, false)]
        [InlineData(false, false, false)]
        [InlineData(2, 4, false)]
        [InlineData(4, 2, true)]
        [InlineData(1, false, true)]
        [InlineData(1, null, true)]
        public void GreaterThanWorks<T1, T2>(T1 first, T2 second, bool expectedResult)
        {
            Any<T1> any = first;
            Any<T2> any2 = second;

            Assert.Equal(expectedResult, any > any2);
            Assert.Equal(expectedResult, any > second);
            Assert.Equal(expectedResult, first > any2);

        }


        [Theory]
        [InlineData("false", false, true)]
        [InlineData("true", true, true)]
        [InlineData(true, true, true)]
        [InlineData(false, true, false)]
        [InlineData(false, false, true)]
        [InlineData(4, "4", true)]
        [InlineData(4, 4, true)]
        //[InlineData(false, null, true)] this test needs some love
        [InlineData(1, true, true)]
        [InlineData(0, false, true)]
        public void EqualsWorks<T1, T2>(T1 first, T2 second, bool expectedResult)
        {
            Any<T1> any = first;
            Any<T2> any2 = second;

            Assert.Equal(expectedResult, any == any2);
            Assert.Equal(expectedResult, any == second);
            Assert.Equal(expectedResult, first == any2);

        }

    }
}
