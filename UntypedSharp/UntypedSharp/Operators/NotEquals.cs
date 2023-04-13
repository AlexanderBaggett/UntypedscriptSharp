using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UntypedSharp
{
    public partial class AnyHelpers
    {           
        public static bool operator ! (AnyHelpers self)
        {
            return !self;
        }
        public static bool operator !=(object left, AnyHelpers right)
        {
            return !(left == right);
        }

        public static bool operator <=(object left, AnyHelpers right)
        {
            return (left < right) || (left == right);
        }

        public static bool operator >=(object left, AnyHelpers right)
        {
            return (left < right) || left == right;
        }
    }
    public partial class Any<T> : AnyHelpers
    {

        public static bool operator !(Any<T> self)
        {
            return !self;
        }

        public static bool operator !=(Any<T> left, AnyHelpers right)
        {
            return !(left == right);
        }
        public static bool operator !=(Any<T> left, object right)
        {
            return !(left == right);
        }

        public static bool operator <=(Any<T> left, object right)
        {
            return (left < right) || left == right;
        }
        public static bool operator >=(Any<T> left, object right)
        {
            return (left > right) || (left == right);
        }


        public static bool operator <=(Any<T> left, AnyHelpers right)
        {

            return (left < right ) || (left == right);
        }


        public static bool operator >=(Any<T> left, AnyHelpers right)
        {
            return (left > right) || (left == right);
        }
    }
}
