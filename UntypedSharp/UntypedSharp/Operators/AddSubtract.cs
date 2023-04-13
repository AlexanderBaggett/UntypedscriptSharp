﻿using Microsoft.VisualBasic;
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
            if (IsNumber(left) && IsNumber(right))
            {
                return AsNumber(left) + AsNumber(right);
            }
            return AsString(left) + AsString(right);
        }

        public static object operator -(object left, AnyHelpers right)
        {
            //they are both numbers
            if (IsNumber(left) && IsNumber(right))
            {
                return AsNumber(left) - AsNumber(right);
            }
            return "NaN";
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
            return AsString(left) + AsString(right);
        }


        public static object operator+(Any<T> left, AnyHelpers right)
        {
            if (IsNumber(left) && IsNumber(right))
            {
                return AsNumber(left) + AsNumber(right);
            }
            return AsString(left) + AsString(right);
        }

        public static object operator -(Any<T> left, object right)
        {
            //they are both numbers
            if (IsNumber(left) && IsNumber(right))
            {
                return AsNumber(left) - AsNumber(right);
            }
            return "NaN";
        }

        public static object operator -(Any<T> left, AnyHelpers right)
        {
            //they are both numbers
            if (IsNumber(left) && IsNumber(right))
            {
                return AsNumber(left) - AsNumber(right);
            }
            return "NaN";
        }


    }

}
