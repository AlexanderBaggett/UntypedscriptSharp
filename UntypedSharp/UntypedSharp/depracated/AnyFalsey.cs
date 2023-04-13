using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UntypedSharp.depracated
{
    public partial class Any
    {
        internal static bool IsBool(Any any) => any.Value is bool;
        internal static bool IsString(Any any) => any.Value is string;

        internal static bool IsNumber(Any any) => Information.IsNumeric(any.Value);

        internal static bool IsDate(Any any) =>  any.Value is DateTime || any.Value is DateTimeOffset;

        internal static decimal AsNumber(Any any) => Convert.ToDecimal(any.Value);

        internal static bool AsBool(Any any) => (bool)any.Value;

        private static bool IsFalsy(Any any)
        {
            if (any == null) return true;

            var value = any.Value;

            if (value == null) return true;

            var isEmptyString = IsString(any) && string.IsNullOrEmpty(any.Value.ToString());
            var isFalseString = IsString(any) && any.Value.ToString().Trim().ToLower() == "false";
            var isFalse = value is bool tg && tg == false;
            var isZeroString = value is string && value.ToString().Trim().ToLower() == "0";
            var isZero = IsNumber(any) && Convert.ToDouble(any.Value) == 0;

            return  isFalse || isEmptyString || isFalseString || isZeroString || isZero;

        }
    }
}
