using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Dynamic;

namespace UntypedSharp
{

    public partial class AnyHelpers
    {
        internal static bool IsLessThan(string s1, string s2)
        {
            int len = Math.Min(s1.Length, s2.Length);

            for (int i = 0; i < len; i++)
            {
                if (s1[i] < s2[i])
                {
                    return true;
                }
                else if (s1[i] > s2[i])
                {
                    return false;
                }
                else if (s1[i] == s2[i])
                {
                    continue;
                }
            }

            return s1.Length < s2.Length;
        }

        internal static bool IsAnyGenericType(object obj)
        {
            var anytype = typeof(Any<>);
            var objType = obj.GetType();

            return objType.IsGenericType &&
            objType.GetGenericTypeDefinition() == anytype;

        }
        internal static bool IsBool(object obj) =>  obj is bool;

        internal static bool AsBool(object obj) => (bool)obj;

        internal static bool IsDate(object obj) =>  obj is DateTime || obj is DateTimeOffset;

        internal static DateTimeOffset AsDate(object obj) => (DateTimeOffset)obj;
        internal static bool IsNumber(object obj) => !(obj is string) && !(obj is bool) && Information.IsNumeric(obj);

        internal static decimal AsNumber(object obj) => Convert.ToDecimal(obj);

        internal static bool IsString(object obj) => IsNull(obj) ? true :  obj is string;

        internal static string AsString(object obj) => obj.ToString();

        internal static bool IsNull(object obj) => obj == null;


        internal static bool IsFalsy(object any)
        {

            if (any == null) return true;

            var value = any;

            var isEmptyString = any is string && string.IsNullOrEmpty(value.ToString());
            var isFalseString = any is string && value.ToString().Trim().ToLower() == "false";
            var isFalse = value is bool tg && tg == false;
            var isZeroString = value is string && value.ToString().Trim().ToLower() == "0";

            if (IsNumber(any))
            {
                //next-level bullshittery
                double x = Convert.ToDouble(value);
                var isZero = x == 0;

                if (isZero) return true;
            }

            return isFalse || isEmptyString || isFalseString || isZeroString;

        }

        internal object GetValue()
        {
            var x = this as dynamic;
            return x.Value;
        }
        /// <summary>
        /// //
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        internal static bool IsBool(AnyHelpers obj) => obj.GetValue() is bool;
        internal static bool AsBool(AnyHelpers obj) => (bool)obj.GetValue();

        internal static bool IsDate(AnyHelpers obj) => obj.GetValue() is DateTime || obj.GetValue() is DateTimeOffset;

        internal static DateTimeOffset AsDate(AnyHelpers obj) => (DateTimeOffset)obj.GetValue();
        internal static bool IsNumber(AnyHelpers obj) => !(obj.GetValue() is string) &&  !(obj.GetValue() is bool) && Information.IsNumeric(obj.GetValue());

        internal static decimal AsNumber(AnyHelpers obj) => Convert.ToDecimal(obj.GetValue());

        internal static bool IsString(AnyHelpers obj) => obj.GetValue() is string;
        internal static string AsString(AnyHelpers obj) =>  obj.GetValue().ToString();

        internal static bool IsNull(AnyHelpers obj) =>  obj.GetValue() == null;


        internal static bool IsFalsy(AnyHelpers any)
        {
            var nullchecker = (object)any;

            //don't want to infinite loop with our == operator
            if (nullchecker == null) return true;

            var value = any.GetValue();

            if (value == null) return true;

            var isEmptyString = value is string && string.IsNullOrEmpty(value.ToString());
            var isFalseString = value is string && value.ToString().Trim().ToLower() == "false";
            var isFalse = value is bool tg && tg == false;
            var isZeroString = value is string && value.ToString().Trim().ToLower() == "0";

            if (IsNumber(any))
            {
                //next-level bullshittery
                double x = Convert.ToDouble(value);
                var isZero = x == 0;

                if (isZero) return true;
            }

            return isFalse || isEmptyString || isFalseString || isZeroString;

        }

    }
}
