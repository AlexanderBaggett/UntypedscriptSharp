using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UntypedSharp.depracated;

namespace UntypedSharp
{
    public partial class AnyHelpers
    {
        public static bool operator >(object left, AnyHelpers right)
        {
            //they are both numbers
            if (IsNumber(left) && IsNumber(right))
            {
                return AsNumber(left) > AsNumber(right);
            }
            if (IsBool(left) && IsBool(right))
            {
                var b2 = AsBool(right);
                if (b2)
                {
                    return false;
                }
                return true; //true will always be > false and < true
            }
            if (IsDate(left) && IsDate(right))
            {
                var date1 = AsDate(left);
                var date2 = AsDate(right);
                return date1 > date2;
            }
            if (IsString(left) && IsString(right))
            {
                var string1 =AsString(left);
                var string2 = AsString(right);

                return !AnyHelpers.IsLessThan(string1, string2);

            }
            return left > right;
        }



    }
    public partial class Any<T> : AnyHelpers
    {

        public static bool operator >(Any<T> left, AnyHelpers right)
        {
            //they are both numbers
            if (IsNumber(left) && IsNumber(right))
            {
                return AsNumber(left) > AsNumber(right);
            }
            if (IsBool(left) && IsBool(right))
            {
                var b2 = AsBool(right);
                if (b2)
                {
                    return false;
                }
                return true; //true will always be > false and < true
            }
            if (IsDate(left) && IsDate(right))
            {
                var date1 = AsDate(left);
                var date2 = AsDate(right);
                return date1 > date2;
            }
            if (IsString(left) && IsString(right))
            {
                var string1 = AsString(left);
                var string2 = AsString(right);

                return !AnyHelpers.IsLessThan(string1, string2);

            }
            return left > right;
        }
        public static bool operator >(Any<T> left, object right)
        {
            //they are both numbers
            if (IsNumber(left) && IsNumber(right))
            {
                return AsNumber(left) > AsNumber(right);
            }
            if (IsBool(left) && IsBool(right))
            {
                var b2 = AsBool(right);
                if (b2)
                {
                    return false;
                }
                return true; //true will always be > false and < true
            }
            if (IsDate(left) && IsDate(right))
            {
                var date1 = AsDate(left);
                var date2 = AsDate(right);
                return date1 > date2;
            }
            if (IsString(left) && IsString(right))
            {
                var string1 = AsString(left);
                var string2 = AsString(right);

                return !AnyHelpers.IsLessThan(string1, string2);

            }
            return left > right;
        }
    }
}