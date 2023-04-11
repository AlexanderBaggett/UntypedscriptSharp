using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UntypedSharp
{
    public partial class AnyHelpers
    {

        public static object operator +(object left, AnyHelpers right)
        {
            if (IsNumber(left) && IsNumber(right.GetValue()))
            {
                return AsNumber(left) + AsNumber(right.GetValue());
            }
            else if (IsString(left) && IsNumber(right))
            {
                return left.ToString() + right.GetValue().ToString();
            }
            else if (IsNumber(left) && IsString(right.GetValue()))
            {
                return left.ToString() + right.GetValue().ToString();
            }
            return left.ToString() + right.GetValue().ToString();
        }
    }

    public partial class Any<T> : AnyHelpers
    {
        public static object operator +(Any<T> left, object right)
        {
            if (IsNumber(left) && IsNumber(right))
            {
                return AsNumber(left) + AsNumber(right);
            }
            else if (IsString(left) && IsNumber(right))
            {
                return left.Value.ToString() + right.ToString();
            }
            else if (IsNumber(left) && IsString(right))
            {
                return left.Value.ToString() + right.ToString();
            }
            return left.Value.ToString() + right.ToString();
        }


        public static object operator+(Any<T> left, AnyHelpers right)
        {
            if (IsNumber(left) && IsNumber(right.GetValue()))
            {
                return AsNumber(left) + AsNumber(right.GetValue());
            }
            else if (IsString(left) && IsNumber(right.GetValue()))
            {
                return left.Value.ToString() + right.GetValue().ToString();
            }
            else if (IsNumber(left) && IsString(right.GetValue()))
            {
                return left.Value.ToString() + right.GetValue().ToString();
            }
            return left.Value.ToString() + right.GetValue().ToString();
        }

        public static object operator +(Any<T> left, decimal right)
        {
            if (IsNumber(left))
            {
                return AsNumber(left.Value) + right;
            }
            return left.Value.ToString() + right.ToString();
        }
        public static object operator +(decimal left, Any<T> right)
        {
            if (IsNumber(right))
            {
                return left + AsNumber(right);
            }
            return left.ToString() + right.Value.ToString();
        }

        public static object operator +(Any<T> left, double right)
        {
            if (IsNumber(left))
            {
                return AsNumber(left.Value) + (decimal)right;
            }
            return left.Value.ToString() + right.ToString();
        }
        public static object operator +(double left, Any<T> right)
        {
            if (IsNumber(right))
            {
                return (decimal)left + AsNumber(right);
            }
            return left.ToString() + right.Value.ToString();
        }
        public static object operator +(Any<T> left, int right)
        {
            if (IsNumber(left))
            {
                return AsNumber(left.Value) + right;
            }
            return left.Value.ToString() + right.ToString();
        }
        public static object operator +(int left, Any<T> right)
        {
            if (IsNumber(right))
            {
                return left + AsNumber(right);
            }
            return left.ToString() + right.Value.ToString();
        }
        public static object operator +(Any<T> left, float right)
        {
            if (IsNumber(left))
            {
                return AsNumber(left.Value) + (decimal)right;
            }
            return left.Value.ToString() + right.ToString();
        }
        public static object operator +(float left, Any<T> right)
        {
            if (IsNumber(right))
            {
                return (decimal)left + AsNumber(right);
            }
            return left.ToString() + right.Value.ToString();
        }
        public static object operator +(Any<T> left, byte right)
        {
            if (IsNumber(left))
            {
                return AsNumber(left.Value) + right;
            }
            return left.Value.ToString() + right.ToString();
        }
        public static object operator +(byte left, Any<T> right)
        {
            if (IsNumber(right))
            {
                return left + AsNumber(right);
            }
            return left.ToString() + right.Value.ToString();
        }
        public static object operator +(Any<T> left, short right)
        {
            if (IsNumber(left))
            {
                return AsNumber(left.Value) + right;
            }
            return left.Value.ToString() + right.ToString();
        }
        public static object operator +(short left, Any<T> right)
        {
            if (IsNumber(right))
            {
                return left + AsNumber(right);
            }
            return left.ToString() + right.Value.ToString();
        }
        public static object operator +(Any<T> left, sbyte right)
        {
            if (IsNumber(left))
            {
                return AsNumber(left.Value) + right;
            }
            return left.Value.ToString() + right.ToString();
        }
        public static object operator +(sbyte left, Any<T> right)
        {
            if (IsNumber(right))
            {
                return left + AsNumber(right);
            }
            return left.ToString() + right.Value.ToString();
        }
        public static object operator +(Any<T> left, ushort right)
        {
            if (IsNumber(left))
            {
                return AsNumber(left.Value) + right;
            }
            return left.Value.ToString() + right.ToString();
        }
        public static object operator +(ushort left, Any<T> right)
        {
            if (IsNumber(right))
            {
                return left + AsNumber(right);
            }
            return left.ToString() + right.Value.ToString();
        }
        public static object operator +(Any<T> left, uint right)
        {
            if (IsNumber(left))
            {
                return AsNumber(left.Value) + right;
            }
            return left.Value.ToString() + right.ToString();
        }
        public static object operator +(uint left, Any<T> right)
        {
            if (IsNumber(right))
            {
                return left + AsNumber(right);
            }
            return left.ToString() + right.Value.ToString();
        }
        public static object operator +(Any<T> left, ulong right)
        {
            if (IsNumber(left))
            {
                return AsNumber(left.Value) + right;
            }
            return left.Value.ToString() + right.ToString();
        }
        public static object operator +(ulong left, Any<T> right)
        {
            if (IsNumber(right))
            {
                return left + AsNumber(right);
            }
            return left.ToString() + right.Value.ToString();
        }
    }

}
